using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testCollision : MonoBehaviour
{
    public Text hud;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hud.text = ""+crashCounter.fireHydrant;
    }
}
