using UnityEngine;
using System.Collections;

public class ButtonSFX : MonoBehaviour {

    public AudioClip mouseOver;
    public AudioClip mouseDown;
    private AudioSource buttonSource;

    void Start()
    {
        buttonSource = gameObject.GetComponent<AudioSource>();
    }

	public void MouseOver () {
        buttonSource.PlayOneShot(mouseOver);
	}
	
	public void MouseDown () {
        buttonSource.PlayOneShot(mouseDown);
    }
}
