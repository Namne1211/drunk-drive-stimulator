using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public WayPoint prevWayPoint;
    public WayPoint nextWayPoint;

    public List<WayPoint> branches = new List<WayPoint>();
    //ratio on the percentage of branching
    [Range(0f, 1f)]
    public float branchRatio = 0.5f;

    public Vector3 GetPostion()
    {
        return this.transform.position;
    }
}
