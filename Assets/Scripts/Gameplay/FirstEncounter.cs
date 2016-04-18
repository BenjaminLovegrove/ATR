﻿using UnityEngine;
using System.Collections;

// One off script for the first enemy encounter in the game

public class FirstEncounter : MonoBehaviour
{
    public Memory memScript;
    public AudioSource memDialogue;
    public Transform cameraLookAtTarget;    
    public float encounterDuration;
    public float spawnObjectsDelay;
    public AudioSource audio;
    public GameObject[] setActiveObjects;
    private bool triggered;
    private Transform playersTrans;
    private bool onlyPlayOnce;
    private float totalDuration;


    void Awake()
    {
        onlyPlayOnce = false;
        playersTrans = GameObject.FindGameObjectWithTag("Player").transform;
        totalDuration = encounterDuration;
    }

	void FixedUpdate ()
    {
        // When sequence begins
        if (triggered)
        {
            //Disable controls after a small reaction time
            if (encounterDuration < totalDuration - 0.5f)
            {
                EventManager.inst.controlsDisabled = true;
                playersTrans.transform.rotation = Quaternion.Lerp(playersTrans.transform.rotation, Quaternion.LookRotation(cameraLookAtTarget.position - playersTrans.transform.position), Time.deltaTime * 1f);

                // When sequence ends (added only once so it doesnt keep turning the music up)
                if (!triggered && !onlyPlayOnce)
                {
                    memScript.musicLerp = 0;
                    memScript.musicFadeIn = true;
                    onlyPlayOnce = true;
                }
            }
            
            encounterDuration -= Time.deltaTime;
            memDialogue.Stop();

            // Delay before guard is activated
            if (encounterDuration < spawnObjectsDelay)
            {
                for (int i = 0; i < setActiveObjects.Length; i++)
                {
                    setActiveObjects[i].SetActive(true);
                }
            }
        }

        // When timer expires
        if (encounterDuration <= 0)
        {
            EventManager.inst.controlsDisabled = false;
            triggered = false;
            setActiveObjects[0].SetActive(false);
        }
    }

    // Trigger sequence
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            print("Encounter Triggered");
            audio.Play();
            triggered = true;
            memScript.musicLerp = 0;
            memScript.musicFadeOut = true;
        }
    }
}
