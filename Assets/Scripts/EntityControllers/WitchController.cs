using System;
using System.Collections;

using UnityEngine;

public class WitchController : EntityControllers
{
    public KeyCode upKey = KeyCode.W;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;

    public Sprite upSprite;
    public Sprite upRightSprite;
    public Sprite rightSprite;
    public Sprite downRightSprite;
    public Sprite downSprite;

    private bool overPentagram = false;
    private bool overChild = false;

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
            Debug.Log("up");
            sprite = upSprite;
        }
        else if (angle < (3f / 2) * rotPerSprite)
        {
            Debug.Log("upright");
            sprite = upRightSprite;
        }
        else if(angle < (5f / 2) * rotPerSprite)
        {
            Debug.Log("right");
            sprite = rightSprite;
        }
        else if(angle < (7f / 2) * rotPerSprite)
        {
            Debug.Log("downright");
            sprite = downRightSprite;
        }
        else
        {
            Debug.Log("down");
            sprite = downSprite;
        }
        return;
    }

    protected override void UpdateLogic() { }

    public void OverPentagram(bool enabled)
    {
        overPentagram = enabled;
    }

    public void OverChild(bool enabled)
    {
        overChild = enabled;
    }
}