using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraCompassRose : MonoBehaviour
{
    private Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = FindObjectOfType<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = System.String.Format("Rotation (X: {0}, Y: {1}, Z: {2}", Camera.main.transform.rotation.x, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z);
    }
}
