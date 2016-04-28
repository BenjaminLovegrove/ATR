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
        buttonSource.PlayOneShot(mouseOver, 0.5f);
	}
	
	public void MouseDown () {
        buttonSource.PlayOneShot(mouseDown);
    }
}
