using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AROnlyCameraManager : MonoBehaviour
{
	public RenderTexture _active;
	[Tooltip("Make sure 0 is the bot, and 1-3 are respective cameras")]
	public Camera[] cameras;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Camera cam in cameras){
			cam.targetTexture = null;
		}
    }

    // Update is called once per frame
    void Update()
    {
		foreach (Touch touch in Input.touches)
		{
			Ray ray = Camera.main.ScreenPointToRay(touch.position);

			if (touch.phase == TouchPhase.Began)
			{
				RaycastHit hit = new RaycastHit();
				if (Physics.Raycast(ray, out hit, 1000))
				{
					Debug.Log("touched " + hit.transform.name);
					/*switch (hit.transform.name)
					{
						case "Camera1":
							foreach (Camera cam in cameras)
							{
								cam.targetTexture = null;
							}
							cameras[1].targetTexture = _active;
							break;
						case "Camera2":
							foreach (Camera cam in cameras)
							{
								cam.targetTexture = null;
							}
							cameras[2].targetTexture = _active;
							break;
						case "Camera3":
							foreach (Camera cam in cameras)
							{
								cam.targetTexture = null;
							}
							cameras[3].targetTexture = _active;
							break;
						case "RobotTargetCylinder":
							foreach (Camera cam in cameras)
							{
								cam.targetTexture = null;
							}
							cameras[0].targetTexture = _active;
							break;
					}*/
					switch (hit.transform.tag)
					{
						case "Camera1":
							foreach (Camera cam in cameras)
							{
								cam.targetTexture = null;
							}
							cameras[1].targetTexture = _active;
							break;
						case "Camera2":
							foreach (Camera cam in cameras)
							{
								cam.targetTexture = null;
							}
							cameras[2].targetTexture = _active;
							break;
						case "Camera3":
							foreach (Camera cam in cameras)
							{
								cam.targetTexture = null;
							}
							cameras[3].targetTexture = _active;
							break;
						case "Cylinder":
							foreach (Camera cam in cameras)
							{
								cam.targetTexture = null;
							}
							cameras[0].targetTexture = _active;
							break;
					}

				}
			}
		}
	}
}
