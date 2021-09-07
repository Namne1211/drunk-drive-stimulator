using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playHomeCutscene : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject videoPlayer;
    public int stopTime;
    public Camera carCamera;
    public Camera videoCamera;

    float videoStartTime;
    bool collided;


    void Start()
    {
        videoPlayer.SetActive(false);
    }

    private void Update()
    {
        if (collided && Time.time - videoStartTime >= stopTime-0.5f)
        {
            SceneManager.LoadScene("endScreen");
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
            videoPlayer.SetActive(true);
            //stop video after provided time and load next scene
            Destroy(videoPlayer, stopTime);
            videoStartTime = Time.time;
            collided = true;
        }
    }
}
