using UnityEngine;
using System.Collections;

public class ChildPickupScript : MonoBehaviour {

    WitchController witchController;

	// Use this for initialization
	void Start () {
        witchController = GameObject.Find("Witch").GetComponent<WitchController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            witchController.OverChild(true, transform.parent.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            witchController.OverChild(false, transform.parent.gameObject);
        }
    }
}
