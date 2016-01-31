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

    public List<AudioClip> kidScreams;
    private bool overPentagram = false;
    private bool overChild = false;

    private GameObject heldChild;
    private bool holdingChild = false;

    private GameObject pentagramObject;

    private static readonly Vector3 upHitbox = new Vector3(0, 2, 0);
    private static readonly Vector3 upRightHitbox = new Vector3(1.5f, 1.5f, 0);
    private static readonly Vector3 rightHitbox = new Vector3(2, 0, 0);
    private static readonly Vector3 downRightHitbox = new Vector3(1.5f, -1.5f, 0);
    private static readonly Vector3 downHitbox = new Vector3(0, -2, 0);
    private static readonly Vector3 downLeftHitbox = new Vector3(-1.5f, -1.5f, 0);
    private static readonly Vector3 leftHitbox = new Vector3(-2, 0, 0);
    private static readonly Vector3 upLeftHitbox = new Vector3(-1.5f, 1.5f, 0);

    public bool isStunned = false;

    private GameObject hitHitBox;

    public List<GameObject> ChildrenWithinRange = new List<GameObject>();

    protected void Start()
    {
        base.Start();
        hitHitBox = transform.FindChild("HitBox").gameObject;
    }

    protected override Vector2 ComputeAdditionalForces()
    {
        if (isStunned) return new Vector2(0, 0);

        var x = Input.GetKey(rightKey) ? 1 : 0;
        x -= Input.GetKey(leftKey) ? 1 : 0;

        var y = Input.GetKey(upKey) ? 1 : 0;
        y -= Input.GetKey(downKey) ? 1 : 0;

        var force = new Vector2(x, y);

        if (force.x != 0 || force.y != 0)
        {
            force = force.normalized;
        }

        force *= strength;
        return force;
    }

    // Angle is measured in rotations, clockwise from the positive-y axis.
    protected override void UpdateSprite(float angle)
    {
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
            hitHitBox.transform.localPosition = upHitbox;
        }
        else if (angle < (3f / 2) * rotPerSprite)
        {
            sprite = upRightSprite;
            if (mirrorAcrossY)
            {
                hitHitBox.transform.localPosition = upLeftHitbox;
            }
            else
            {
                hitHitBox.transform.localPosition = upRightHitbox;
            }
        }
        else if (angle < (5f / 2) * rotPerSprite)
        {
            sprite = rightSprite;
            if (mirrorAcrossY)
            {
                hitHitBox.transform.localPosition = leftHitbox;
            }
            else
            {
                hitHitBox.transform.localPosition = rightHitbox;
            }
        }
        else if (angle < (7f / 2) * rotPerSprite)
        {
            sprite = downRightSprite;
            if (mirrorAcrossY)
            {
                hitHitBox.transform.localPosition = downLeftHitbox;
            }
            else
            {
                hitHitBox.transform.localPosition = downRightHitbox;
            }
        }
        else
        {
            sprite = downSprite;
            hitHitBox.transform.localPosition = downHitbox;
        }
        return;
    }


    protected override void UpdateLogic()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            if (holdingChild) //Drop child
            {
                mass -= heldChild.GetComponent<ChildController>().mass;
                if (overPentagram)
                {
                    pentagramObject.GetComponent<PentagramController>().KidDroppedFunction(heldChild);
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
                if (overPentagram)
                {
                    pentagramObject.GetComponent<PentagramController>().KidPickedUpFunction(heldChild);
                }

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
                mass += heldChild.GetComponent<ChildController>().mass;
                heldChild.transform.parent = gameObject.transform;
                heldChild.GetComponent<SpriteRenderer>().sprite = heldChild.GetComponent<ChildController>().capturedSprite;
                heldChild.GetComponent<ChildController>().enabled = false;

                heldChild.GetComponent<Rigidbody2D>().isKinematic = true;
                heldChild.transform.localPosition = new Vector3(0, -0.5f, 0);
                holdingChild = true;

                if (kidScreams.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, kidScreams.Count);
                    AudioSource.PlayClipAtPoint(kidScreams[randomIndex], Camera.main.transform.position);
                }
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            // We collided with another player, drop any held child now
            if (holdingChild)
            {
                heldChild.transform.parent = null;
                heldChild.GetComponent<ChildController>().enabled = true;
                heldChild.GetComponent<Rigidbody2D>().isKinematic = false;
                holdingChild = false;
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
   
        
   