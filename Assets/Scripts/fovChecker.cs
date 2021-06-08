using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class fovChecker : MonoBehaviour
{
    public Material[] materials;

    private bool c_point;
    public Collider[] frustums;
    private Renderer[] _rings;
    private int count;//, _count;

    private int[] seen;
    public Text _text;



    // Start is called before the first frame update
    void Start()
    {
        //gotThemAll = getCameras(false);
        Ring[] rings = GameObject.FindObjectsOfType<Ring>();
        _rings = new Renderer[4];
        foreach(Ring ring in rings)
        {
            _rings[ring.myVal] = ring.gameObject.GetComponent<Renderer>();
        }
        seen = new int[] { 0, 0, 0 };
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        count = seen[0] + seen[1] + seen[2];
        _text.text = "The robot is visible\nby " + count.ToString() + " cameras.";
        for (int i = 0; i < 4; i++)
        {
            _rings[i].enabled = i == count;
        }

    }
    /*bool getCameras(bool checkFirst)
    {
        bool[] marked = { false, false, false };
        if (checkFirst)
        {
            for (int i = 0; i < numCameras; i++)
            {
                if (cameras[i] == null)
                    marked[i] = true;
            }
        }
        else marked = new bool[] { true, true, true};
        for (int i = 0; i < numCameras; i++)
        {
            if(marked[i])// cameras[i] = GameObject.Find(System.String.Format("cam_{0}", i+1)).GetComponent<Camera>();
                cameras[i] = GameObject.FindGameObjectWithTag(System.String.Format("Camera{0}", i + 1)).GetComponent<Camera>();

        }
        for (int i = 0; i < numCameras; i++)
        {
            if (cameras[i] == null)
                return false;
        }
        return true;
    }*/
    void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i < frustums.Length; i++)
        {
            if(other == frustums[i])
            {
                seen[i] = 1;
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < frustums.Length; i++)
        {
            if (other == frustums[i])
            {
                seen[i] = 1;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < frustums.Length; i++)
        {
            if (other == frustums[i])
            {
                seen[i] = 0;
            }
        }
    }
}
