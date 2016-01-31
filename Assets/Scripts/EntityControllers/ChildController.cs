using UnityEngine;
using System.Collections.Generic;

public class ChildController : EntityControllers
{
    private Transform waypointNode;
    private Transform waypointAnchor;

    public float animationFrequency = 4f;

    public List<Sprite> upRightSprites;
    public List<Sprite> downRightSprites;

    public static float waypointRadius = 1;

    Vector2 waypoint = new Vector2(0, 0);

    new void Start()
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

    private float timeSinceTransition = 0f;
    private int frameCount = 0;
    protected override void UpdateSprite(float angle)
    {
        if(timeSinceTransition > 1 / animationFrequency)
        {
            frameCount += 1;
            if (angle > 0.5)
            {
                angle = 1 - angle;
                mirrorAcrossY = true;
            }
            else
            {
                mirrorAcrossY = false;
            }
            if(angle < 0.25)
            {
                frameCount %= upRightSprites.Count;
                sprite = upRightSprites[frameCount];
            }
            else
            {
                frameCount %= downRightSprites.Count;
                sprite = downRightSprites[frameCount];
            }
            timeSinceTransition = 0;
        }
        else
        {
            timeSinceTransition += Time.deltaTime;
        }
    }

    protected override void UpdateLogic() {}
}
