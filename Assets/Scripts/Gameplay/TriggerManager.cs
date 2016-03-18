using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

// Script to handle trigger events that can occur throughout the game
// Set appropriate bools on the corresponding game object

public class TriggerManager : MonoBehaviour
{
    public enum LevelSelect
    {
        ONE, TWO, THREE
    }

    public AudioSource audio;

    [Header("Trigger Settings")]
    bool triggered = false;

    // The duration in which the memory lasts is equal...
    // ...to the length of the corresponding dialogue audio.
    // The unique memory encounter is set via the int.
    // The int values will be unique to each scene.
    [Header("Memory Trigger")]
    public bool memory;
    private float memoryDuration;
    public int memoryEventNumber;
    public AudioClip[] memoryDialogue;
    public GameObject memoryObj;
    public float objFadeTimer;
	public GameObject[] switchObjects;
    private bool startTimer = false;
    public bool nightTime = false;
    public AudioClip newBGM;
    public bool extraDiminish = false;

    [Header("Fog Change")]
    public bool fogChange;
    public float targFog;
    private GlobalFog fog;
    private float startFog;
    private float fogLerp = 1;

    [Header("Enemy Patrol Start Trigger")]
    public bool enemyTrigger;
    public GameObject enemy;

	[Header("Checkpoint")]
	public bool checkpoint;

    [Header("End Level")]
    public bool endLevel;
    public LevelSelect levelSelect;
    public GameObject loadScreenUI;
    private string setLevel;

    IEnumerator LoadNextScene()
    {
        loadScreenUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        AsyncOperation async = Application.LoadLevelAsync(EventManager.inst.currentLevel);
        while (!async.isDone)
        {
            yield return null;
        }
    }

    void Start()
    {
        fog = GameObject.Find("Player").GetComponentInChildren<GlobalFog>();
        audio = GameObject.Find("Player").GetComponent<AudioSource>();
        
        //Disable enemies to be later enabled
        if (enemy != null)
        {
            enemy.SetActive(false);
        }

        // Assign string value for each level
        switch (levelSelect)
        {
            case LevelSelect.ONE:
                setLevel = "City Outskirts";
                break;

            case LevelSelect.TWO:
                setLevel = "City";
                break;

            case LevelSelect.THREE:
                setLevel = "Coast Ending";
                break;
        }
    }

    void OnTriggerEnter (Collider col)
    {
	    if (col.gameObject.tag == "Player" && !triggered)
        {
			triggered = true;

            // Memory
            if (memory)
            {
                EventManager.inst.currentMemory = memoryEventNumber;

                audio.PlayOneShot(memoryDialogue[memoryEventNumber]);
                memoryDuration = memoryDialogue[memoryEventNumber].length;
                col.BroadcastMessage("EnterMemory", memoryDuration);
                col.BroadcastMessage("NightCheck", nightTime);
                col.BroadcastMessage("SetSwitch", switchObjects);
                col.BroadcastMessage("ExtraDim", extraDiminish);
                if (newBGM != null)
                {
                    col.BroadcastMessage("SetBGM", newBGM);
                }

                if (memoryObj != null)
                {
                    memoryObj.SetActive(true);
                    startTimer = true;
                }
             }

            // Checkpoint
			if (checkpoint)
            {
                print("Checkpoint Triggered");
				EventManager.inst.currentCheckPoint ++;
			}

            // Enemy
            if (enemyTrigger)
            {
                print("Enemy Triggered");
                enemy.SetActive(true);
            }

            // Fog
            if (fogChange)
            {
                print("Fog Triggered");
                startFog = fog.heightDensity;
                fogLerp = 0;
            }

            // End Level
            if (endLevel)
            {
                print("Level Complete");
                EventManager.inst.currentLevel = setLevel;
                EventManager.inst.currentCheckPoint = 0;
                if (loadScreenUI != null)
                {
                    loadScreenUI.SetActive(true);
                }
                StartCoroutine("LoadNextScene");
            }
        }
	}

    void FixedUpdate()
    {
        // Change fog
        if (fogLerp < 1)
        {
            fogLerp += Time.deltaTime / 4;
            fog.heightDensity = Mathf.Lerp(startFog, targFog, fogLerp);
        }
        
        // Countdown the fade timer on memory objects
        if (startTimer)
        {
            objFadeTimer -= Time.deltaTime;

            if (objFadeTimer <= 0f)
            {
                memoryObj.SendMessage("FadeOutReceiver");
            }
        }

    }
}
