using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class waypointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected|GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(WayPoint waypoint,GizmoType gizmoType)
    {
        if((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.yellow * 0.5f;
        }
        //draw sphere on waypoint
        Gizmos.DrawSphere(waypoint.transform.position, 1f);

        Gizmos.color = Color.white;

        //drawline connected
        if(waypoint.prevWayPoint != null)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawLine(waypoint.transform.position, waypoint.prevWayPoint.transform.position);
        }
        if(waypoint.nextWayPoint != null)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawLine(waypoint.transform.position, waypoint.nextWayPoint.transform.position);
        }
    }
}
