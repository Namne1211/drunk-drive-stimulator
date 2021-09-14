using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Canvas buttons;
    public int stopTime;
    bool clicked;
    float videoStartTime;
    
    void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "popup.mp4");
    }
    public void Update()
    {
        if (clicked && Time.time - videoStartTime >= stopTime)
        {
            SceneManager.LoadScene("driveToBar");
        }
    }
    public void loadDriveToBar()
    {
        if(!clicked) videoStartTime = Time.time;
        clicked = true;
        buttons.enabled = false;
        videoPlayer.Play();
        crashCounter.fireHydrant = 0;
        crashCounter.lampPost = 0;
        crashCounter.tree = 0;
        crashCounter.building = 0;
        crashCounter.trafficCone = 0;
        crashCounter.trashCan = 0;      
    }


    public void closeGame()
    {
        Application.Quit();
    }
    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
