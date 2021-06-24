// HDH 6/2021
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AROnlyCameraManager : MonoBehaviour
{
	//public RenderTexture _active;
	[Tooltip("Make sure 0 is the bot, and 1-3 are respective cameras")]
	public Camera[] cameras;
	private Camera cam, _cam;
    // Start is called before the first frame update
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
		if (_cam != cam)
		{
			_cam.enabled = true;
			cam.enabled = false;
			cam = _cam;
		}
		foreach (Touch touch in Input.touches)
		{
			Ray ray = Camera.main.ScreenPointToRay(touch.position);

			if (touch.phase == TouchPhase.Began)
			{
				RaycastHit hit = new RaycastHit();
				if (Physics.Raycast(ray, out hit, 1000, LayerMask.NameToLayer("fov")))
				{
					Debug.Log("touched " + hit.transform.name);
					switch (hit.transform.name)
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
						case "RobotTargetCylinder":
							_cam = cameras[0];
							break;
					}

				}
			}
		}
	}

	public void setCam(int num)
	{
		_cam = cameras[num];
	}

}
