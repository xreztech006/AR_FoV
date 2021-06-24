//==========================================
// Title:  fovChecker
// Author: HDH
// Date:   24 Jun 2021
//==========================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class fovChecker : MonoBehaviour
{
    [Tooltip("0: Blue, 1: Green, 2: Yellow, 3: Red")]
    public Material[] materials;
    [Tooltip("0: Yellow, 1: Blue, 2: Red (not as imposrtant as the colours)")]
    public Collider[] frustums;
    // we will only need to turn the rings on and off, no need to keep more than the renderers
    private Renderer[] _rings;
    private int count;
    // the integer array allows each camera it's own variable to adjust, couting is done in update
    private int[] seen;
    // text to display count on
    public Text _text;

    void Start()
    {
        // rings have a script on them which is just a named class with a public int called myVal
        Ring[] rings = GameObject.FindObjectsOfType<Ring>();
        _rings = new Renderer[4];
        foreach(Ring ring in rings)
        {
            // myVal means you can find the rings easy and also name them in the editor
            _rings[ring.myVal] = ring.gameObject.GetComponent<Renderer>();
        }
        seen = new int[] { 0, 0, 0 };
    }

    void FixedUpdate()
    {

        count = seen[0] + seen[1] + seen[2];
        _text.text = "The robot is visible\nby " + count.ToString() + " cameras.";
        for (int i = 0; i < 4; i++)
        {
            // only the ring with the matching count should be on
            _rings[i].enabled = i == count;
        }

    }
    // set the count by checking how many of the frustrums are currently collided with using all three of these
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
