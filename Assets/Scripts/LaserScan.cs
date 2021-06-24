//==========================================
// Title:  LaserScan
// Author: HDH : MH, OC
// Date:   24 Jun 2021
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
	[Tooltip("The sphere is both a visual element and a way to draw a striaght line from the robot-out")]
	public GameObject target;
	// combo slider/values to give users access to laser count and angle values
	public Slider count;
	public Slider setAngle;
	private int numberLines;
	private float angle;
	// menu adjustable defaults removed for now, left if needed
	//private string laserskey = "Lasers";
	//private string anglekey = "Angle";
	private int defaultLasers = 15;
	private float defaultAngle = 230.0f;
	private GameObject[] lines;
	bool upCount, upAngle, lateStart = false;

	void Start()
	{
		upCount = upAngle = lateStart = true;

		// menu adjustable defaults removed for now, left if needed
		//numberLines =  PlayerPrefs.GetInt(laserskey, defaultLasers);
		//angle = PlayerPrefs.GetFloat(anglekey, defaultAngle);
		numberLines = defaultLasers;
		angle = defaultAngle; 

		lines = new GameObject[50]; // there are always 50 lasers ready, we just shrink the ones we don't use
		for (int i = 0; i < lines.Length; i++)
		{
			lines[i] = Instantiate(linePrefab);
			lines[i].transform.parent = this.transform;
		}
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
		 // These cont.
		if (upCount)
		{
			numberLines = (int)count.value;
			upCount = false;
		}

		// Bit shift the index of the layer (8) to get a bit mask
		int layerMask = 1 << LayerMask.NameToLayer("walls") ;

        // This would cast rays only against colliders in layer 8.
		
		// angles only work because 0 is in the center, and we move from the positive halfway point to the negative for symmetry
		float currangle = angle/2;
        float angleincrement = angle / (numberLines);
		for (int i =0; i < numberLines; i++) {
			var lineRenderer = lines[i].GetComponent<LineRenderer>();
			Vector3 direction = Quaternion.Euler(0, -currangle+.0001f, 0) * new Vector3((transform.position - target.transform.position).x, 0, (transform.position - target.transform.position).z);
			Vector3 startsPunkt = this.transform.position; // new Vector3(transform.position.x-xoffset, transform.position.y - ydecrement, transform.position.z - zoffset); 
			
			RaycastHit hit;
			// Does the ray intersect any objects excluding the player layer
			if (Physics.Raycast(startsPunkt, direction, out hit, Mathf.Infinity, layerMask))
			{
				// construct a line in real space from the laserscanner to the hit point
				Vector3[] positions = new Vector3[]{startsPunkt, hit.point};
				lineRenderer.SetPositions(positions);

				//Debug.Log(hit.distance);
				//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
				//Debug.Log("Did Hit");

				//=======WIP=======HDH=======Refraction test
				if (hit.collider.gameObject.name == "TranslucentDoorCollider")
				{
					//Random.seed = (int)(hit.point.x + hit.point.y + hit.point.z);
					Random.InitState(i);
					Vector3 bounceDirection = Random.insideUnitCircle.normalized;
					Vector3[] bounce = new Vector3[] { hit.point, bounceDirection };
					lineRenderer.SetPositions(bounce);
				}
				//=======WIP=======HDH=======
			}

			// else constuct a line from the laserscanner-out, the maximum distance
			// BUGGIE
			//   Note this should not happen because the colliders outside of the hospital
			//		Left for posterity
			else
			{
				
				Vector3[] positions = new Vector3[]{startsPunkt, new Vector3 (direction.x * linedistance, this.transform.position.y, direction.z * linedistance) };
				lineRenderer.SetPositions(positions);

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
}
