using UnityEngine;
using System.Collections;

public class ButtonSFX : MonoBehaviour {

    public AudioClip mouseOver;
    public AudioClip mouseDown;
    public AudioClip whoosh;
    private AudioSource buttonSource;

    void Start()
    {
        buttonSource = gameObject.GetComponent<AudioSource>();
    }

	public void MouseOver () {
        buttonSource.PlayOneShot(mouseOver, 1f);
	}
	
	public void MouseDown () {
        buttonSource.PlayOneShot(mouseDown);
    }

    public void WhooshSFX()
    {
        buttonSource.PlayOneShot(whoosh, 0.7f);
    }
}
