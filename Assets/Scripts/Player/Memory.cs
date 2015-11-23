using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

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

    void Start()
    {
        bloom = gameObject.GetComponent<BloomAndFlares>();
        fog = gameObject.GetComponent<GlobalFog>();

        startBloom = bloom.bloomIntensity;
        startFog = fog.heightDensity;
    }

	void Update () {
	
        if (Input.GetKeyDown(KeyCode.M))
        {
            fadeTimer = 0;
            memoryLength = 5f;
            //memoryLength = clip.length;
        }

        if (memoryLength > 0)
        {
            EnterMemory();
        }

        if (memoryLength < 0 && bloom.bloomIntensity > startBloom)
        {
            ExitMemory();
        }

    }

    void EnterMemory ()
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
}


