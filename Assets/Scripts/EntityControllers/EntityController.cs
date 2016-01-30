using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EntityControllers : MonoBehaviour
{
    Rigidbody2D rigidbody;

    protected Vector2 position
    {
        get { return (Vector2)transform.position; }
        set { transform.position = (Vector2)value; }
    }

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        rigidbody.AddForce(ComputeAdditionalForces());
        UpdateLogic();
    }

    abstract protected Vector2 ComputeAdditionalForces();
    protected abstract void UpdateLogic();
}
