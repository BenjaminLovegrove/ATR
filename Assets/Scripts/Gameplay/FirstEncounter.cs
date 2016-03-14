using UnityEngine;
using System.Collections;

// One off script for the first enemy encounter in the game

public class FirstEncounter : MonoBehaviour
{
    public Transform cameraLookAtTarget;
    private bool triggered;
    public float encounterDuration;
    public float spawnObjectsDelay;
    public AudioSource audio;
    public GameObject[] setActiveObjects;
    public Transform playersTrans;

    void Awake()
    {
        playersTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

	void FixedUpdate ()
    {
        // When sequence begins
        if (triggered)
        {
            EventManager.inst.controlsDisabled = true;
            playersTrans.transform.rotation = Quaternion.Lerp(playersTrans.transform.rotation, Quaternion.LookRotation(cameraLookAtTarget.position - playersTrans.transform.position), Time.deltaTime * 1f);
            encounterDuration -= Time.deltaTime;            
        }

        // Delay before guard is activated
        if (encounterDuration < spawnObjectsDelay)
        {
            for (int i = 0; i < setActiveObjects.Length; i++)
            {
                setActiveObjects[i].SetActive(true);
            }
        }

        // When timer expires
        if (encounterDuration <= 0)
        {
            triggered = false;            
        }

        // When sequence ends
        if (!triggered)
        {
            EventManager.inst.controlsDisabled = false;
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
        }
    }
}
