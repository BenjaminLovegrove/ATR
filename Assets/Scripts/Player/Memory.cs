using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

// Script for handling in game memory events

public class Memory : MonoBehaviour
{
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

    private bool memoryPlaying = false;

    public AudioClip[] memoryDialogue;

    private float delayTimer;

    void Start()
    {
        bloom = gameObject.GetComponent<BloomAndFlares>();
        fog = gameObject.GetComponent<GlobalFog>();

        startBloom = bloom.bloomIntensity;
        startFog = fog.heightDensity;
    }

	void FixedUpdate ()
    {
        MemoryTimer();

        // Hack to test memories
        if (Input.GetKeyDown(KeyCode.M))
        {
            print("Memory Triggered");            
            memoryPlaying = true;
            fadeTimer = 0;
            memoryLength = 5f;
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

    void EnterMemory (float duration)
    {
        startFog = fog.heightDensity;
        memoryFog = fog.heightDensity / 7.5f;

        print("Memory receiver triggered on Memory script");
        EventManager.inst.controlsDisabled = true;
        memoryPlaying = true;        
        fadeTimer = 0;
        memoryLength = duration;
    }

    void MemoryLerp()
    {
        if (fadeTimer < 1)
        {
            fadeTimer += Time.deltaTime / fadeTime;
        }
        memoryLength -= Time.deltaTime * 0.75f;

        bloom.bloomIntensity = Mathf.Lerp(startBloom, memoryBloom, fadeTimer);
        fog.heightDensity = Mathf.Lerp(startFog, memoryFog, fadeTimer);

    }

    void ExitMemory()
    {
        fadeTimer -= Time.deltaTime / fadeTime;
        bloom.bloomIntensity = Mathf.Lerp(startBloom, memoryBloom, fadeTimer);
        fog.heightDensity = Mathf.Lerp(startFog, memoryFog, fadeTimer);
    }

    // Once the delay timer reaches the value of the memory length, the script will end
    void MemoryTimer()
    {
        if (memoryPlaying)
        {
            EventManager.inst.controlsDisabled = true;
            delayTimer += Time.deltaTime;
        }

        if (delayTimer > memoryLength)
        {
            EventManager.inst.controlsDisabled = false;
            delayTimer = 0;
            memoryPlaying = false;
        }
    }
}


