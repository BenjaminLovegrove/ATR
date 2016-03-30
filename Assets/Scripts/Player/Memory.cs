using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Water;

// Script for handling in game memory events

public class Memory : MonoBehaviour
{
    [Header("Visual Effects")]
    public bool nightTime = false;
    public bool switchSkyBox;
    public float fadeTime = 4f;
    public float flashDelay;
    public float memoryFog;
    public float fogDiminishAmount = 0.1f;
    public float memoryBloom = 1f;

    [Header("Memory Objects")]
    public AudioClip[] memoryDialogue;  
    public GameObject[] switchMe;
    public Terrain myTerrain;
    public Light sceneLighting;
    public Material originalSkyBox;
    public Material alternateSkyBox;

    // Protected variables
    private bool disableControls;
    private float startBloom;
    private float startFog;
    private bool extraDiminish = false;
    private float fadeTimer;
    private float memoryLength = 0f;
    private float memTimer = 0f;
    private float bgmMaxVol;
    private float bgmLerp = 0;
    private float delayTimer;
    private bool memorySkippable;

    // Components
    private AudioClip newBGM;
    private AudioSource bgmSource;
    private AudioSource dialogueAudio;
    private BloomAndFlares bloom;
    private GameObject[] waterObjs;
    private GameObject skySphere;    
    private GlobalFog fog;        
    private Image memoryFlashObj;
    private Water[] water;    
    
    // Display memory flash game obj coroutine
    IEnumerator InstantiateMemFlash()
    {
        memorySkippable = false;
        EventManager.inst.memoryPlaying = true;
        memoryFlashObj.CrossFadeAlpha(255, 1, false);
        yield return new WaitForSeconds(2f);
        StartMemory();
        yield return new WaitForSeconds(flashDelay - 1.5f);
        memoryFlashObj.CrossFadeAlpha(255, 1, false);
        yield return new WaitForSeconds(2);
        EndMemory();
    }

    // Skip memory coroutine
    IEnumerator SkipMemory()
    {
        memTimer = 2;
        dialogueAudio.Stop();
        memoryFlashObj.CrossFadeAlpha(255, 1, false);
        yield return new WaitForSeconds(2);
        EndMemory();
    }

    void Awake()
    {
        // Apply regular controls
        EventManager.inst.memoryLookScalar = 1;
        EventManager.inst.memoryMoveScalar = 1;

        // Get all water objects
        waterObjs = GameObject.FindGameObjectsWithTag("Water");
        if (waterObjs != null)
        {
            water = new Water[waterObjs.Length];
            for (int i = 0; i < waterObjs.Length; i++)
            {
                water[i] = waterObjs[i].GetComponent<Water>();
            }
        }

        // Getters and setters
        dialogueAudio = GameObject.Find("MemoryDialogue").GetComponent<AudioSource>();
        bgmSource = GameObject.Find("BackGroundMusicSource").GetComponent<AudioSource>();
        bgmMaxVol = bgmSource.volume;
        sceneLighting = GameObject.Find("Directional Light").GetComponent<Light>();
        skySphere = GameObject.Find("skySphere");
        memoryFlashObj = GameObject.Find("MemoryFlashObj").GetComponent<Image>();
        bloom = gameObject.GetComponent<BloomAndFlares>();
        fog = gameObject.GetComponent<GlobalFog>();

        startBloom = bloom.bloomIntensity;
        startFog = fog.heightDensity;
    }

    void Update()
    {
        SkipMemoryCheck();
    }

	void FixedUpdate()
    {
        MemoryTimers();        
    }

