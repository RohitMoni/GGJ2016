using System;
using System.Collections;

using UnityEngine;


public class WitchController : MonoBehaviour {

    public float mass = 1;
    public float drag = 1;

    Vector2 position = new Vector2(0, 0);
    Vector2 velocity = new Vector2(0, 0);

    Vector2 forceApplied = new Vector2(0, 0);

    public KeyCode upKey = KeyCode.W;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;

    // Use this for initialization
    void Start() { }
	
	// Update is called once per frame
	void Update ()
    {

        if (KeyboardInputChanged())
        {
            forceApplied = DetermineForceFromKeyboardInput();
            Debug.Log(forceApplied);
        }

        var acceleration = forceApplied - drag * velocity;

        velocity += Time.deltaTime * acceleration;

        position += Time.deltaTime * velocity;

        transform.position = position;
    }

    bool KeyboardInputChanged()
    {
        return Input.GetKeyDown(upKey)
            || Input.GetKeyUp(upKey)

            || Input.GetKeyDown(rightKey)
            || Input.GetKeyUp(rightKey)

            || Input.GetKeyDown(downKey)
            || Input.GetKeyUp(downKey)

            || Input.GetKeyDown(leftKey)
            || Input.GetKeyUp(leftKey);
    }

    Vector2 DetermineForceFromKeyboardInput()
    {
        var x = Input.GetKey(rightKey) ? 1 : 0;
        x -= Input.GetKey(leftKey) ? 1 : 0;

        var y = Input.GetKey(upKey) ? 1 : 0;
        y -= Input.GetKey(downKey) ? 1 : 0;

        return new Vector2(x, y);
    }
}