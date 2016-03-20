using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Water;

// Script for handling in game memory events

public class Memory : MonoBehaviour
{
    private GameObject[] waterObjs;
    private Water[] water;
    private BloomAndFlares bloom;
    private GlobalFog fog;
    public Terrain myTerrain;
    private AudioSource dialogueAudio;

    private float startBloom;
    public float memoryBloom = 1f;
    private float startFog;
    float memoryFog;
    public float fogDiminishAmount = 0.1f;
    private bool extraDiminish = false;

    public float fadeTime = 4f;

    private float fadeTimer;
    private float memoryTotalLength = 0f;
    private float memoryLength = 0f;

    private bool memoryPlaying = false;
	public GameObject[] switchMe;

    public AudioClip[] memoryDialogue;
    private AudioSource bgmSource;
    private AudioClip newBGM;
    private float bgmMaxVol;
    private float bgmLerp = 0;

    private float delayTimer;
    public Image memoryFlashObj;
    public float flashDelay;

    bool nightTime = false;
    GameObject skySphere;
    Light sceneLighting;


    // Spawn memory flash game obj co-routine
    IEnumerator InstantiateMemFlash()
    {
        EventManager.inst.memoryPlaying = true;
        memoryFlashObj.CrossFadeAlpha(255, 1, false);
        yield return new WaitForSeconds(2f);
        StartMemory();
        yield return new WaitForSeconds(flashDelay - 1.5f);
        memoryFlashObj.CrossFadeAlpha(255, 1, false);
        yield return new WaitForSeconds(2);
        EndMemory();
    }

    IEnumerator SkipMemory()
    {
        memoryPlaying = false;
        memoryLength = 2;
        dialogueAudio.Stop();
        memoryFlashObj.CrossFadeAlpha(255, 1, false);
        yield return new WaitForSeconds(2);
        EndMemory();
    }

    void StartMemory()
    {
        if (waterObjs != null)
        {
            foreach (Water w in water)
            {
                w.WaterReflections(true);
            }
        }

        if (myTerrain != null)
        {
            myTerrain.treeDistance = 150;
        }
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
        if (nightTime)
        {
            sceneLighting.intensity = sceneLighting.intensity * 0.25f;
            RenderSettings.fog = false;
            skySphere.SetActive(false);
        }

        memoryFlashObj.CrossFadeAlpha(0, 1, false);
    }

    void EndMemory()
    {
        EventManager.inst.memoryPlaying = false;
        bgmSource.Play();
        bgmLerp = 1;
        if (newBGM != null)
        {
            bgmSource.clip = newBGM;
            newBGM = null;
        }

        if (waterObjs != null)
        {
            foreach (Water w in water)
            {
                w.WaterReflections(false);
            }
        }
        if (myTerrain != null)
        {
            myTerrain.treeDistance = 100;
        }
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
        if (nightTime)
        {
            sceneLighting.intensity = sceneLighting.intensity / 0.25f;
            RenderSettings.fog = true;
            skySphere.SetActive(true);
        }


        memoryFlashObj.CrossFadeAlpha(0, 1, false);
    }

    void Start()
    {
        //Get all water objects
        waterObjs = GameObject.FindGameObjectsWithTag("Water");
        if (waterObjs != null)
        {
            water = new Water[waterObjs.Length];
            for (int i = 0; i < waterObjs.Length; i++)
            {
                water[i] = waterObjs[i].GetComponent<Water>();
            }
        }

        //Get stuff
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

    void NightCheck(bool check)
    {
        nightTime = check;
    }
    
	void FixedUpdate ()
    {
        MemoryTimer();
        MemoryTest(); // *** Comment this out of release builds ***

        //Skip memory
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopCoroutine("InstantiateMemFlash");
            StartCoroutine("SkipMemory");
        }
    }

    // Hack to test memories
    void MemoryTest()
    {        
        if (Input.GetKeyDown(KeyCode.M) && EventManager.inst.developerMode)
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

        if (!extraDiminish)
        {
            memoryFog = fog.heightDensity * fogDiminishAmount;
        } else
        {
            memoryFog = fog.heightDensity * (fogDiminishAmount * 1.5f) ;
            extraDiminish = false;
        }

        memoryPlaying = true;        
        fadeTimer = 0;
        memoryLength = duration;
        memoryTotalLength = duration;
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
            delayTimer += Time.fixedDeltaTime;
        }

        if (delayTimer > memoryTotalLength)
        {
            delayTimer = 0;
            memoryPlaying = false;
        }
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
}


