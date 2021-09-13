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
            bool shouldBranch = false;
            
            //check random if should branch or not
            if(currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
            {
                shouldBranch = Random.Range(0f, 1f)<=currentWaypoint.branchRatio ? true :false;
            }
            //if branches go
            if (shouldBranch)
            {
                currentWaypoint = currentWaypoint.branches[(Random.Range(0, currentWaypoint.branches.Count - 1))];
            }else {
                //check if there is a way or not
                if(currentWaypoint.nextWayPoint != null)
                {
                    currentWaypoint = currentWaypoint.nextWayPoint;
                    car.SetDestination(currentWaypoint.GetPostion());
                }
                else
                {
                    currentWaypoint = currentWaypoint.prevWayPoint;
                    car.SetDestination(currentWaypoint.GetPostion());
                }
            }
            
            
        }
    }
}
