using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

// One off script for the first enemy encounter in the game

public class FirstEncounter : MonoBehaviour
{
    public Memory memScript;
    public AudioSource memDialogue;
    public Transform cameraLookAtTarget;
    public Transform cameraLookAtTarget2;
    public float encounterDuration;
    public float spawnObjectsDelay;
    public AudioSource audio;
    public GameObject[] setActiveObjects;
    private bool triggered;
    private bool triggeredCheck;
    private Transform playersTrans;
    private Transform playersTrans2;
    private bool musicResumed;
    private bool crouched;
    private float totalDuration;
    public AudioSource crouchSFXSource;
    public AudioClip crouchSFX;
    public AudioClip standSFX;
    public VignetteAndChromaticAberration vignette;
    public float startVignetteIntensity;
    public bool audioNotTriggered = true;
    public PlayerCam playerCamScr;
    public AudioSource crunchsfx;


    void Awake()
    {
        triggeredCheck = false;
        vignette = GameObject.Find("Camera").GetComponent<VignetteAndChromaticAberration>();
        startVignetteIntensity = vignette.intensity;
        musicResumed = false;
        crouched = false;
        playersTrans = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>().transform;
        playersTrans2 = GameObject.FindGameObjectWithTag("Player").transform;
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
                vignette.intensity = Mathf.Lerp(vignette.intensity, 13, Time.deltaTime);
                EventManager.inst.playerCrouch = true;
                if (!crouched)
                {
                    crouchSFXSource.PlayOneShot(crouchSFX);
                    crouched = true;
                }

                EventManager.inst.controlsDisabled = true;

                if (encounterDuration > totalDuration * 0.1f)
                {                  
                    Quaternion newDir = Quaternion.Lerp(playersTrans.transform.rotation, Quaternion.LookRotation(cameraLookAtTarget.position - playersTrans.transform.position), Time.deltaTime * 1f);
                    //playersTrans.transform.localEulerAngles = (new Vector3(newDir.eulerAngles.x, 0, 0));
                    playersTrans2.transform.localEulerAngles = (new Vector3(0, newDir.eulerAngles.y, 0));
                } else
                {
                    Quaternion newDir = Quaternion.Lerp(playersTrans.transform.rotation, Quaternion.LookRotation(cameraLookAtTarget2.position - playersTrans.transform.position), Time.deltaTime * 3f);
                    //playersTrans.transform.localEulerAngles = (new Vector3(newDir.eulerAngles.x, 0, 0));
                    playersTrans2.transform.localEulerAngles = (new Vector3(0, newDir.eulerAngles.y, 0));

                   
                }

                if (audioNotTriggered && encounterDuration < totalDuration * 0.12f)
                {
                    audioNotTriggered = false;
                    crunchsfx.Play();
                }

                playerCamScr.rotationY = Mathf.Lerp(playerCamScr.rotationY, 5, Time.deltaTime * 5);
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

        if (!EventManager.inst.firstEncounterPlaying && !EventManager.inst.memoryPlaying)
        {
            EventManager.inst.controlsDisabled = false;
        }


        // When sequence ends (added only once so it doesnt keep turning the music up)
        if (!triggered && !musicResumed && encounterDuration < 0)
        {
            EventManager.inst.firstEncounterPlaying = false;
            EventManager.inst.playerCrouch = false;
            crouchSFXSource.PlayOneShot(standSFX);
            musicResumed = true;
            Invoke("FadeInSFX", 57f);
        }
    }

    // Trigger sequence
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && EventManager.inst.firstEncounter)
        {
            EventManager.inst.firstEncounterPlaying = true;
            EventManager.inst.firstEncounter = false;

            if (!triggeredCheck)
            {
                PlaySFX();
                EventManager.inst.timeSinceFirstEnc = 15f;
            }
            triggeredCheck = true;
            triggered = true;
        }
    }

    void PlaySFX()
    {
        audio.Play();
        memScript.bgmSource.Stop();
        memScript.musicLerp = 0;
        memScript.musicFadeOut = true;
    }

    void FadeInSFX()
    {
        memScript.musicLerp = 0;
        memScript.musicFadeIn = true;
        memScript.bgmSource.Play();
    }
}
