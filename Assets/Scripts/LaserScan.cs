//==========================================
// Title:  LaserScan
// Author: HDH : MH, OC
// Date:   30 Jun 2021
//==========================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LaserScan : MonoBehaviour
{
	public GameObject linePrefab;
	// offsets unused but left for posterity
	public float ydecrement = 0;
	public float xoffset = 0;
	public float zoffset = 0;
	[Tooltip("Distance to draw lines when they don't hit a wall")]
	public float linedistance;
	[Tooltip("The sphere is both a visual element and a way to consistently calculate a vector from the robot-out")]
	public GameObject target;
	// combo slider/values to give users access to laser count and angle values
	public Slider count;
	public Slider setAngle;
	[Tooltip("The graphs for the lasers, green is a separate graph for brevity")]
	public UnityEngine.UI.Extensions.UILineRenderer graphRenderer, greenOneRenderer;
	private int numberLines;
	private float angle;
	// menu adjustable defaults removed for now, left if needed
	//private string laserskey = "Lasers";
	//private string anglekey = "Angle";
	private int defaultLasers = 15;
	private float defaultAngle = 230.0f;
	private GameObject[] lines;
	bool upCount, upAngle, lateStart = false;
	// materials to switch the laser from red to green and vv
	public Material green, red;
	// keeps Track of the index of the laser that should be green( based on hardcoded 1/3(numberlines) + 1)
	int greenOne;
	float[] randList;

	void Start()
	{
		upCount = upAngle = lateStart = true;

		// menu adjustable defaults removed for now, left if needed
		//numberLines =  PlayerPrefs.GetInt(laserskey, defaultLasers);
		//angle = PlayerPrefs.GetFloat(anglekey, defaultAngle);
		numberLines = defaultLasers;
		angle = defaultAngle;
		randList = new float[50];
		lines = new GameObject[50]; // there are always 50 lasers ready, we just shrink the ones we don't use
		for (int i = 0; i < lines.Length; i++)
		{
			lines[i] = Instantiate(linePrefab);
			lines[i].transform.parent = this.transform;
			randList[i] = Random.value;
		}
		greenOne = numberLines / 3 + 1;
		lines[greenOne].GetComponent<LineRenderer>().material = green;
		StartCoroutine(coRandList());
	}
    void FixedUpdate()
    {
		// set the laser object in the right spot after the start in case it bugs out
        if (lateStart)
        {
			gameObject.transform.localRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
			lateStart = false;
        }
		// These are on booleans for speed of processing, if the slider get's adjusted, the value will update, else it will get passed
		if (upAngle)
		{
			angle = setAngle.value;
			upAngle = false;
		}
		 // These cont. + update the integer rep of green one and change approp. materials
		if (upCount)
		{
			numberLines = (int)count.value;
			upCount = false;
			lines[greenOne].GetComponent<LineRenderer>().material = red;
			greenOne = numberLines / 3  +  1;
			lines[greenOne].GetComponent<LineRenderer>().material = green;
		}

		// Bit shift the index of the layer (8) to get a bit mask
		int layerMask =  1 << LayerMask.NameToLayer("walls")     ;
		layerMask    |= (1 << LayerMask.NameToLayer("laserscan"));

		// This would cast rays only against colliders in layer 8.
		float[] hitDistances = new float[numberLines];
		// angles only work because 0 is in the center, and we move from the positive halfway point to the negative for symmetry
		float currangle = numberLines >1 ? numberLines > 2 ? angle/2 : 45f: 0f;
        float angleincrement = angle / (numberLines);
		for (int i =0; i < numberLines; i++) {
			var lineRenderer    = lines[i].GetComponent<LineRenderer>();
			Vector3 direction   = Quaternion.Euler(0, -currangle + .0001f, 0) * new Vector3((transform.position - target.transform.position).x, 0, (transform.position - target.transform.position).z);
			Vector3 startsPunkt = this.transform.position; // new Vector3(transform.position.x-xoffset, transform.position.y - ydecrement, transform.position.z - zoffset); 
			
			RaycastHit hit;
			// Does the ray intersect any walls
			if (Physics.Raycast(startsPunkt, direction, out hit, Mathf.Infinity, layerMask))
			{
				Vector3 _point = hit.point;
				// construct a line in real space from the laserscanner to the hit point
				
				hitDistances[i] = hit.distance;
				//Debug.Log(hit.distance);
				//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
				//Debug.Log("Did Hit");

				
				if (hit.collider.gameObject.name == "TranslucentDoorCollider")
				{
					//Random.seed = (int)(hit.point.x + hit.point.y + hit.point.z);
					float val = randList[i];
					// override point if translucent collided
					_point = startsPunkt + (direction * linedistance) * val;
					// mark the distance as translucent using negative numbers, for the graphs sake
					hitDistances[i] *= -1;
				}

				Vector3[] positions = new Vector3[] { startsPunkt, _point};
				lineRenderer.SetPositions(positions);
			}

			// else constuct a line from the laserscanner-out, the maximum distance
			// BUGGIE
			//   Note this should not happen because of the colliders outside of the hospital
			//		Left for posterity
			else
			{
				
				Vector3[] positions = new Vector3[]{startsPunkt, new Vector3 (direction.x * linedistance, this.transform.position.y, direction.z * linedistance) };
				lineRenderer.SetPositions(positions);
				hitDistances[i] = linedistance;
				//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
				//Debug.Log("Did not Hit");
			}

			// angles cont.
			currangle -= angleincrement;
		}
		//set the unused lines to sit inside the laserscanner's purple sphere
		for (int i = numberLines; i < 50; i++)
		{
			var lineRenderer = lines[i].GetComponent<LineRenderer>();
			Vector3 startsPunkt = this.transform.position;
			Vector3[] positions = new Vector3[] { startsPunkt, startsPunkt };
			lineRenderer.SetPositions(positions);
		}
		setGraph(hitDistances);
    }
	//message functions for sliders
	public void SetAngle()
	{
		upAngle = true;
	}
	public void setNum()
	{
		upCount = true;
	}
	// Draws the line which serves as the graph of the distances
	//    param: the distances of the lines (length should already match numberLines)
	private void setGraph(float[] distances)
	{
		// make the Vector2 list to serve as the UILineRenderer Path
		Vector2[] pointList = new Vector2[numberLines*3 + 2];

		// >counts the actual lines because each line uses three points
		int line = 0;

		// divide the graph into equal parts for the lines
		float increment = 1.0f/ (numberLines+1);

		// We are going to "crawl" along the bottom and draw each spike one at a time
		//     this could have been done in a for loop on 'i,' but HDH feels this reads easier

		foreach(float dist in distances)
		{
			// >ad hoc distance scaling
			float relDist = dist / 70;
			// we marked tranlucent collisions using negative numbers, this catches them
			if (relDist < 0) relDist = randList[line];
			// There is a maximum distance graphable, as determined by relDistance (ad hoc)
			if (relDist > 1) relDist = 1;
			// lines use three points
			int i = line * 3;
			
			// each line uses the same point for first and last point
			pointList[i] = pointList[i+2] = new Vector2(line * increment + increment, 0);

			// the middle point is the one that goes up the input distance(scaled)
			pointList[i + 1] = new Vector2((line) * increment + increment, relDist);

			// this is for the green line, integer rep is useful here
			//    slightly different from above because it only needs two points (not crawling), and it's own renderer
			if(line == greenOne)
			{
				
					greenOneRenderer.Points = new Vector2[] { new Vector2(line * increment + increment, 0), new Vector2((line * increment + increment), relDist) };
			
				
				}
			line++;
		}
		if (numberLines == 0)
		{
			greenOneRenderer.Points = new Vector2[1];
		}
		// finish off by drawing the rest of the bottom (just a bit less awkward since we had to crawl the bottom for the rest of the lines as well)
		pointList[numberLines * 3] = new Vector2(1, 0);
		pointList[numberLines * 3 + 1] = new Vector2(0, 0);

		//  then graph
		graphRenderer.Points = pointList;
	}
	//used by the translucent portions of the script, creates an array of 50 random numbers 
	// within the legnth bounds of the laserscanner every quarter second
	// read from on a line by line basis as needed 
	IEnumerator coRandList()
	{
		while (true)
		{
			for (int i = 0; i < lines.Length; i++)
			{
				randList[i] = Random.value;
			}
			yield return new WaitForSeconds(.25f);
		}
	}
}
