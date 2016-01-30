using UnityEngine;
using System.Collections;

public abstract class EntityControllers : MonoBehaviour
{

    public float mass = 1;
    public float drag = 1;

    protected Vector2 position
    {
        get { return (Vector2)transform.position; }
        set { transform.position = (Vector2)value;  }
    }

    Vector2 velocity = new Vector2(0, 0);

    // Use this for initialization
    void Start() { }
	
	// Update is called once per frame
	void Update ()
    {
        var forceApplied = UpdateForce();

        var acceleration = (forceApplied - drag * velocity) / mass;

        velocity += Time.deltaTime * acceleration;

        transform.position += (Vector3)(Time.deltaTime * velocity);

        UpdateLogic();
        position += Time.deltaTime * velocity;
        //transform.position += (Vector3)(Time.deltaTime * velocity);
    }

    abstract protected Vector2 UpdateForce();
    protected abstract void UpdateLogic();
}
