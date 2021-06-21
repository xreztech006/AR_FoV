using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anchorCastHandler : MonoBehaviour
{
	public GameObject vessel;
	public string layerName;
	public float defaultHeight = 5.300132f;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 startsPunkt = Camera.main.transform.position;
		Vector3 direction = startsPunkt - this.transform.position;
		 // new Vector3(transform.position.x-xoffset, transform.position.y - ydecrement, transform.position.z - zoffset); 

		RaycastHit hit;
		// Does the ray intersect any objects excluding the player layer
		if (Physics.Raycast(startsPunkt, direction, out hit, Mathf.Infinity, LayerMask.NameToLayer(layerName)))
		{

			vessel.transform.position = hit.point;
		}
		else vessel.transform.position = new Vector3(this.transform.position.x,
													defaultHeight,
													this.transform.position.z);
		vessel.transform.rotation = this.transform.rotation;

	}
}
