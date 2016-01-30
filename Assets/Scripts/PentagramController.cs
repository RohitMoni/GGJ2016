using UnityEngine;
using System.Collections;

public class PentagramController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Pentagram collision detected");
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("OverPentagram", true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Pentagram collision left");
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("OverPentagram", false);
        }
    }
}
