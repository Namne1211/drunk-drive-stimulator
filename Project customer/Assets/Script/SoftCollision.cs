using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftCollision : MonoBehaviour
{


    public softCrashType softCrashType = new softCrashType();

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
                case softCrashType.fireHydrant : crashCounter.fireHydrant++; 
                    soundManager.PlaySound("firehydrant");
                    break;
                case softCrashType.lampPost : crashCounter.lampPost++;
                    soundManager.PlaySound("lamp");
                    break;
                case softCrashType.tree : crashCounter.tree++;
                    soundManager.PlaySound("tree");
                    break;
                case softCrashType.building : crashCounter.building++;
                    soundManager.PlaySound("building");
                    break;
                case softCrashType.trafficCone : crashCounter.trafficCone++;
                    soundManager.PlaySound("trafficcone");
                    break;
                case softCrashType.trashCan : crashCounter.trashCan++;
                    soundManager.PlaySound("trashcan");
                    break;
            } 
            
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
