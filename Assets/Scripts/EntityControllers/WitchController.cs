using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchController : EntityControllers
{
    public KeyCode upKey = KeyCode.W;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode pickupKey = KeyCode.M;

    public Sprite upSprite;
    public Sprite upRightSprite;
    public Sprite rightSprite;
    public Sprite downRightSprite;
    public Sprite downSprite;

    private bool overPentagram = false;
    private bool overChild = false;

    private GameObject heldChild;
    private GameObject grabableChild;
    private bool holdingChild = false;

    private GameObject pentagramObject;

    //List<BadGuy> badguys = new List<BadGuy>();
    public List<GameObject> ChildrenWithinRange = new List<GameObject>();

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

    // Angle is measured in rotations, clockwise from the positive-y axis.
    protected override void UpdateSprite(float angle)
    {
        //bool mustFlip = false;
        if (angle > 0.5)
        {
            angle = 1 - angle;
            mirrorAcrossY = true;
        }
        else
        {
            mirrorAcrossY = false;
        }

        var rotPerSprite = 1f / 8;

        if (angle < (1f / 2) * rotPerSprite)
        {
            sprite = upSprite;
        }
        else if (angle < (3f / 2) * rotPerSprite)
        {
            sprite = upRightSprite;
        }
        else if(angle < (5f / 2) * rotPerSprite)
        {
            sprite = rightSprite;
        }
        else if(angle < (7f / 2) * rotPerSprite)
        {
            sprite = downRightSprite;
        }
        else
        {
            sprite = downSprite;
        }
        return;
    }


    protected override void UpdateLogic()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            if (holdingChild) //Drop child
            {
                if (overPentagram)
                {
                    pentagramObject.GetComponent<PentagramController>().KidDroppedFunction();
                    for (int i = 0; i < ChildrenWithinRange.Count; i++)
                    {
                        if (heldChild.gameObject == ChildrenWithinRange[i].gameObject)
                        {
                            ChildrenWithinRange.RemoveAt(i);
                            break;
                        }
                    }
                    Destroy(heldChild);
                    //overChild = false;
                    holdingChild = false;
                }
                else
                {
                    heldChild.transform.parent = null;
                    heldChild.GetComponent<ChildController>().enabled = true;
                    heldChild.GetComponent<Rigidbody2D>().isKinematic = false;
                    holdingChild = false;
                }
            }
            else if(ChildrenWithinRange.Count > 0 && holdingChild == false) //Grab child
            {
                heldChild = grabableChild;
                float distance = 10000f;
                for (int i = 0; i < ChildrenWithinRange.Count; i++)
                {                    
                    float newDistance = Vector3.Distance(gameObject.transform.position, ChildrenWithinRange[i].transform.position);
                    if(newDistance < distance)
                    {
                        distance = newDistance;
                        heldChild = ChildrenWithinRange[i];
                    }
                }

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
        //overChild = enabled;
        if (enabled)
        {
            ChildrenWithinRange.Add(child);
        }
        else
        {
            for (int i=0; i < ChildrenWithinRange.Count; i++)
            {
                if (child.gameObject == ChildrenWithinRange[i].gameObject)
                {
                    ChildrenWithinRange.RemoveAt(i);
                    break;
                }
            }
        }
    }

}

public struct ChildrenInRange
{
    public GameObject child;

    public void ChildInRange(GameObject newChild)
    {
        child = newChild;
    }
}
   
        
   