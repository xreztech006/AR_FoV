using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleManager : MonoBehaviour
{
    Renderer[] obstacles;
    int tracker;
    // Start is called before the first frame update
    void Start()
    {
        obstacles = transform.GetComponentsInChildren<Renderer>();
        tracker = 1;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 1; i < 4; i++)
        {
            obstacles[i].enabled = (i == tracker + 1) ? true : false;

        }
    }
    public void toggleObstacle()
    {
        tracker = (tracker + 1 )% (obstacles.Length );
    }
}
