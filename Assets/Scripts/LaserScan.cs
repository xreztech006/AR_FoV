using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LaserScan : MonoBehaviour
{
	public GameObject linePrefab;
	// makes the lasers slightly lower than the green dot
	public float ydecrement;
	public float xoffset;
	public float zoffset;
	// distance to draw lines when they don't hit a wall
	public float linedistance;
	public GameObject target;
	public Slider count;
	public Slider setAngle;
	private int numberLines;
	private float angle;
	private string laserskey = "Lasers";
	private string anglekey = "Angle";
	private int defaultLasers = 15;
	private float defaultAngle = 230.0f;
	private GameObject[] lines;
	bool upCount, upAngle, lateStart = false;

	void Start()
	{
		upCount = upAngle = lateStart = true;
		numberLines =  PlayerPrefs.GetInt(laserskey, defaultLasers);
		angle = PlayerPrefs.GetFloat(anglekey, defaultAngle);
		lines = new GameObject[50];
		for (int i = 0; i < lines.Length; i++)
		{
			lines[i] = Instantiate(linePrefab);
			lines[i].transform.parent = this.transform;
		}
	}
    void FixedUpdate()
    {
        if (lateStart)
        {
			gameObject.transform.localRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
			lateStart = false;
        }
		if (upAngle)
		{
			angle = setAngle.value;
			upAngle = false;
		}
		if (upCount)
		{
			numberLines = (int)count.value;
			upCount = false;
		}
		// Bit shift the index of the layer (8) to get a bit mask
		int layerMask = 1 << LayerMask.NameToLayer("walls") ;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;
		
		float currangle = angle/2;
        float angleincrement = angle / (numberLines-1);
		var tlineRenderer = lines[0].GetComponent<LineRenderer>();
		tlineRenderer.SetPositions(new Vector3[] { this.transform.position, new Vector3((transform.position - target.transform.position).x, 0, (transform.position - target.transform.position).z) * linedistance });
		for (int i =1; i < numberLines; i++) {
			var lineRenderer = lines[i].GetComponent<LineRenderer>();
			Vector3 direction = Quaternion.Euler(0, -currangle+.01f, 0) * new Vector3((transform.position - target.transform.position).x, 0, (transform.position - target.transform.position).z);
			Vector3 startsPunkt = this.transform.position; // new Vector3(transform.position.x-xoffset, transform.position.y - ydecrement, transform.position.z - zoffset); 
			
			RaycastHit hit;
			// Does the ray intersect any objects excluding the player layer
			if (Physics.Raycast(startsPunkt, direction, out hit, Mathf.Infinity, layerMask))
			{

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
			else
			{
				Vector3[] positions = new Vector3[]{startsPunkt, direction*linedistance};
				lineRenderer.SetPositions(positions);
				
				//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
				//Debug.Log("Did not Hit");
			}
			
			currangle -= angleincrement;
		}
		for (int i = numberLines; i < 50; i++)
		{
			var lineRenderer = lines[i].GetComponent<LineRenderer>();
			Vector3 startsPunkt = this.transform.position;
			Vector3[] positions = new Vector3[] { startsPunkt, startsPunkt };
			lineRenderer.SetPositions(positions);
		}
    }
	public void SetAngle()
	{
		upAngle = true;
	}
	public void setNum()
	{
		upCount = true;
	}
}
