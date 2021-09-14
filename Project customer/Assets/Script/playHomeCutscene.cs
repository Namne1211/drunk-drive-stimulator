using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playHomeCutscene : MonoBehaviour
{
    
   

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "PlayerCar")
        {
            SceneManager.LoadScene("endScreen");
        }
    }
}
