using UnityEngine;
using System.Collections.Generic;

using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FilmGrainOverlay : MonoBehaviour {

    Image image;
    public float animationFrequency = 12f;
    public List<Sprite> filmGrainSprites;

    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
        image = GetComponent<Image>();
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

            image.sprite = filmGrainSprites[frame];
        }
	}
}
