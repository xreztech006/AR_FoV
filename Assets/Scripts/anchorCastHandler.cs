//==========================================
// Title:  anchorCastHandler
// Author: HDH
// Date:   24 Jun 2021
//==========================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anchorCastHandler : MonoBehaviour
{
	[Tooltip("the object to be placed by the anchor cast")]
	public GameObject vessel;
	[Tooltip("the anchor layer should only have a flat mesh collider")]
	public string layerName;
	[Tooltip("the default height should be the same height as the anchor layer")]
	public float defaultHeight = 5.300132f;


    void Update()
    {
		Vector3 startsPunkt = Camera.main.transform.position;
		Vector3 direction = startsPunkt - this.transform.position; //relative to the camera
		
		RaycastHit hit;
		// Does the ray intersect any objects excluding the player layer
		if (Physics.Raycast(startsPunkt, direction, out hit, Mathf.Infinity,  1 >> LayerMask.NameToLayer(layerName)))
		{

			vessel.transform.position = hit.point; //--put the vessel object at the place the raycast hits
		}
		else vessel.transform.position = new Vector3(this.transform.position.x,
													defaultHeight,
													this.transform.position.z);
			//--else put it where this is but at the right height

		var _angle = this.transform.rotation.eulerAngles.y;
		vessel.transform.rotation = Quaternion.Euler(0, _angle, 0);
		//--then lock it's rotation to the Y axis
	}
}
