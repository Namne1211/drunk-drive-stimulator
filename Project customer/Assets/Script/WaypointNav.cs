using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNav : MonoBehaviour
{

    AIcar car;
    public WayPoint currentWaypoint;

    private void Awake()
    {
        car = GetComponent<AIcar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        car.SetDestination(currentWaypoint.GetPostion());
    }

    // Update is called once per frame
    void Update()
    {
        if (car.reachedDestination)
        {
            currentWaypoint = currentWaypoint.nextWayPoint;
            car.SetDestination(currentWaypoint.GetPostion());
        }
    }
}
