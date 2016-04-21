using UnityEngine;
using System.Collections;

public class PlaceholderMusicSwitch : MonoBehaviour {

    public AudioSource bgmSource;
    public AudioClip track0;
    public AudioClip track1;
    private int trackNum;
    private float switchDelay;

	void Update () {
	    if (!bgmSource.isPlaying)
        {
            switchDelay += Time.deltaTime;
        }

        if (!bgmSource.isPlaying && switchDelay > 12f)
        {
            if (trackNum == 0)
            {
                bgmSource.clip = track1;
                trackNum = 1;
            } else
            {
                bgmSource.clip = track0;
                trackNum = 0;
            }

            bgmSource.Play();
            switchDelay = 0;
        }
	}
}
