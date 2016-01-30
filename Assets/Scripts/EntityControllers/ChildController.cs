using UnityEngine;
using System.Collections;
using System;

public class ChildController : EntityControllers
{
    
    public static float waypointRadius = 1;
    public static float xMin = 0;
    public static float xMax = 10;
    public static float yMin = 0;
    public static float yMax = 10;

    Vector2 waypoint = new Vector2(0, 0);

    void Start()
    {
        waypoint = getNewWaypoint();
    }

    protected override Vector2 UpdateForce()
    {
        if((waypoint - position).magnitude < waypointRadius)
        {
            waypoint = getNewWaypoint();
        }

        var force = waypoint - position;

        if (force.x != 0 || force.y != 0)
        {
            force = force.normalized;
        }
        return force;
    }

    Vector2 getNewWaypoint()
    {
        var x = xMin + (xMax - xMin) * UnityEngine.Random.value;
        var y = yMin + (yMax - yMin) * UnityEngine.Random.value;
        return new Vector2(x, y);
    }
}
