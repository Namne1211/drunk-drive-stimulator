using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayCrashCount : MonoBehaviour
{
    public Text displayText;
    void Start()
    {
        displayText.text = "Fire hydrants hit: " + crashCounter.fireHydrant;
    }
}
