using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayDieCutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Camera carCamera;
    public Camera videoCamera;

    bool collided;


    void Start()
    {
        if(SceneManager.GetActiveScene().name == "driveToBar")
        {
            videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "carcrash1.mp4");
        }
        else if (SceneManager.GetActiveScene().name == "driveHome")
        {
            videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "carcrash2.mp4");
        }

        videoPlayer.Prepare();
        
    }

    private void Update()
    {
        if (collided && !videoPlayer.isPlaying)
        {
            //videoPlayer.Stop();
            SceneManager.LoadScene("MainMenu");
        }
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "PlayerCar")
        {
            //idsable player so no more interactions are possible and play cutscene video 
            player.enabled = false;
            videoCamera.gameObject.SetActive(true);
            carCamera.gameObject.SetActive(false);
            videoPlayer.Play();
            collided = true;
        }
    }
}
