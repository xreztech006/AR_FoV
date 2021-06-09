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
        transform.localPosition = new Vector3(transform.localPosition.x, 0.01489996f, transform.localPosition.z);

    }
}
