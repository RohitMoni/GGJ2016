using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PentagramController : MonoBehaviour {

    const int maxScore = 7;
    public int playerNumber;
    private int score = 0;
    private SpriteRenderer winSprite;
    private bool gameover = false;
    public Vector3 [] kidPositions;
    private List<bool> kidPositionOccupied;

    // Use this for initialization
    void Start () {
        winSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        winSprite.enabled = false;
        kidPositionOccupied = new List<bool>(kidPositions.Count());
        for (int i = 0; i < kidPositions.Count(); ++i)
        {
            kidPositionOccupied.Add(false);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return) && gameover)
        {
            Time.timeScale = 1;
            Application.LoadLevel("MainMenu");
        }
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

    public void KidDroppedFunction (GameObject kidDropped)
    {
        kidDropped.transform.parent = transform;
        if (score < kidPositions.Count())
        {
            kidDropped.transform.localPosition = Vector3.zero;

            Debug.Log(kidPositionOccupied.Count);
            for (int i = 0; i < kidPositionOccupied.Count; ++i)
            {
                if (!kidPositionOccupied[i])
                {
                    kidDropped.transform.localPosition = kidPositions[i];
                    kidPositionOccupied[i] = true;
                    break;
                }
            }
        }

        ++score;

        Debug.Log(score);
        if (score >= maxScore)
        {
            Time.timeScale = 0;
            winSprite.enabled = true;
            gameover = true;
        }
    }

    public void KidPickedUpFunction(GameObject kidPickedUp)
    {
        for (int i = 0; i < kidPositionOccupied.Count(); ++i)
        {
            var kidPosition = kidPositions[i];
            if (kidPickedUp.transform.localPosition == kidPosition)
            {
                Debug.Log("kid picked up!: " + i);
                kidPositionOccupied[i] = false;
                --score;
            }
        }
    }
}
