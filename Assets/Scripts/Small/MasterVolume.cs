using UnityEngine;
using System.Collections;

// Attach this script to any gameobject that has an audio source
// that you wish to be effected by the master volume setting
// *** DO NOT PLACE THIS ON THE PLAYER OBJ, IT WILL CONFLICT WITH SCALING HEARTBEAT SFX...
// ...I have manually added the scalar to the audio source instead ***

public class MasterVolume : MonoBehaviour
{
    public AudioSource[] audio;
    private float tempVolume;

	void Start ()
    {
        audio = gameObject.GetComponents<AudioSource>();
	}	

	void FixedUpdate ()
    {
        tempVolume = EventManager.inst.masterVolume;

        if (audio[0].volume != tempVolume)
        {
            for (int i = 0; i < audio.Length; i++)
            {
                audio[i].volume = EventManager.inst.masterVolume;
            }
        }        
	}
}
