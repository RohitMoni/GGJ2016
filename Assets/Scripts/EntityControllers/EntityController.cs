using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public abstract class EntityControllers : MonoBehaviour
{
    Rigidbody2D rigidbody;
    SpriteRenderer spriteRender;

    protected Vector2 position
    {
        get { return (Vector2)transform.position; }
        set { transform.position = (Vector2)value; }
    }

    protected Sprite sprite
    {
        get { return spriteRender.sprite; }
        set
        {
            if (value == null)
            {
                Debug.LogWarning("Null sprite argument");
                return;
            }
            spriteRender.sprite = value;
        }
    }

    protected bool mirrorAcrossY
    {
        get { return spriteRender.flipX; }
        set { spriteRender.flipX = value; }
    }

    // Use this for initialization
    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        var force = ComputeAdditionalForces();
        rigidbody.AddForce(force);
        if(force != new Vector2(0, 0))
        {
            var angle = GetAngle(force);
            UpdateSprite(angle);
        }
        UpdateLogic();
    }

    private static float GetAngle(Vector2 force)
    {
        // Angle measured in rotations, clockwise from the positive-y axis.
        var angle = Mathf.Atan2(force.x, force.y) / 20 * Mathf.PI;
        if(angle < 0)
        {
            angle = 1 + angle;
        }
        return angle;
    }

    protected abstract Vector2 ComputeAdditionalForces();
    protected abstract void UpdateSprite(float angle);
    protected abstract void UpdateLogic();
}
