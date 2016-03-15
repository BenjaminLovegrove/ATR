using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;
using UnityEngine.UI;

// Script for handling in game memory events

public class Memory : MonoBehaviour
{
    private BloomAndFlares bloom;
    private GlobalFog fog;

    private float startBloom;
    public float memoryBloom = 1f;
    private float startFog;
    float memoryFog;
    public float fogDiminishAmount = 0.1f;

    public float fadeTime = 4f;

    private float fadeTimer;
    private float memoryLength = 0f;

    private bool memoryPlaying = false;
	private GameObject[] switchMe;

    public AudioClip[] memoryDialogue;

    private float delayTimer;
    public Image memoryFlashObj;
    public float flashDelay;

    bool nightTime = false;
    GameObject skySphere;
    Light sceneLighting;


    // Spawn memory flash game obj co-routine
    IEnumerator InstantiateMemFlash()
    {
        memoryFlashObj.CrossFadeAlpha(255, 1, false);
        yield return new WaitForSeconds(1.25f);

        if (switchMe != null)
        {
            foreach (GameObject obj in switchMe)
            {
                obj.SetActive(false);
            }
        }
        if (nightTime)
        {
            sceneLighting.intensity = sceneLighting.intensity * 0.1f;
            RenderSettings.fog = false;
            skySphere.SetActive(false);
        }

        memoryFlashObj.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(flashDelay);
        memoryFlashObj.CrossFadeAlpha(255, 1, false);
        yield return new WaitForSeconds(1.25f);

        if (switchMe != null)
        {
            foreach (GameObject obj in switchMe)
            {
                obj.SetActive(true);
            }
        }
        if (nightTime)
        {
            sceneLighting.intensity = sceneLighting.intensity / 0.1f;
            RenderSettings.fog = true;
            skySphere.SetActive(true);
        }


        memoryFlashObj.CrossFadeAlpha(0, 1, false);
    }

    void Start()
    {
        sceneLighting = GameObject.Find("Directional Light").GetComponent<Light>();
        skySphere = GameObject.Find("skySphere");
        memoryFlashObj = GameObject.Find("MemoryFlashObj").GetComponent<Image>();
        bloom = gameObject.GetComponent<BloomAndFlares>();
        fog = gameObject.GetComponent<GlobalFog>();

        startBloom = bloom.bloomIntensity;
        startFog = fog.heightDensity;
    }

    void NightCheck(bool check)
    {
        nightTime = check;
    }

	void FixedUpdate ()
    {
        MemoryTimer();
        MemoryTest(); // *** Comment this out of release builds ***
    }

    // Hack to test memories
    void MemoryTest()
    {        
        if (Input.GetKeyDown(KeyCode.M))
        {
            print("Memory Triggered");
            memoryPlaying = true;
            fadeTimer = 0;
            memoryLength = 5f;
            flashDelay = memoryLength;
            StartCoroutine("InstantiateMemFlash");
        }

        if (memoryLength > 0)
        {
            MemoryLerp();
        }

        // Exit memory
        if (memoryLength < 0 && bloom.bloomIntensity > startBloom)
        {
            ExitMemory();
        }
    }

    // Sendmessage receiver to externally activate a memory
    // The float will determine the length of the memory
    void EnterMemory (float duration)
    {
        flashDelay = duration;
        StartCoroutine("InstantiateMemFlash");
        
        startFog = fog.heightDensity;
        memoryFog = fog.heightDensity * fogDiminishAmount;


        EventManager.inst.controlsDisabled = true;
        memoryPlaying = true;        
        fadeTimer = 0;
        memoryLength = duration;
    }

	void SetSwitches(GameObject[] switchObjects){
		switchMe = switchObjects;
	}

    void MemoryLerp()
    {
        if (fadeTimer < 1)
        {
            fadeTimer += Time.deltaTime / fadeTime;
        }
        memoryLength -= Time.deltaTime;

        bloom.bloomIntensity = Mathf.Lerp(startBloom, memoryBloom, fadeTimer);
        fog.heightDensity = Mathf.Lerp(startFog, memoryFog, fadeTimer);
    }

    // Exit memory
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