    // Skip memory
    void SkipMemoryCheck()
    {
        if (EventManager.inst.memoryPlaying && memorySkippable)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StopCoroutine("InstantiateMemFlash");
                StartCoroutine("SkipMemory");
            }
        }
    }

    // Begin memory
    void StartMemory()
    {
        memorySkippable = true;
        
        // Disable controls toggle
        if (disableControls)
        {
            EventManager.inst.memoryLookScalar = 0;
            EventManager.inst.memoryMoveScalar = 0;
        }

        // Apply scalar if controls are to not be fully disabled
        if (!disableControls)
        {
            EventManager.inst.memoryLookScalar = 0.05f;
            EventManager.inst.memoryMoveScalar = 0.25f;
        }

        // Enable water reflection
        if (waterObjs != null)
        {
            foreach (Water w in water)
            {
                w.WaterReflections(true);
            }
        }

        // Increase tree density
        if (myTerrain != null)
        {
            myTerrain.treeDistance = 150;
        }

        // Switch active/inactive objects
        if (switchMe != null)
        {
            foreach (GameObject obj in switchMe)
            {
                if (obj.activeSelf)
                {
                    obj.SetActive(false);
                }
                else
                {
                    obj.SetActive(true);
                }
            }
        }

        // Replace skybox material
        if (switchSkyBox)
        {
            RenderSettings.skybox = alternateSkyBox;
        }

        // Change scene lighting if night time
        if (nightTime)
        {
            sceneLighting.intensity = sceneLighting.intensity * 0.25f;
            RenderSettings.fog = false;
            skySphere.SetActive(false);
        }

        memoryFlashObj.CrossFadeAlpha(0, 1, false);
    }

    // End memory
    void EndMemory()
    {
        EventManager.inst.memoryPlaying = false;
        EventManager.inst.memoryPlaying = false;

        // Set controls back to normal
        disableControls = false;
        EventManager.inst.memoryLookScalar = 1;
        EventManager.inst.memoryMoveScalar = 1;

        if (newBGM != null)
        {
            bgmSource.clip = newBGM;
            newBGM = null;
        }

        // Resume music
        bgmSource.Play();
        bgmLerp = 1;

        // Turn off water reflection
        if (waterObjs != null)
        {
            foreach (Water w in water)
            {
                w.WaterReflections(false);
            }
        }

        // Reduce tree density
        if (myTerrain != null)
        {
            myTerrain.treeDistance = 100;
        }

        // Switch back active/inactive objects
        if (switchMe != null)
        {
            foreach (GameObject obj in switchMe)
            {
                if (obj.activeSelf)
                {
                    obj.SetActive(false);
                }
                else
                {
                    obj.SetActive(true);
                }
            }
        }

        // Return original skybox
        if (switchSkyBox)
        {
            RenderSettings.skybox = originalSkyBox;
        }

        // Return the lightning to normal if night
        if (nightTime)
        {
            sceneLighting.intensity = sceneLighting.intensity / 0.25f;
            RenderSettings.fog = true;
            skySphere.SetActive(true);
        }

        memoryFlashObj.CrossFadeAlpha(0, 1, false);
    }

    // Sendmessage receiver to externally activate a memory
    // The float will determine the length of the memory
    // TriggerManager script will activate this function
    void EnterMemory (float duration)
    {
        bgmSource.Pause();
        flashDelay = duration;
        StartCoroutine("InstantiateMemFlash");        
        startFog = fog.heightDensity;

        if (!extraDiminish)
        {
            memoryFog = fog.heightDensity * fogDiminishAmount;
        }
        
        else
        {
            memoryFog = fog.heightDensity * (fogDiminishAmount * 1.5f);
            extraDiminish = false;
        }

        EventManager.inst.memoryPlaying = true;
        fadeTimer = 0;
        memTimer = duration;
        memoryLength = duration;
    }

    // Transition fog and bloom density
    void MemoryLerp()
    {
        if (fadeTimer < 1)
        {
            fadeTimer += Time.deltaTime / fadeTime;
        }
        memTimer -= Time.deltaTime;

        bloom.bloomIntensity = Mathf.Lerp(startBloom, memoryBloom, fadeTimer);
        fog.heightDensity = Mathf.Lerp(startFog, memoryFog, fadeTimer);
    }

    // Exit memory
    void FadeOutMemory()
    {
        fadeTimer -= Time.deltaTime / fadeTime;
        bloom.bloomIntensity = Mathf.Lerp(startBloom, memoryBloom, fadeTimer);
        fog.heightDensity = Mathf.Lerp(startFog, memoryFog, fadeTimer);
    }

    // Increment timers
    void MemoryTimers()
    {
        if (EventManager.inst.memoryPlaying)
        {
            delayTimer += Time.fixedDeltaTime;
        }

        // Once the delay timer reaches the value of the memory length, the script will end
        if (delayTimer > memoryLength)
        {
            delayTimer = 0;
            EventManager.inst.memoryPlaying = true;
        }

        if (memTimer > 0)
        {
            MemoryLerp();
        }

        if (memTimer < 0 && bloom.bloomIntensity > startBloom)
        {
            FadeOutMemory();
        }
    }

    #region Simple Functions
    void SetSwitches(GameObject[] switchObjects)
    {
        switchMe = switchObjects;
    }

    void NightCheck(bool check)
    {
        nightTime = check;
    }

    void SetSwitch(GameObject[] switches)
    {
        switchMe = switches;
    }

    void SetBGM(AudioClip passBGM)
    {
        newBGM = passBGM;
    }

    void ExtraDim(bool check)
    {
        extraDiminish = true;
    }

    void DisableControls(bool disable)
    {

        disableControls = disable;
    }
    #endregion
}



