using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayBarCutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    float videoStartTime;
    bool collided;
    public Camera carCamera;
    public Camera videoCamera;


    void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "barCutScene.mp4");
        videoPlayer.Prepare();
    }

    private void Update()
    {
        if (collided && !videoPlayer.isPlaying)
        {
            //videoPlayer.Stop();
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
            collided = true;
        }
    }

}
