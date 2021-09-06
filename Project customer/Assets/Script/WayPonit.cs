using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public WayPoint prevWayPoint;
    public WayPoint nextWayPoint;

    public Vector3 GetPostion()
    {
        return this.transform.position;
    }
}
