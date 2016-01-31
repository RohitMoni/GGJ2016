using UnityEngine;
using System.Collections;

public class MainMenuSound : MonoBehaviour
{
    public AudioClip menusound;

	// Use this for initialization
	void Start () {
	    if (menusound != null)
	    {
            AudioSource.PlayClipAtPoint(menusound, Camera.main.transform.position);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
