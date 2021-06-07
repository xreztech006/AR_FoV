using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserConstriant : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(0.0f, q.eulerAngles.y, 0.0f);
        transform.rotation = q;
        Vector3 pos = transform.position;
        pos = new Vector3(pos.x, 0.0f, pos.z);
    }
}
