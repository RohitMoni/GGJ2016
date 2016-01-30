using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour {
    int player1Score, player2Score;
    SpriteRenderer spriteRenderer;
    public int winningScore;
    public Sprite player1Wins, player2Wins;

	// Use this for initialization
	void Start () {
        player1Score = 0;
        player2Score = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void UpdateScore(int playerNumber, int newScore) {
        int currPlayer = 0;
        if (playerNumber == 1)
        {
            player1Score = newScore;
            currPlayer = 1;
        }
        else if (playerNumber == 2) {
            player2Score = newScore;
            currPlayer = 2;
        }

        if (newScore >= winningScore) {
            if (currPlayer == 1)
            {
                spriteRenderer.sprite = player1Wins;
            }
            else if (currPlayer == 2)
            {
                spriteRenderer.sprite = player2Wins;
            }
            spriteRenderer.enabled = true;
        }
    }
}
