using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleManager : MonoBehaviour
{
    int numObstacles;
    int tracker;
    // Start is called before the first frame update
    void Start()
    {
        numObstacles = this.transform.childCount;
        tracker = 1;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < numObstacles; i++)
        {
            this.transform.GetChild(i).transform.gameObject.SetActive((i == tracker) ? true : false);
            
        }
    }
    public void toggleObstacle()
    {
        tracker = (tracker + 1 )% (numObstacles );
    }
}
