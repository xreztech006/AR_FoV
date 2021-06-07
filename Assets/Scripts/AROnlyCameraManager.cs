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
	}

	public void setCam(int num)
	{
		_cam = cameras[num];
	}

}
