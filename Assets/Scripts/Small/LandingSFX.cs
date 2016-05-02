using UnityEngine;
using System.Collections;

public class LandingSFX : MonoBehaviour {

    private bool triggered;
    public AudioClip sfx;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && !triggered)
        {
            triggered = true;
            if (EventManager.inst.currentMemory != 8)
            {
                AudioSource.PlayClipAtPoint(sfx, col.transform.position, 0.6f);
            }
        }
    }
}
