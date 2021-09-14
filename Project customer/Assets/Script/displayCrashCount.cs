using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayCrashCount : MonoBehaviour
{
    public Text fireHydrantsText;
    public Text trafficConeText;
    public Text buildingText;
    public Text trashText;
    public Text treeText;
    public Text lampText;

    void Start()
    {
        fireHydrantsText.text = "" + crashCounter.fireHydrant;
        trafficConeText.text = "" + crashCounter.trafficCone;
        buildingText.text = "" + crashCounter.building;
        trashText.text = "" + crashCounter.trashCan;
        treeText.text = "" + crashCounter.tree;
        lampText.text = "" + crashCounter.lampPost;

    }
}
