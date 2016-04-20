﻿using UnityEngine;
using System.Collections;

public class LandingSFX : MonoBehaviour {

    private bool triggered;
    public AudioClip sfx;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && !triggered)
        {
            triggered = true;
            AudioSource.PlayClipAtPoint(sfx, col.transform.position, 0.6f);
        }
    }
}
