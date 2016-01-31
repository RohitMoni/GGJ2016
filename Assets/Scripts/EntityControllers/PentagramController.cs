using UnityEngine;
using System.Collections;

public class PentagramController : MonoBehaviour {

    const int maxScore = 2;
    public int playerNumber;
    private int score = 0;
    private SpriteRenderer winSprite;
    private bool gameover = false;

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

    public void KidDroppedFunction ()
    {
        ++score;
        if (score >= maxScore)
        {
            Time.timeScale = 0;
            winSprite.enabled = true;
            gameover = true;
        }
    }
}
