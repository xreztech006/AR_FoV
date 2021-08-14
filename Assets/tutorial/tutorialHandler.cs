//==========================================
// Title:  tutorialHandler
// Author: HDH via https://forum.unity.com/threads/solved-how-to-change-scene-after-video-has-ended.753563/
// Date:   01 Aug 2021
//==========================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class tutorialHandler : MonoBehaviour
{
     VideoPlayer video;
    //add the scene switcher to the loop point handler
    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CheckOver;


    }
    //this checks if the screen has been touched to prematurely end the video
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }
    }
    //the script to add to the loop point handler, switched back to the default scene
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(0);//the scene that you want to load after the video has ended.
    }
}
