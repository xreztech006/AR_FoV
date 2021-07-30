//==========================================
// Title:  obstacleManager
// Author: HDH
// Date:   30 Jul 2021
//==========================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Message and tracker for cycling between obstacles - would work with any child setup
/// </summary>
public class obstacleManager : MonoBehaviour
{
    int numObstacles;
    int tracker;
    bool toggled;
    
    /// <summary>
    /// Use Start to populate initial values. Reading from children allows the list of objcts to be arbitrary
    /// We make the toggle true at the start to ensure only one object is showing on load
    /// </summary>
    void Start()
    {
        toggled = true;
        numObstacles = this.transform.childCount;
        tracker = 1;
    }

    /// <summary>
    /// Every 
    /// </summary>
    void Update()
    {
        if (toggled) // this line simply prevents the for loop from running every frame
        {
            for (int i = 0; i < numObstacles; i++)
            {
                this.transform.GetChild(i).transform.gameObject.SetActive((i == tracker) ? true : false);
                //only enable the "current" object
            }
            toggled = false; // reset
        }
    }
    /// <summary>
    /// This is the function to call from the button, it will tick the tracker and confine it to the range of number of obstacles
    /// | Also flips the boolean so update knows the change it
    /// </summary>
    public void toggleObstacle()
    {
        tracker = (tracker + 1 )% (numObstacles );
        toggled = true;
    }
}
