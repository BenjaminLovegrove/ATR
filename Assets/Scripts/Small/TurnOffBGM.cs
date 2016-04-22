using UnityEngine;
using System.Collections;

public class TurnOffBGM : MonoBehaviour {

    public AudioSource bgmSource;
	
	// Update is called once per frame
	void OnTriggerEnter (Collider col) {
	    if (col.gameObject.tag == "Player")
        {
            bgmSource.Pause();
        }
	}
}
