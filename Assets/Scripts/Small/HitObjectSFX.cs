using UnityEngine;
using System.Collections;

public class HitObjectSFX : MonoBehaviour {

    public AudioSource thisAudio;
    public AudioClip[] clips;

	void Start () {
        thisAudio = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && !thisAudio.isPlaying)
        {
            thisAudio.clip = clips[Random.Range(0, clips.Length - 1)];
            thisAudio.Play();
        }
    }
}
