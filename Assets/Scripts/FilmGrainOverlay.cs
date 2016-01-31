using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class FilmGrainOverlay : MonoBehaviour {

    SpriteRenderer spriteRender;
    public float animationFrequency = 12f;
    public List<Sprite> filmGrainSprites;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        spriteRender = GetComponent<SpriteRenderer>();
    }

	// Use this for initialization
	void Start () {
	    
	}

    private float timeSinceTransition = 0;
	void Update () {
        timeSinceTransition += Time.deltaTime;
        if(timeSinceTransition > 1 / animationFrequency)
        {
            timeSinceTransition = 0;

            var frame = (int)Random.Range(0, filmGrainSprites.Count - 1);
            int randomFrame = (int)(Random.value * (filmGrainSprites.Count - 1));

            spriteRender.sprite = filmGrainSprites[frame];
        }
	}
}
