using System;
using System.Collections;

using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WitchController : EntityControllers
{

    public KeyCode upKey = KeyCode.W;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode pickupKey = KeyCode.M;

    public Texture2D upRightSprite;
    public Texture2D downRightSprite;

    private bool overPentagram = false;
    private bool overChild = false;

    private GameObject heldChild;
    private GameObject grabableChild;
    private bool holdingChild = false;

    protected override Vector2 ComputeAdditionalForces()
    {
        var x = Input.GetKey(rightKey) ? 1 : 0;
        x -= Input.GetKey(leftKey) ? 1 : 0;

        var y = Input.GetKey(upKey) ? 1 : 0;
        y -= Input.GetKey(downKey) ? 1 : 0;

        var force = new Vector2(x, y);

        if (force.x != 0 || force.y != 0)
        {
            force = force.normalized;
        }
        return force;
    }

    protected override void UpdateLogic()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            if (holdingChild == true) //Drop child
            {
                heldChild.transform.parent = null;
                holdingChild = false;
            }
            else if(overChild == true) //Grab child
            {
                heldChild = grabableChild;
                heldChild.transform.parent = gameObject.transform;
                holdingChild = true;
            }
        }
    }

    public void OverPentagram(bool enabled)
    {
        overPentagram = enabled;
    }

    public void OverChild(bool enabled, GameObject child)
    {
        overChild = enabled;
        grabableChild = child;
    }
}