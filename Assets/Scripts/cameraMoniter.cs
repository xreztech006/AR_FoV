using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMoniter : MonoBehaviour
{
    public cameraConstraint _cam1;
    public cameraConstraint _cam2;
    public cameraConstraint _cam3;
    // Start is called before the first frame update
    public void acam1()
    {
        _cam1._tracking = true;
    }
    public void dcam1()
    {
        _cam1._tracking = false;
    }
    public void acam2()
    {
        _cam2._tracking = true;
    }
    public void dcam2()
    {
        _cam2._tracking = false;
    }
    public void acam3()
    {
        _cam3._tracking = true;
    }
    public void dcam3()
    {
        _cam3._tracking = false;
    }
}
