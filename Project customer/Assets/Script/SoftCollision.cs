using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftCollision : MonoBehaviour
{


    public softCrashType softCrashType = new softCrashType();
    public AudioSource softCrashSound;

    [SerializeField] float damageResetTime;

    float crashTime;
    bool collided;

    void Update()
    {
        if (collided && Time.time - crashTime > damageResetTime)
        {
            collided = false;
        }
    }
    void OnCollisionStay(Collision collision)
    {
        
        if (!collided && collision.collider.gameObject.tag == "PlayerCar")
        {
            
            switch (softCrashType)
            {
                case (softCrashType)0 : crashCounter.fireHydrant++;
                    break;
                case (softCrashType)1 : crashCounter.lampPost++;
                    break;
                case (softCrashType)2 : crashCounter.tree++;
                    break;
                case (softCrashType)3 : crashCounter.building++;
                    break;
                case (softCrashType)4 : crashCounter.trafficCone++;
                    break;
                case (softCrashType)5 : crashCounter.trashCan++;
                    break;
            } 
            //softCrashSound.Play();
            crashTime = Time.time;
            collided = true;
        }
    }
}
public enum softCrashType
{
    fireHydrant,
    lampPost,
    tree,
    building,
    trafficCone,
    trashCan
}
