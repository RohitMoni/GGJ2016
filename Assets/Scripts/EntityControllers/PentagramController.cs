using UnityEngine;
using System.Collections;
using System.Linq;

public class PentagramController : MonoBehaviour {

    const int maxScore = 5;
    public int playerNumber;
    private int score = 0;
    private SpriteRenderer winSprite;
    private bool gameover = false;
    public Vector3 [] kidPositions;

    // Use this for initialization
    void Start () {
        winSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        winSprite.enabled = false;
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
            kidDropped.transform.position = Vector3.zero;
            kidDropped.transform.localPosition = kidPositions[score];
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
}
