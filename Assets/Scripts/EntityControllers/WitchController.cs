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

    private GameObject pentagramObject;

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
                if (overPentagram && holdingChild == true)
                {
                    print("Dropping Child");
                    pentagramObject.GetComponent<PentagramController>().KidDroppedFunction();
                    Destroy(heldChild);
                    overChild = false;
                    holdingChild = false;
                }
                else if (holdingChild == true)
                {
                    print("Dropping Child");
                    heldChild.transform.parent = null;
                    heldChild.GetComponent<ChildController>().enabled = true;
                    heldChild.GetComponent<Rigidbody2D>().isKinematic = false;
                    holdingChild = false;
                }
            }
            else if(overChild == true && holdingChild == false) //Grab child
            {
                print("Grabbing Child");
                heldChild = grabableChild;
                heldChild.transform.parent = gameObject.transform;
                heldChild.GetComponent<ChildController>().enabled = false;
                heldChild.GetComponent<Rigidbody2D>().isKinematic = true;
                heldChild.transform.localPosition = new Vector3(0, -0.5f, 0);
                holdingChild = true;
            }
        }
    }

    public void OverPentagram(bool enabled, GameObject pentagram)
    {
        overPentagram = enabled;
        pentagramObject = pentagram;
    }

    public void OverChild(bool enabled, GameObject child)
    {
        overChild = enabled;
        grabableChild = child;
    }
}