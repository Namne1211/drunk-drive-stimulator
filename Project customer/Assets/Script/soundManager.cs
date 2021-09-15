using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static AudioClip hitBuilding, hitFireHydrant, hitTrashcan, hitTree, hitLamp, hitTrafficCone, hitCar;
    static AudioSource audioSrc;
    void Start()
    {
        hitBuilding = Resources.Load<AudioClip>("hitBuilding");
        hitFireHydrant = Resources.Load<AudioClip>("hitFireHydrant");
        hitTrashcan = Resources.Load<AudioClip>("hitTrashCan");
        hitTree = Resources.Load<AudioClip>("hitTree");
        hitLamp = Resources.Load<AudioClip>("hitLamp");
        hitTrafficCone = Resources.Load<AudioClip>("hitTrafficCone");
        hitCar = Resources.Load<AudioClip>("hitCar");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "building":
                audioSrc.PlayOneShot(hitBuilding);
                break;
            case "firehydrant":
                audioSrc.PlayOneShot(hitFireHydrant);
                break;
            case "trashcan":
                audioSrc.PlayOneShot(hitTrashcan);
                break;
            case "tree":
                audioSrc.PlayOneShot(hitTree);
                break;
            case "lamp":
                audioSrc.PlayOneShot(hitLamp);
                break;
            case "trafficcone":
                audioSrc.PlayOneShot(hitTrafficCone);
                break;
            case "car":
                audioSrc.PlayOneShot(hitCar);
                break;
        }
    }
    void Update()
    {
        
    }

}
