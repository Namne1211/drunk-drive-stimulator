using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WayPointManager : EditorWindow
{
    [MenuItem("Tools/WayPointEditort")]
    public static void Open()
    {
        //open window
        GetWindow<WayPointManager>();
    }

    public Transform waypointRoot;

    private void OnGUI()
    {

        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        //check if there is a root for way point or not
        if(waypointRoot == null)
        {
            EditorGUILayout.HelpBox("please assign a root transform", MessageType.Warning);
        }
        else
        {

            EditorGUILayout.BeginVertical("box");
            DrawButton();
            EditorGUILayout.EndVertical();
        }
        obj.ApplyModifiedProperties();

    }

    //handle button

    void DrawButton()
    {
        if(GUILayout.Button("create Waypoint"))
        {
            CreateWaypoint();
        }
        if(Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<WayPoint>())
        {
           
            if (GUILayout.Button("create Waypoint before"))
            {
                CreateWaypointBefore();
            }
            if (GUILayout.Button("create Waypoint after"))
            {
                CreateWaypointAfter();
            }
            if (GUILayout.Button("remove Waypoint"))
            {
                RemoveWaypoint();
            }
            if (GUILayout.Button("Add branch "))
            {
                AddBranch();
            }
        }

    }

    void CreateWaypoint()
    {
        //create new child with way point component
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        WayPoint waypoint = waypointObject.GetComponent<WayPoint>();

        WayPoint From = Selection.activeGameObject.GetComponent<WayPoint>();
        if (waypointRoot.childCount > 1)
        {
            //st the waypoint prevwaypoint
            waypoint.prevWayPoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<WayPoint>();
            waypoint.prevWayPoint.nextWayPoint = waypoint;
            //palace the waypoint at the last position
            waypoint.transform.position = waypoint.prevWayPoint.transform.position;
            waypoint.transform.forward = waypoint.prevWayPoint.transform.forward;
        }
        Selection.activeGameObject = waypoint.gameObject;
            
    }

    void CreateWaypointBefore()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        WayPoint newWaypoint = waypointObject.GetComponent<WayPoint>();

        WayPoint selectedWaypoint = Selection.activeGameObject.GetComponent<WayPoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        if (selectedWaypoint.prevWayPoint != null)
        {
            newWaypoint.prevWayPoint = selectedWaypoint.prevWayPoint;
            selectedWaypoint.prevWayPoint.nextWayPoint = newWaypoint;
        }

        newWaypoint.nextWayPoint = selectedWaypoint;

        selectedWaypoint.prevWayPoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void CreateWaypointAfter()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        WayPoint newWaypoint = waypointObject.GetComponent<WayPoint>();

        WayPoint selectedWaypoint = Selection.activeGameObject.GetComponent<WayPoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        if (selectedWaypoint.nextWayPoint != null)
        {
            newWaypoint.nextWayPoint = selectedWaypoint.nextWayPoint;
            selectedWaypoint.nextWayPoint.prevWayPoint = newWaypoint;
        }

        selectedWaypoint.nextWayPoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void RemoveWaypoint()
    {
        WayPoint selectedWaypoint = Selection.activeGameObject.GetComponent<WayPoint>();

        if(selectedWaypoint.nextWayPoint != null)
        {
            selectedWaypoint.nextWayPoint.prevWayPoint = selectedWaypoint.prevWayPoint;
        }
        if (selectedWaypoint.prevWayPoint != null)
        {
            selectedWaypoint.prevWayPoint.nextWayPoint = selectedWaypoint.prevWayPoint;
            Selection.activeGameObject = selectedWaypoint.prevWayPoint.gameObject;
        }

        DestroyImmediate(selectedWaypoint.gameObject);
    }

    void AddBranch()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        WayPoint waypoint = waypointObject.GetComponent<WayPoint>();

        WayPoint branchedFrom = Selection.activeGameObject.GetComponent<WayPoint>();
        branchedFrom.branches.Add(waypoint);

        waypoint.transform.position = branchedFrom.transform.position;
        waypoint.transform.forward = branchedFrom.transform.forward;

        Selection.activeGameObject = waypoint.gameObject;
    }
}
