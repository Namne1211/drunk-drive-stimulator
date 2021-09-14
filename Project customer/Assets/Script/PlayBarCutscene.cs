using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayBarCutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public int stopTime;
    float videoStartTime;
    bool collided;
    public Camera carCamera;
    public Camera videoCamera;


    void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "barCutScene.mp4");
    }

    private void Update()
    {
        if (collided && Time.time - videoStartTime >= stopTime)
        {
            SceneManager.LoadScene("driveHome");
        }
    }

    void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "PlayerCar")
        {
            //idsable player so no more interactions are possible, swithc camera, and play cutscene video 
            player.enabled = false;
            videoCamera.gameObject.SetActive(true);
            carCamera.gameObject.SetActive(false);
            
            videoPlayer.Play();
            //stop video after provided time and load next scene
            Destroy(videoPlayer, stopTime);
            videoStartTime = Time.time;
            collided = true;
        }
    }

}
