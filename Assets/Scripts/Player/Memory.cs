using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

// Script for handling in game memory events

public class Memory : MonoBehaviour {

    private BloomAndFlares bloom;
    private GlobalFog fog;

    private float startBloom;
    public float memoryBloom = 1f;
    private float startFog;
    public float memoryFog = 0.5f;

    public float fadeTime = 4f;
    public float bufferTime;

    private float fadeTimer;
    private float memoryLength = 0f;

    // these vars might duplicate what you already have
    // I'll leave you to mess with them if you want, otherwise let me know
    private float delayTimer;
    private bool delayEnable;

    void Start()
    {
        bloom = gameObject.GetComponent<BloomAndFlares>();
        fog = gameObject.GetComponent<GlobalFog>();

        startBloom = bloom.bloomIntensity;
        startFog = fog.heightDensity;
    }

	void FixedUpdate ()
    {
        RemoveControls();
	
        if (Input.GetKeyDown(KeyCode.M))
        {
            fadeTimer = 0;
            memoryLength = 5f;
            //memoryLength = clip.length;
        }

        if (memoryLength > 0)
        {
            MemoryLerp();
        }

        if (memoryLength < 0 && bloom.bloomIntensity > startBloom)
        {
            ExitMemory();
        }

    }

    void EnterMemory ()
    {
        fadeTimer = 0;
        memoryLength = 5f;
        //memoryLength = clip.length;
    }

    void MemoryLerp()
    {
        if (fadeTimer < 1)
        {
            fadeTimer += Time.deltaTime / fadeTime;
        }
        memoryLength -= Time.deltaTime / (memoryLength + bufferTime);

        // if (fadeTimer < (1 - ((bufferTime/2)/memoryLength)) && !memoryclip.isPlaying)
        // {
        //    playclip;
        // }

        bloom.bloomIntensity = Mathf.Lerp(startBloom, memoryBloom, fadeTimer);
        fog.heightDensity = Mathf.Lerp(startFog, memoryFog, fadeTimer);

    }

    void ExitMemory()
    {
        fadeTimer -= Time.deltaTime / fadeTime;
        bloom.bloomIntensity = Mathf.Lerp(startBloom, memoryBloom, fadeTimer);
        fog.heightDensity = Mathf.Lerp(startFog, memoryFog, fadeTimer);
    }

    // Set your duration of delayTimer then set delayEnable to true
    // delaytimer = 7.43f;
    // delayEnable = true;
    void RemoveControls()
    {
        if (delayEnable)
        {
            EventManager.inst.controlsDisabled = true;
            delayTimer -= Time.deltaTime;
        }

        if (delayTimer < 0)
        {
            EventManager.inst.controlsDisabled = false;
        }
    }
}


