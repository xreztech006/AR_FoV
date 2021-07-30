//==========================================
// Title:  fovChecker
// Author: HDH
// Date:   24 Jul 2021
//==========================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// Lives on the bot, somewhere. checks when the bot (this script's object itself) is within 
///  camera view frustrums, and whether or not line of sight is blocked, then displays that info
///  in both the text on the UI and by enabling the proper ring object beneath the bot
/// </summary>
public class fovChecker : MonoBehaviour
{
    [Tooltip("0: Blue, 1: Green, 2: Yellow, 3: Red")]
    public Material[] materials;
    [Tooltip("0: Yellow, 1: Blue, 2: Red (not as imposrtant as the ring colours)")]
    public Collider[] frustums;
    // we will only need to turn the rings on and off, no need to keep more than the renderers
    private Renderer[] _rings;
    private int count;
    // the integer array allows each camera it's own variable to adjust, couting is done in update
    private int[] seen;
    // text to display count on
    public Text _text;

    //start creates an array of renderers and ints
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
        //given all collision which are noted from the trigger message-handlers, make sure they have line of sight
        for (int i = 0; i < 3; i++)
        {
            if (seen[i] > 0 && wallCheckCast(frustums[i].transform.parent.gameObject)) seen[i] = 0;
        }

        //now that we are sure which are actually seen, get a count
        count = seen[0] + seen[1] + seen[2];

        //update the text
        _text.text = "The robot is visible\nby " + count.ToString() + " cameras.";

        //and then the rings
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
    /// <summary>
    /// This function checks to make sure that line of sight for a given camera (by it's frustum) is not blocked by a wall
    /// </summary>
    /// <param name="frustum"></param>
    /// <returns></returns>
    bool wallCheckCast(GameObject frustum)
    {
        Vector3 _frustum = frustum.transform.position;
        Vector3 direction = _frustum - this.transform.position ;
        int layerMask = 1 << LayerMask.NameToLayer("walls");
        //layerMask    |= 1 << LayerMask.NameToLayer("_frustum");
        //layerMask = ~layerMask;
        RaycastHit hit;
        // return Physics.Raycast(this.transform.position, direction, out hit, direction.magnitude, layerMask) ? false : true;
        if (Physics.Raycast(this.transform.position, direction, out hit, direction.magnitude, layerMask))
        {
            return hit.distance != direction.magnitude ? true : false;
        }
        else return false;

    }
}
