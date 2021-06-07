using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraConstraint : MonoBehaviour
{
    private Camera _cam;
    public GameObject _tracked; // public because there are three different tracked fiducials this script needs to aplpy to
    public float _height; // Public because it might change
    public bool _tracking;
    void Update()
    {
        if (_tracking){
            // length of vector from camera center to proper camera height
            float _depth = (_cam.transform.position.y - _height) / Mathf.Cos(_cam.transform.localRotation.x);

            // 2D viewport location of fiducial 
            Vector2 _vp = _cam.WorldToViewportPoint(_tracked.transform.position);

            // Where that point should be given the corrected depth
            this.transform.position = _cam.ViewportToWorldPoint(new Vector3(_vp.x, _vp.y, _depth));

            // Still gotta match the rotation
            this.transform.rotation = _tracked.transform.rotation;
        }

    }
        // Start is called before the first frame update
    void Start()
    {
        // get that camera
        _cam = Camera.main;
    }


}
