using UnityEngine;
using System.Collections;

public class EndingTrigger : MonoBehaviour {

    public AudioSource BGM;
    public AudioClip precreditsAudio;
    public bool switchedTrack;

	void OnTriggerEnter (Collider col) {

        if (col.gameObject.tag == "Player")
        {
            EventManager.inst.atEndTerrain = true;
        }
	}

    void OnTriggerExit (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            EventManager.inst.atEndTerrain = false;
        }
    }

    void Update()
    {
        if (!switchedTrack && EventManager.inst.atEndTerrain)
        {
            BGM.volume = Mathf.Lerp(BGM.volume, 0, Time.deltaTime / 2);

            if (BGM.volume < 0.05f)
            {
                BGM.clip = precreditsAudio;
                BGM.Play();
                BGM.volume = 0.45f;
                switchedTrack = true;
            }
        }
    }
}
