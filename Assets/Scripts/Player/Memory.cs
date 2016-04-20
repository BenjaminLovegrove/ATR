﻿using UnityEngine;
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
    private Text subUI1;
    private Text subUI2;
    public string subString1;
    public string subString2;
    
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

        subUI1 = GameObject.Find("Subtitles1").GetComponent<Text>();
        subUI2 = GameObject.Find("Subtitles2").GetComponent<Text>();
        dialogueAudio = GameObject.Find("MemoryDialogue").GetComponent<AudioSource>();
        bgmSource = GameObject.Find("BackGroundMusicSource").GetComponent<AudioSource>();
        breathingSource = GameObject.Find("BreathingSFX").GetComponent<AudioSource>();
        sceneLighting = GameObject.Find("Directional Light").GetComponent<Light>();
        skySphere = GameObject.Find("skySphere");
        memoryFlashObj = GameObject.Find("MemoryFlashObj").GetComponent<Image>();
        bloom = gameObject.GetComponent<BloomAndFlares>();
        fog = gameObject.GetComponent<GlobalFog>();

        bgmMaxVolume = bgmSource.volume;
        breathingMaxVolume = breathingSource.volume;
        dialogueVolume = dialogueAudio.volume;
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
        if (musicFadeOut)
        {
            musicLerp += Time.deltaTime / 2;
            bgmSource.volume = Mathf.Lerp(bgmMaxVolume, 0, musicLerp);
            breathingSource.volume = Mathf.Lerp(breathingMaxVolume, 0, musicLerp);
            if (musicLerp > 1)
            {
                musicFadeOut = false;
            }
        }

        if (musicFadeIn)
        {
            musicLerp += Time.deltaTime / 4;
            bgmSource.volume = Mathf.Lerp(0, bgmMaxVolume, musicLerp);
            breathingSource.volume = Mathf.Lerp(0, breathingMaxVolume, musicLerp);
            dialogueAudio.volume = Mathf.Lerp(dialogueVolume, 0, musicLerp);
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
        StartCoroutine("Subtitles" + EventManager.inst.subtitleNum);

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
            foreach (GameObject smoke in endSmokes)
            {
                smoke.SetActive(true);
                smoke.GetComponent<ParticleSystem>().enableEmission = true;
            }
            oilRigs.SetActive(true);
            RenderSettings.fogDensity = 0.001f;
            fog.heightDensity = 0.7f;
            fadeToBlack.CrossFadeAlpha(1, 30, false);
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

    void FadeTextIn1(float duration)
    {
        subUI1.CrossFadeAlpha(1, duration / 3, false);
    }

    void FadeTextOut1(float duration)
    {
        subUI1.CrossFadeAlpha(0, duration, false);
    }

    void FadeTextIn2(float duration)
    {
        subUI2.CrossFadeAlpha(1, duration / 3, false);
    }

    void FadeTextOut2(float duration)
    {
        subUI2.CrossFadeAlpha(0, duration, false);
    }
    #endregion

    public IEnumerator Subtitles1()
    {
        yield return new WaitForSeconds(1f);
        FadeTextOut1(0);
        FadeTextOut2(0);

        // John
        subString1 = "Hey";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(1f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Alex
        subString2 = "...Hey you";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(1f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "Going for a swim?";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(1f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Alex
        subString2 = "Haha! yeah, it’s totally not freezing right now or anything";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(2f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "You're brave! I'll help you in";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(1f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Alex
        subString2 = "Haha! Stop! Hey John...?";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(1f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "Yeah?";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(1f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Alex
        subString2 = "Isn't it amazing here?";
        subUI2.text = subString2;
        yield return new WaitForSeconds(1f);
        FadeTextIn2(1);
        yield return new WaitForSeconds(1f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "I know what you mean, and Marcy clearly loves it";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(2f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Alex
        subString2 = "Sure does... What a place to grow up";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(2f);
        FadeTextOut2(1);
    }

    public IEnumerator Subtitles2()
    {
        yield return new WaitForSeconds(1f);
        FadeTextOut1(0);
        FadeTextOut2(0);

        // Alex
        subString2 = "Hey";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(1f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "Ah, you're still up";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(1f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Alex
        subString2 = "You must be pretty busy, it's really late";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(2f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "I know, same as always";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(1f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Alex
        subString2 = "So.. the doctors today..";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(3f);
        subString2 = "apparently she’s getting worse, pretty quickl.. um..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3f);
        subString2 = "it would be nice if you could be around a bit.. she wan..";
        subUI2.text = subString2;        
        yield return new WaitForSeconds(3f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "I know.. I'll try, but work again";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(1f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Alex
        subString2 = "John..  listen to her…. I was thinking..";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(3f);
        subString2 = "Maybe we could move somewhere! Away from the city!..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3f);
        subString2 = "Somewhere on the the coast? The air would be cleaner… maybe that would help";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "What?? We can’t right now! Everything we have is here…";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(3f);
        subString1 = "You know what, I’m really tired, I’m heading to bed";
        subUI1.text = subString1;
        yield return new WaitForSeconds(2f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Subtitles3()
    {
        yield return new WaitForSeconds(1f);
        FadeTextOut1(0);
        FadeTextOut2(0);

        // John
        subString1 = "Alex, talk to me";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(1f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Alex
        subString2 = "I... Uh...";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(2f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "It’s been months like this… We can’t stay this way";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(1f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Alex
        subString2 = "It’s been months? I’m not going to forget about everything,";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(3f);
        subString2 = "our daughter, because it’s been months .. I just.. I need a while..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(2f);
        subString2 = "you wouldn’t really understand";
        subUI2.text = subString2;
        yield return new WaitForSeconds(2f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "SHE... I loved her just as much as you did, that’s why I always...";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(3f);
        subString1 = "you know what.. forget it";
        subUI1.text = subString1;
        yield return new WaitForSeconds(1f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Subtitles4()
    {
        yield return new WaitForSeconds(1f);
        FadeTextOut1(0);
        FadeTextOut2(0);

        // Alex
        subString2 = "John... I’ve been thinking.. I’m gonna leave.. you know, with everyone else.. I need change";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(4f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "What?! Who’s just willing to move on now?";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(2f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Alex
        subString2 = "John...";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(1f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "I thought we said we wouldn’t, we said a long time ago we wanted to stay here,";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(3.5f);
        subString1 = "this is where our life and all our memories are...";
        subUI1.text = subString1;
        yield return new WaitForSeconds(2f);
        subString1 = "I don’t want to just leave here";
        subUI1.text = subString1;
        yield return new WaitForSeconds(2f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Alex
        subString2 = "I... wasn’t really asking... We both need a change,";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(3f);
        subString2 = "I think it’s best if, just I go for now... alone... ";
        subUI2.text = subString2;        
        yield return new WaitForSeconds(3f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);        
    }

    public IEnumerator Subtitles5()
    {
        yield return new WaitForSeconds(1f);
        FadeTextOut1(0);
        FadeTextOut2(0);

        // Guy
        subString2 = "So.. she just left?";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(2f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "Yeah.. it’s for best though.. She deserved a fresh start";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(3f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Guy
        subString2 = "So, are you planning to go? Maybe you could still fix things?";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(3f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "I don’t know.. eventually I guess..";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(2f);
        subString1 = "Maybe I just need a bit more time.. and honestly,";
        subUI1.text = subString1;
        yield return new WaitForSeconds(3f);
        subString1 = "I’m not sure anything could take things back to how they were";
        subUI1.text = subString1;
        yield return new WaitForSeconds(3f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Guy
        subString2 = "Well.. don’t spend too long thinking about it...";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(3f);
        subString2 = "Just.. who knows what’s going to happen in the next couple of years?";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3f);
        subString2 = "At this rate, everyone will be gone soon enough. I’m just saying,";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3f);
        subString2 = "you can’t rely on time to fix things, time really just makes you, accept how things are";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3.5f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Subtitles6()
    {
        yield return new WaitForSeconds(1f);
        FadeTextOut1(0);
        FadeTextOut2(0);

        // Guy
        subString2 = "God, it’s even worse in the city, I can’t see shit.. It hasn’t even been that long";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(4f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "Well I guess when no one’s around it’s easy to not care any more..";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(3f);
        subString1 = "It’s amazing how empty it suddenly is";
        subUI1.text = subString1;
        yield return new WaitForSeconds(2f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Guy
        subString2 = "Honestly, I think something needs to change real soon";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(2.5f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "Yeah, easier said than done though. What difference could we make";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(3f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Guy
        subString2 = "This smog.. They really must just not care about the people here,";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(3f);
        subString2 = "or even trying to save what’s left.. If this keeps up we’ll have to try something..";
        subUI2.text = subString2;
        yield return new WaitForSeconds(4f);
        subString2 = "and who’s going to stop us?";
        subUI2.text = subString2;
        yield return new WaitForSeconds(2f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Subtitles7()
    {
        yield return new WaitForSeconds(1f);
        FadeTextOut1(0);
        FadeTextOut2(0);

        // Guy
        subString2 = "Shh.. Look at these guys, what the hell is happening?";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(3f);
        subString2 = "There’s no chance of leaving now";
        subUI2.text = subString2;
        yield return new WaitForSeconds(2f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // John
        subString1 = "Do you think anyone knows what’s going on? Can’t stop us can they?";
        subUI1.text = subString1;
        FadeTextIn1(1);
        yield return new WaitForSeconds(3f);
        FadeTextOut1(1);

        yield return new WaitForSeconds(1f);

        // Guard
        subString2 = "*Hey you there!*";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(1.5f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);

        // Guy
        subString2 = "Run!";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(1f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Subtitles8()
    {
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Subtitles9()
    {
        yield return new WaitForSeconds(1f);
        FadeTextOut1(0);
        FadeTextOut2(0);

        // Alex
        subString2 = "Hey, so you made it. What do you think?";
        subUI2.text = subString2;
        FadeTextIn2(1);
        yield return new WaitForSeconds(3f);
        subString2 = "Think things could be different?";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3f);
        subString2 = "Do you think you could be happy?";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3f);
        subString2 = "Are you thinking about all the things you wish you could go back and change?";
        subUI2.text = subString2;
        yield return new WaitForSeconds(5f);
        subString2 = "You can’t spend forever thinking about the past...";
        subUI2.text = subString2;
        yield return new WaitForSeconds(4f);
        subString2 = "You just have to do what you can now...";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3f);
        subString2 = "We have to accept our mistakes, learn from them...";
        subUI2.text = subString2;
        yield return new WaitForSeconds(4f);
        subString2 = "Be better because of them";
        subUI2.text = subString2;
        yield return new WaitForSeconds(3f);
        FadeTextOut2(1);

        yield return new WaitForSeconds(1f);
    }

}



