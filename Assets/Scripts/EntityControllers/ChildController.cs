using UnityEngine;
using System.Collections;
using System;

public class ChildController : EntityControllers
{

    private Transform waypointNode;
    private Transform waypointAnchor;
    
    public static float waypointRadius = 1;
    public static float xMin = 0;
    public static float xMax = 10;
    public static float yMin = 0;
    public static float yMax = 10;

    Vector2 waypoint = new Vector2(0, 0);

    void Start()
    {
        base.Start();
        waypointAnchor = GameObject.Find("WaypointAnchor").transform;
        GetNewWaypoint();
    }

    protected override Vector2 ComputeAdditionalForces()
    {
        if ((waypoint - position).magnitude < waypointRadius)
        {
            GetNewWaypoint();
        }

        var force = waypoint - position;

        if (force.x != 0 || force.y != 0)
        {
            force = force.normalized;
        }
        force *= strength;
        return force;
    }

    void GetNewWaypoint()
    {
        if (waypointNode != null)
        {
            waypointNode =
                waypointNode.gameObject.GetComponent<ChildWaypointNodeScript>().GetRandomConnectedNode().transform;
        }
        else
        {
            int randomNode = UnityEngine.Random.Range(0, waypointAnchor.childCount);
            waypointNode = waypointAnchor.GetChild(randomNode).transform;
        }

        waypoint = waypointNode.transform.position;
    }

    protected override void UpdateSprite(float angle)
    {
    }

    protected override void UpdateLogic() {}
}
