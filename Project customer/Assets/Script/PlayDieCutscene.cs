using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayDieCutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    [SerializeField] int stopTime;
    public Camera carCamera;
    public Camera videoCamera;

    float videoStartTime;
    bool collided;


    void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "deathbycarplaceholder.mp4");
    }

    private void Update()
    {
        if (collided && Time.time - videoStartTime >= stopTime)
        {
            videoPlayer.Stop();
            SceneManager.LoadScene("MainMenu");
        }
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "PlayerCar")
        {
            //idsable player so no more interactions are possible and play cutscene video 
            soundManager.PlaySound("car");
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
