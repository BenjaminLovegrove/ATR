using UnityEngine;
using System.Collections;

public class MenuFountain : MonoBehaviour {

    public AudioSource thisAudio;

    void Start ()
    {
        thisAudio = gameObject.GetComponent<AudioSource>();
    }

    void OnEnable () {
        thisAudio.Stop();
        thisAudio.Play();
	}

}
