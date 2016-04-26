using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

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
    private bool musicResumed;
    private bool crouched;
    private float totalDuration;
    public AudioSource crouchSFXSource;
    public AudioClip crouchSFX;
    public AudioClip standSFX;
    public VignetteAndChromaticAberration vignette;
    public float startVignetteIntensity;

    void Awake()
    {
        vignette = GameObject.Find("Camera").GetComponent<VignetteAndChromaticAberration>();
        startVignetteIntensity = vignette.intensity;
        musicResumed = false;
        crouched = false;
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
                vignette.intensity = Mathf.Lerp(vignette.intensity, 6, Time.deltaTime);
                EventManager.inst.playerCrouch = true;
                if (!crouched)
                {
                    crouchSFXSource.PlayOneShot(crouchSFX);
                    crouched = true;
                }

                EventManager.inst.controlsDisabled = true;
                playersTrans.transform.rotation = Quaternion.Lerp(playersTrans.transform.rotation, Quaternion.LookRotation(cameraLookAtTarget.position - playersTrans.transform.position), Time.deltaTime * 1f);
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
            vignette.intensity = Mathf.Lerp(vignette.intensity, startVignetteIntensity, Time.deltaTime / 4);
            EventManager.inst.controlsDisabled = false;
            triggered = false;
            setActiveObjects[0].SetActive(false);
        }

        if (!EventManager.inst.firstEncounter && !EventManager.inst.memoryPlaying)
        {
            EventManager.inst.controlsDisabled = false;
        }


        // When sequence ends (added only once so it doesnt keep turning the music up)
        if (!triggered && !musicResumed && encounterDuration < 0)
        {
            EventManager.inst.firstEncounter = false;
            EventManager.inst.playerCrouch = false;
            crouchSFXSource.PlayOneShot(standSFX);
            memScript.musicLerp = 0;
            memScript.musicFadeIn = true;
            musicResumed = true;
        }
    }

    // Trigger sequence
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            EventManager.inst.firstEncounter = true;
            print("Encounter Triggered");
            if (!triggered)
            {
                PlaySFX();
            }
            triggered = true;
        }
    }

    void PlaySFX()
    {
        audio.Play();
        memScript.musicLerp = 0;
        memScript.musicFadeOut = true;
    }
}
