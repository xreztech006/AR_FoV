using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 MyPos;
    //public Vector3 MyScale;

    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
      MyPos = transform.position;
      //MyScale = transform.localScale;
    }
}
