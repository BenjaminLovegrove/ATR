using UnityEngine;
using System.Collections;

public class SpawnSFX : MonoBehaviour {

    public AudioSource spawnSFX;
    public AudioSource ambientBreathing;
    public float ambientDelay;
    private bool started;

	void Start () {
	    if (EventManager.inst.hasDied)
        {
            //spawnSFX.Play();
            EventManager.inst.hasDied = false;
        }
	}
	
	void Update () {
        ambientDelay -= Time.deltaTime;

        if (ambientDelay < 0 && !ambientBreathing.isPlaying && !started)
        {
            ambientBreathing.Play();
        }
	}
}
