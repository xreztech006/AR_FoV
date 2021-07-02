//==========================================
// Title:  AROnlyCameraManager
// Author: HDH : MH
// Date:   24 Jun 2021
//==========================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AROnlyCameraManager : MonoBehaviour
{
	
	[Tooltip("Make sure 0 is the bot, and 1-3 are respective cameras")]
	public Camera[] cameras;
	// current and update camera
	private Camera cam, _cam;
    
    void Start()
    {
        foreach (Camera cam in cameras){
			cam.targetTexture = null;
		}
		cam = _cam = cameras[0];
    }

    // Update is called once per frame
    void Update()
    {
		// check for touches, if it is a camera sphere, set the update cam var to the right camera
		// foreach (Touch touch in Input.touches)
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, (1 << LayerMask.NameToLayer("fov"))))
			{
				Debug.Log("touched " + hit.collider.name);
				switch (hit.collider.name)
				{
					case "Camera1":
						_cam = cameras[1];
						break;
					case "Camera2":
						_cam = cameras[2];
						break;
					case "Camera3":
						_cam = cameras[3];
						break;
					case "Camera0":
						_cam = cameras[0];
						break;
				}

			}
			
		}
		//if it changed, switch to that camera (this works with the menu function as well)
		if (_cam != cam)
		{
			_cam.enabled = true;
			cam.enabled = false;
			cam = _cam;
		}
	}

	//the UI uses this script to change the update cam variable to be caught in the update loop
	public void setCam(int num)
	{
		_cam = cameras[num];
	}
	
}
