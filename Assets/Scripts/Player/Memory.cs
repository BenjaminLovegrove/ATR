using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Water;

// Script for handling in game memory events

public class Memory : MonoBehaviour
{
    [Header("Visual Effects")]
    public bool nightTime;
    public bool switchSkyBox;
    public float fadeTime;
    public float flashDelay;
    public float memoryFog;
    public float fogDiminishAmount;
    public float memoryBloom;

    [Header("Memory Objects")]
    public AudioClip[] memoryDialogue;
    public GameObject[] switchMe;
    public Terrain myTerrain;
    public Light sceneLighting;
    public Material originalSkyBox;
    public Material alternateSkyBox;
    public GameObject oilRigs;

    // Protected variables
    private bool disableControls;
    private bool loadCredits;
    private float startBloom;
    private float startFog;
    private bool extraDiminish;
    private float fadeTimer;
    private float memoryLength;
    private float memTimer;
    private float delayTimer;
    private bool memorySkippable;
    public bool musicFadeOut;
    public bool musicFadeIn;
    public float musicLerp;

    // Components
    private AudioClip newBGM;
    private AudioSource bgmSource;
    private AudioSource dialogueAudio;
    private BloomAndFlares bloom;
    private GameObject[] waterObjs;
    private GameObject skySphere;    
    private GlobalFog fog;        
    private Image memoryFlashObj;
    private RawImage fadeToBlack;
    private Water[] water;    
    
    // Display memory flash game obj coroutine
    IEnumerator InstantiateMemFlash()
    {
        memorySkippable = false;
        EventManager.inst.memoryPlaying = true;
        memoryFlashObj.CrossFadeAlpha(255, 1, false);
        yield return new WaitForSeconds(2f);
        bgmSource.Pause();
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
        //dialogueAudio.Stop();
        memoryFlashObj.CrossFadeAlpha(255, 1, false);
        yield return new WaitForSeconds(2);
        EndMemory();
    }

    void Awake()
    {
        // Apply regular controls
        EventManager.inst.memoryLookScalar = 1;
        EventManager.inst.memoryMoveScalar = 1;

        fadeToBlack = GameObject.Find("FadeToBlack").GetComponent<RawImage>();

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
        FadeBGM();
        MemoryTimers();        
    }
    
    // Music fade in/out for memories
    void FadeBGM()
    {
        musicLerp += Time.deltaTime / 3;

        if (musicFadeOut)
        {
            bgmSource.volume = Mathf.Lerp(0.35f, 0f, musicLerp);
            if (musicLerp > 1)
            {
                musicFadeOut = false;
            }
        }

        if (musicFadeIn)
        {
            bgmSource.volume = Mathf.Lerp(0f, 0.35f, musicLerp);
            if (musicLerp > 1)
            {
                musicFadeIn = false;
            }
        }
    }

    // Skip memory
    void SkipMemoryCheck()
    {
        if (EventManager.inst.memoryPlaying && memorySkippable)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !EventManager.inst.credits)
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
            if (EventManager.inst.credits)
            {
                EventManager.inst.memoryLookScalar = 0;
                EventManager.inst.memoryMoveScalar = 0;
            }
            else
            {
                EventManager.inst.memoryLookScalar = 0.05f;
                EventManager.inst.memoryMoveScalar = 0.175f;
            }

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
        if (myTerrain != null && EventManager.inst.currentLevel == "Coast Ending")
        {
            myTerrain.treeDistance = 1500;
        } else if (myTerrain != null)
        {
            myTerrain.treeDistance = 200;
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
        // Set controls back to normal
        disableControls = false;
        if (!EventManager.inst.credits)
        {
            EventManager.inst.memoryLookScalar = 1;
            EventManager.inst.memoryMoveScalar = 1;
        }

        if (newBGM != null)
        {
            bgmSource.clip = newBGM;
            newBGM = null;
        }

        // Resume music 
        bgmSource.Play();
        musicLerp = 0;
        musicFadeIn = true;
        

        // Turn off water reflection
        if (waterObjs != null)
        {
            foreach (Water w in water)
            {
                w.WaterReflections(false);
            }
        }

        // Reduce tree density
        // Increase tree density
        if (myTerrain != null && EventManager.inst.currentLevel == "Coast Ending")
        {
            myTerrain.treeDistance = 100;
        }
        else if (myTerrain != null)
        {
            myTerrain.treeDistance = 150;
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

        // Switch skybox
        if (switchSkyBox)
        {
            RenderSettings.skybox = alternateSkyBox;
        }

        // Return the lightning to normal if night
        if (nightTime)
        {
            sceneLighting.intensity = sceneLighting.intensity / 0.25f;
            RenderSettings.fog = true;
            skySphere.SetActive(true);
        }

        memoryFlashObj.CrossFadeAlpha(0, 1, false);
        Invoke("MemoryPlayingDelay", 1f);
        EventManager.inst.controlsDisabled = false;

        // Load credits and fade out
        if (loadCredits)
        {
            oilRigs.SetActive(true);
            RenderSettings.fogDensity = 0.001f;
            fog.heightDensity = 0.7f;
            fadeToBlack.CrossFadeAlpha(1, 10, false);
            Invoke("CutToCredits", 30f);
        }
    }

    // Sendmessage receiver to externally activate a memory
    // The float will determine the length of the memory
    // TriggerManager script will activate this function
    void EnterMemory (float duration)
    {
        musicLerp = 0;
        musicFadeOut = true;
        flashDelay = duration;
        StartCoroutine("InstantiateMemFlash");        
        startFog = fog.heightDensity;

        if (!extraDiminish)
        {
            memoryFog = fog.heightDensity * fogDiminishAmount;
        }
        
        else
        {
            memoryFog = fog.heightDensity * (fogDiminishAmount / 4.5f);
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
    void MemoryPlayingDelay()
    {
        EventManager.inst.memoryPlaying = false;
    }

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

    void LoadCredits(bool credits)
    {
        loadCredits = credits;
    }

    void CutToCredits()
    {
        Application.LoadLevel("Credits");
    }
    #endregion
}



