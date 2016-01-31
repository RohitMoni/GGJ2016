using UnityEngine;
using System.Collections;

public class PentagramController : MonoBehaviour {

    public int playerNumber;
    private int score = 0;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<WitchController>().OverPentagram(true, gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<WitchController>().OverPentagram(false, gameObject);
        }
    }

    public void KidDroppedFunction ()
    {
        ++score;
        Debug.Log(score + " of team: " + playerNumber);
    }
}
