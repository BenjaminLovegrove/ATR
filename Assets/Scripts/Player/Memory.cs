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
    public float memoryDuration;
    public float memoryFog;
    public float fogDiminishAmount;
    public float memoryBloom;
    //private AudioSource fadingSource;

    [Header("Memory Objects")]
    public AudioClip[] memoryDialogue;
    public GameObject[] switchMe;
    public Terrain myTerrain;
    public Light sceneLighting;
    public Material originalSkyBox;
    public Material alternateSkyBox;
    public GameObject oilRigs;
    private Text subUI1;
    private Text subUI2;
    public string subString1;
    public string subString2;
    private Camera gameCam;
    private float skipLerp;
    private GameObject whiteVignette;
    
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

    private AudioSource dyingSFX;
    private AudioClip newBGM;
    private AudioSource bgmSource;
    private AudioSource dialogueAudio;
    private AudioSource breathingSource;
    private BloomAndFlares bloom;
    private GameObject[] waterObjs;
    private GameObject skySphere;    
    private GlobalFog fog;        
    private Image memoryFlashObj;
    private RawImage fadeToBlack;
    private Water[] water;
    private float dialogueVolume;
    private float bgmMaxVolume;
    private float breathingMaxVolume;
    public GameObject[] endSmokes;
    private AudioSource endMusic;
    public AudioSource exhaustionAudio;
    public float exhaustionMaxVol;
    private AudioSource ambienceAudio;
    private float ambienceAudioMaxVol;
    public AudioSource endingWind;
    private Image whiteBackDrop;

    // Display memory flash game obj coroutine
    IEnumerator InstantiateMemFlash()
    {
        memorySkippable = false;
        EventManager.inst.memoryPlaying = true;
        memoryFlashObj.CrossFadeAlpha(255, 1.75f, false);
        yield return new WaitForSeconds(1);
        if (whiteBackDrop != null)
        {
            whiteBackDrop.CrossFadeAlpha(0, 2f, false);
        }
        yield return new WaitForSeconds(0.25f);
        bgmSource.Pause();
        StartMemory();
        yield return new WaitForSeconds(memoryDuration - 3f);
        memoryFlashObj.CrossFadeAlpha(255, 1, false);
        yield return new WaitForSeconds(2);
        EndMemory();
    }

    // Skip memory coroutine
    IEnumerator SkipMemory()
    {
        memTimer = 2;
        memoryFlashObj.CrossFadeAlpha(255, 1, false);
        yield return new WaitForSeconds(2);
        EndMemory();
    }

    void Awake()
    {
        InitialiseValues();
    }

    void Update()
    {
        SkipMemoryCheck();
        FadeFOV();
    }

	void FixedUpdate()
    {
        FadeBGM();
        MemoryTimers();        
    }
    
    // Music fade in/out for memories
    void FadeBGM()
    {
        if (musicFadeOut)
        {
            if (!EventManager.inst.credits)
            {
                musicLerp += Time.deltaTime / 3;
            } else
            {
                musicLerp += Time.deltaTime / 3;
                exhaustionAudio.volume = Mathf.Lerp(exhaustionMaxVol, exhaustionMaxVol * 0.43f, musicLerp);
                endingWind.volume = Mathf.Lerp(1, 0, musicLerp);
            }
            bgmSource.volume = Mathf.Lerp(bgmMaxVolume, 0, musicLerp);
            breathingSource.volume = Mathf.Lerp(breathingMaxVolume, 0, musicLerp);
            ambienceAudio.volume = Mathf.Lerp(ambienceAudioMaxVol, 0, musicLerp);
            if (musicLerp > 1)
            {
                musicFadeOut = false;
            }
        }

        if (musicFadeIn && !EventManager.inst.credits)
        {
            musicLerp += Time.deltaTime / 4;
            bgmSource.volume = Mathf.Lerp(0, bgmMaxVolume, musicLerp);
            breathingSource.volume = Mathf.Lerp(0, breathingMaxVolume, musicLerp);
            ambienceAudio.volume = Mathf.Lerp(0, ambienceAudioMaxVol, musicLerp);
            if (musicLerp > 1)
            {
                musicFadeIn = false;
            }
        }

        if (skipLerp < 1)
        {
            skipLerp += Time.deltaTime / 4;
            dialogueAudio.volume = Mathf.Lerp(dialogueVolume, 0, skipLerp);
        }
    }

    // Fade fov in mems
    void FadeFOV()
    {
        if (EventManager.inst.memoryPlaying && delayTimer != 0)
        {
            if (!EventManager.inst.credits)
            {
                gameCam.fieldOfView = Mathf.Lerp(60, 75, delayTimer / memoryLength);
            } else
            {
                gameCam.fieldOfView = Mathf.Lerp(60, 85, delayTimer / memoryLength);
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
                //Invoke("CurrentMemoryIncrement", 2f); // The delay is required otherwise the camera will target the next memory look at obj
                skipLerp = 0;
                StopCoroutine("InstantiateMemFlash");
                StartCoroutine("SkipMemory");
                StopCoroutine("Subtitles" + EventManager.inst.subtitleNum);
                FadeTextOut1(0);
                FadeTextOut2(0);
            }
        }
    }

    // Begin memory
    void StartMemory()
    {
        dialogueAudio.volume = dialogueVolume;
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

        if (whiteVignette != null)
        {
            whiteVignette.SetActive(true);
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

        memoryFlashObj.CrossFadeAlpha(0, 2, false);
    }

    // End memory
    void EndMemory()
    {
        Invoke("CurrentMemoryIncrement", 2f);

        // Set controls back to normal
        disableControls = false;
        gameCam.ResetFieldOfView();
        if (!EventManager.inst.credits)
        {
            EventManager.inst.memoryLookScalar = 1;
            EventManager.inst.memoryMoveScalar = 1;
        }

        if (whiteVignette != null)
        {
            whiteVignette.SetActive(false);
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
            exhaustionAudio.Stop();
            breathingSource.Stop();
            foreach (GameObject smoke in endSmokes)
            {
                smoke.SetActive(true);
                smoke.GetComponent<ParticleSystem>().enableEmission = true;
            }
            endMusic.Play();
            //dyingSFX.Play();
            oilRigs.SetActive(true);
            RenderSettings.fogDensity = 0.001f;
            fog.heightDensity = 0.7f;
            fadeToBlack.CrossFadeAlpha(1, 25, false);
            Invoke("CutToCredits", 31.5f);
        }
    }

    // Sendmessage receiver to externally activate a memory
    // The float will determine the length of the memory
    // TriggerManager script will activate this function
    void EnterMemory (float duration)
    {
        musicLerp = 0;
        musicFadeOut = true;
        memoryDuration = duration;
        StartCoroutine("InstantiateMemFlash");
        if (EventManager.inst.currentLevel != "MainMenu")
        {
            StartCoroutine("Subtitles" + EventManager.inst.subtitleNum);
        } 
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
            EventManager.inst.memoryPlaying = false;
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

    void InitialiseValues()
    {
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

        if (EventManager.inst.currentLevel == "Coast Ending")
        {
            dyingSFX = GameObject.Find("DyingSFX").GetComponent<AudioSource>();
        }

        subUI1 = GameObject.Find("Subtitles1").GetComponent<Text>();
        subUI2 = GameObject.Find("Subtitles2").GetComponent<Text>();
        dialogueAudio = GameObject.Find("MemoryDialogue").GetComponent<AudioSource>();
        bgmSource = GameObject.Find("BackGroundMusicSource").GetComponent<AudioSource>();
        breathingSource = GameObject.Find("BreathingSFX").GetComponent<AudioSource>();
        sceneLighting = GameObject.Find("Directional Light").GetComponent<Light>();
        skySphere = GameObject.Find("skySphere");
        memoryFlashObj = GameObject.Find("MemoryFlashObj").GetComponent<Image>();

        if (EventManager.inst.currentLevel == "Coast Ending")
        {
            endMusic = GameObject.Find("CreditsMusic").GetComponent<AudioSource>();
        }

        gameCam = GameObject.Find("Camera").GetComponent<Camera>();
        bloom = gameObject.GetComponent<BloomAndFlares>();
        fog = gameObject.GetComponent<GlobalFog>();
        whiteVignette = GameObject.Find("WhiteVignette");

        if (whiteVignette != null)
        {
            whiteVignette.SetActive(false);
        }
        ambienceAudio = GameObject.Find("Ambience").GetComponent<AudioSource>();
        ambienceAudioMaxVol = ambienceAudio.volume;
        bgmMaxVolume = bgmSource.volume;
        breathingMaxVolume = breathingSource.volume;
        dialogueVolume = dialogueAudio.volume;
        startBloom = bloom.bloomIntensity;
        startFog = fog.heightDensity;
        skipLerp = 5;

        if (exhaustionAudio != null)
        {
            exhaustionMaxVol = exhaustionAudio.gameObject.GetComponent<Exhaustion>().maxVol;
        }

        if (EventManager.inst.currentLevel == "City Outskirts")
        {
            whiteBackDrop = GameObject.Find("WhiteBackDrop").GetComponent<Image>();
        }

        if (!EventManager.inst.firstPlay && whiteBackDrop != null)
        {
            whiteBackDrop.CrossFadeAlpha(0, 0.01f, false);
        }
    }

    #region Simple Functions
    void CurrentMemoryIncrement()
    {
        EventManager.inst.currentMemory++;
    }

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

    void FadeTextIn1(float duration)
    {
        subUI1.CrossFadeAlpha(1, duration, false);
    }

    void FadeTextOut1(float duration)
    {
        subUI1.CrossFadeAlpha(0, duration, false);
    }

    void FadeTextIn2(float duration)
    {
        subUI2.CrossFadeAlpha(1, duration, false);
    }

    void FadeTextOut2(float duration)
    {
        subUI2.CrossFadeAlpha(0, duration, false);
    }
    #endregion

    #region Subtitles
    public IEnumerator Subtitles1()
    {
        yield return new WaitForSeconds(3.2f);
        FadeTextOut1(0);
        FadeTextOut2(0);

        // John
        subString1 = "Hey";
        subUI1.text = subString1;
        FadeTextIn1(0.25f);
        yield return new WaitForSeconds(0.25f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(0.75f);

        // Alex
        subString2 = "...Hey you";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(0.25f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(0.75f);

        // John
        subString1 = "Going for a swim?";
        subUI1.text = subString1;
        FadeTextIn1(0.25f);
        yield return new WaitForSeconds(0.75f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Alex
        subString2 = "Haha! yeah, it’s totally not freezing right now or anything!";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(1.75f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "You're brave! I'll help you in!";
        subUI1.text = subString1;
        FadeTextIn1(0.25f);
        yield return new WaitForSeconds(0.75f);
        FadeTextOut1(0.5f);

        yield return new WaitForSeconds(0.5f);

        // Alex
        subString2 = "Haha! Stop!";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(2.75f);
        subString2 = "Hey John...?";
        subUI2.text = subString2;
        yield return new WaitForSeconds(0.25f);
        FadeTextOut2(1.5f);

        yield return new WaitForSeconds(1.5f);

        // John
        subString1 = "Yeah?";
        subUI1.text = subString1;
        FadeTextIn1(0.25f);
        yield return new WaitForSeconds(0.25f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(0.1f);

        // Alex
        subString2 = "Isn't it amazing here?";
        subUI2.text = subString2;
        yield return new WaitForSeconds(1f);
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(0.75f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(0.5f);

        // John
        subString1 = "I know what you mean, and it seems like Marcy loves it.";
        subUI1.text = subString1;
        FadeTextIn1(0.25f);
        yield return new WaitForSeconds(1.75f);
        FadeTextOut1(1.75f);

        yield return new WaitForSeconds(1.5f);

        // Alex
        subString2 = "Sure does...";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(1.5f);
        subString2 = "I mean.. What a place to grow up.";
        subUI2.text = subString2;
        yield return new WaitForSeconds(1f);
        FadeTextOut2(2);
    }

    public IEnumerator Subtitles2()
    {
        yield return new WaitForSeconds(7.25f);
        FadeTextOut1(0);
        FadeTextOut2(0);

        // Alex
        subString2 = "Hey";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(0.5f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(0.75f);

        // John
        subString1 = "Ah, you're still up.";
        subUI1.text = subString1;
        FadeTextIn1(0.75f);
        yield return new WaitForSeconds(0.75f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(0.5f);

        // Alex
        subString2 = "You must be pretty busy, it's really late.";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(2f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(0.6f);

        // John
        subString1 = "I know.";
        subUI1.text = subString1;
        FadeTextIn1(0.25f);
        yield return new WaitForSeconds(1f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1.25f);

        // Alex
        subString2 = "So.. the doctors today..";
        subUI2.text = subString2;
        FadeTextIn2(0.75f);
        yield return new WaitForSeconds(4f);
        subString2 = "apparently she’s getting worse..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3f);
        subString2 = "pretty quickl.. um.. it would be nice if you could be around a bit.. she wants..";
        subUI2.text = subString2;        
        yield return new WaitForSeconds(3.8f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(0.25f);

        // John
        subString1 = "I know.. I'll try, but work, again.";
        subUI1.text = subString1;
        FadeTextIn1(0.75f);
        yield return new WaitForSeconds(2.5f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(2.75f);

        // Alex
        subString2 = "John..  listen to her...";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(4.5f);
        subString2 = "I was thinking..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(2.6f);
        subString2 = "Maybe we could move somewhere! Away from the city!..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(2.75f);
        subString2 = "Somewhere on the the coast! The air would be cleaner… maybe that would help?";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(0.85f);

        // John
        subString1 = "What?! We can’t right now! Everything we have is here!";
        subUI1.text = subString1;
        FadeTextIn1(0.05f);
        yield return new WaitForSeconds(4.9f);
        subString1 = "You know what, I’m really tired, I’m heading to bed.";
        subUI1.text = subString1;
        yield return new WaitForSeconds(2f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Subtitles3()
    {
        yield return new WaitForSeconds(4.25f);
        FadeTextOut1(0);
        FadeTextOut2(0);

        // John
        subString1 = "Alex, talk to me..";
        subUI1.text = subString1;
        FadeTextIn1(0.25f);
        yield return new WaitForSeconds(1f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(0.5f);

        // Alex
        subString2 = "I... Uh...";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(2f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(0.5f);

        // John
        subString1 = "It’s been months like this… We can’t stay this way.";
        subUI1.text = subString1;
        FadeTextIn1(0.25f);
        yield return new WaitForSeconds(2f);
        FadeTextOut1(2);

        yield return new WaitForSeconds(2.5f);

        // Alex
        subString2 = "It’s 'been months'?! I’m not going to forget about everything,";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(3.3f);
        subString2 = "our daughter, because it’s 'been months'..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3f);
        subString2 = "I just.. I just need a while..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3.75f);
        subString2 = "you wouldn’t really understand.";
        subUI2.text = subString2;
        yield return new WaitForSeconds(2f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(0.5f);

        // John
        subString1 = "SHE... I loved her just as much as you did.. that’s why I always..";
        subUI1.text = subString1;
        FadeTextIn1(0.25f);
        yield return new WaitForSeconds(8f);
        subString1 = "you know what.. forget it.";
        subUI1.text = subString1;
        yield return new WaitForSeconds(2f);
        FadeTextOut1(2);

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Subtitles4()
    {
        yield return new WaitForSeconds(5.5f);
        FadeTextOut1(0);
        FadeTextOut2(0);

        // Alex
        subString2 = "John...";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(2.5f);
        subString2 = "I’ve been thinking..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(2.5f);
        subString2 = "I’m gonna leave.. you know, with everyone else..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(4.25f);
        subString2 = "I need change.";
        subUI2.text = subString2;
        yield return new WaitForSeconds(2f);
        FadeTextOut2(0.5f);

        yield return new WaitForSeconds(0.25f);

        // John
        subString1 = "What?! Who’s just willing to move on now?";
        subUI1.text = subString1;
        FadeTextIn1(0.1f);
        yield return new WaitForSeconds(2f);
        FadeTextOut1(2);

        yield return new WaitForSeconds(1f);

        // Alex
        subString2 = "John...";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(1f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(0.5f);

        // John
        subString1 = "I thought we said we wouldn’t, we said a long time ago we wanted to stay here,";
        subUI1.text = subString1;
        FadeTextIn1(0.25f);
        yield return new WaitForSeconds(4.5f);
        subString1 = "this is where our life and all our memories are..";
        subUI1.text = subString1;
        yield return new WaitForSeconds(2f);
        subString1 = "I don’t want to just leave here.";
        subUI1.text = subString1;
        yield return new WaitForSeconds(1f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1.5f);

        // Alex
        subString2 = "I wasn’t really asking.. We both need change.";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(3.5f);
        subString2 = "I think it’s best if, just I go for now...";
        subUI2.text = subString2;
        yield return new WaitForSeconds(5.25f);
        subString2 = "Alone... ";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);        
    }

    public IEnumerator Subtitles5()
    {
        yield return new WaitForSeconds(3.5f);
        FadeTextOut1(0);
        FadeTextOut2(0);

        // Guy
        subString2 = "So.. she just left?";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(1f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(0.5f);

        // John
        subString1 = "Yeah.. it was for the best though.. she deserved a fresh start.";
        subUI1.text = subString1;
        FadeTextIn1(0.25f);
        yield return new WaitForSeconds(4f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(3f);

        // Guy
        subString2 = "So, are you planning to go? Maybe you could still fix things?";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(3f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1.25f);

        // John
        subString1 = "I don’t know.. eventually I guess..";
        subUI1.text = subString1;
        FadeTextIn1(0.25f);
        yield return new WaitForSeconds(2.5f);
        subString1 = "Maybe I just need a bit more time.. and honestly,";
        subUI1.text = subString1;
        yield return new WaitForSeconds(3f);
        subString1 = "I’m not sure anything could take things back to how they were.";
        subUI1.text = subString1;
        yield return new WaitForSeconds(2.5f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1.25f);

        // Guy
        subString2 = "Well..";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(1.5f);
        subString2 = "don’t spend too long thinking about it...";
        subUI2.text = subString2;
        yield return new WaitForSeconds(1.75f);
        subString2 = "Just.. ";
        subUI2.text = subString2;
        yield return new WaitForSeconds(1.75f);
        subString2 = "who knows what’s going to happen in the next couple of years?";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3.5f);
        subString2 = "At this rate, everyone will be gone soon enough.";
        subUI2.text = subString2;
        yield return new WaitForSeconds(2.5f);
        subString2 = "I’m just saying..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(2f);
        subString2 = "you can’t rely on time to fix things..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(2.5f);
        subString2 = "time really just makes you..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(1.75f);
        subString2 = "accept how things are.";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3.5f);
        FadeTextOut2(1.5f);

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Subtitles6()
    {
        yield return new WaitForSeconds(3.5f);
        FadeTextOut1(0);
        FadeTextOut2(0);

        // Guy
        subString2 = "God, it’s even worse in the city..";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(2.5f);
        subString2 = "I can’t see shit!";
        subUI2.text = subString2;
        yield return new WaitForSeconds(1.5f);
        subString2 = "It hasn’t even been that long..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(1.5f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "Well I guess when no one’s around it’s easy to not care any more..";
        subUI1.text = subString1;
        FadeTextIn1(0.25f);
        yield return new WaitForSeconds(3.75f);
        subString1 = "It’s amazing how empty it suddenly is.";
        subUI1.text = subString1;
        yield return new WaitForSeconds(2f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Guy
        subString2 = "Honestly, I think something needs to change real soon.";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(2.5f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "Yeah, easier said than done. What difference could we make?";
        subUI1.text = subString1;
        FadeTextIn1(0.25f);
        yield return new WaitForSeconds(3f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Guy
        subString2 = "This smog..";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(1.75f);
        subString2 = "They really must just not care about the people here..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(2.75f);
        subString2 = "or even trying to save what’s left..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(2f);
        subString2 = "If this keeps up we’ll have to try something!..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(2.75f);
        subString2 = "..and who’s going to stop us?";
        subUI2.text = subString2;
        yield return new WaitForSeconds(2f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Subtitles7()
    {
        yield return new WaitForSeconds(11.75f);
        FadeTextOut1(0);
        FadeTextOut2(0);

        // Guy
        subString2 = "Look at these guys, what the hell is happening?";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(3.25f);
        subString2 = "There’s no chance of leaving now.";
        subUI2.text = subString2;
        yield return new WaitForSeconds(1f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1.25f);

        // John
        subString1 = "Do you think anyone knows what’s going on?";
        subUI1.text = subString1;
        FadeTextIn1(0.25f);
        yield return new WaitForSeconds(2.5f);
        subString1 = "They can’t stop us can they?";
        subUI1.text = subString1;
        yield return new WaitForSeconds(2f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(2f);

        // Guy
        subString2 = "Run!";
        subUI2.text = subString2;
        FadeTextIn2(0);
        yield return new WaitForSeconds(0.75f);
        FadeTextOut2(2);

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Subtitles8()
    {
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Subtitles9()
    {
        yield return new WaitForSeconds(4f);
        FadeTextOut1(0);
        FadeTextOut2(0);

        // Alex
        subString2 = "Hey.";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(0.5f);
        FadeTextOut2(1.5f);
        yield return new WaitForSeconds(1.25f);
        subString2 = "So you made it.";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(0.25f);
        FadeTextOut2(1.75f);
        yield return new WaitForSeconds(1.75f);
        subString2 = "What do you think?";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(0.25f);
        FadeTextOut2(1.75f);
        yield return new WaitForSeconds(1.75f);
        subString2 = "Think things could be..";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(0.25f);
        yield return new WaitForSeconds(1.25f);
        subString2 = "different?";
        subUI2.text = subString2;
        yield return new WaitForSeconds(0.25f);
        FadeTextOut2(1.25f);
        yield return new WaitForSeconds(1.25f);
        subString2 = "Do you think you could be happy?";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(0.25f);
        FadeTextOut2(2.75f);
        yield return new WaitForSeconds(2.75f);
        subString2 = "Are you thinking about all the things you wish you could go back and change?";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(0.25f);
        FadeTextOut2(5.25f);
        yield return new WaitForSeconds(5.25f);
        subString2 = "You can’t spend forever thinking about the past...";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(0.25f);
        FadeTextOut2(3f);
        yield return new WaitForSeconds(3f);
        subString2 = "You just have to do what you can now...";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(0.25f);
        FadeTextOut2(3.75f);
        yield return new WaitForSeconds(3.75f);
        subString2 = "We have to accept our mistakes.";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(0.25f);
        FadeTextOut2(1.75f);
        yield return new WaitForSeconds(1.75f);
        subString2 = "Learn from them...";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(0.25f);
        FadeTextOut2(1.75f);
        yield return new WaitForSeconds(1.75f);
        subString2 = "Be better because of them.";
        subUI2.text = subString2;
        FadeTextIn2(0.25f);
        yield return new WaitForSeconds(0.25f);
        yield return new WaitForSeconds(0.5f);
        FadeTextOut2(3);

        yield return new WaitForSeconds(1f);
    }
    #endregion
}



