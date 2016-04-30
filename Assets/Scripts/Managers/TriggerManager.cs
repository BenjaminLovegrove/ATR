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

    [Header("Trigger Settings")]
    bool triggered = false;

    // The duration in which the memory lasts is equal...
    // ...to the length of the corresponding dialogue audio.
    // The unique memory encounter is set via the int.
    // The int values will be unique to each scene.
    [Header("Memory")]
    public int subtitleNum;
    public bool memory;
    public bool disableControls;
    public bool nightTime = false;
    public bool extraDiminish = false;
    public int memoryEventNumber;
    public float objFadeTimer;
    public AudioClip memoryDialogue;
    public GameObject memoryObj;    
	public GameObject[] switchObjects;    
    public AudioClip newBGM;
    public float endEarlyTimer;
    private float memoryDuration;
    private AudioSource dialogueAudio;
    private bool startTimer = false;

    [Header("Fog Change")]
    public bool fogChange;
    public float targFog;
    private GlobalFog fog;
    private float startFog;
    private float fogLerp = 1;

    [Header("Enemy Patrol Start")]
    public bool enemyTrigger;
    public GameObject enemy;

	[Header("Checkpoint")]
	public bool checkpoint;
    public int checkpointNo;

    [Header("End Level")]
    public bool endLevel;
    public LevelSelect levelSelect;
    public GameObject loadScreenUI; // There is a load screen prefab
    private string setLevel;

    [Header("Credits")]
    public bool credits;
    public MemLookMovement endMemLerp;
    private GameObject exhaustion;

    // Load next scene and display load screen UI
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

    void Awake()
    {
        if (EventManager.inst.currentLevel == "Coast Ending")
        {
            exhaustion = GameObject.Find("Exhaustion");
            endMemLerp = GameObject.Find("Memlookat").GetComponent<MemLookMovement>();
        }
        
        dialogueAudio = GameObject.Find("MemoryDialogue").GetComponent<AudioSource>();
        fog = GameObject.Find("Player").GetComponentInChildren<GlobalFog>();

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

    void Start()
    {        
        //Disable enemies to be later enabled
        if (enemy != null)
        {
            enemy.SetActive(false);
        }
    }

    void OnTriggerEnter (Collider col)
    {
	    if (col.gameObject.tag == "Player" && !triggered)
        {
            triggered = true;

            // Credits
            if (credits)
            {
                exhaustion.GetComponent<Exhaustion>().enabled = false;
                EventManager.inst.credits = true;
                col.BroadcastMessage("LoadCredits", true);
                endMemLerp.enabled = true;
            }

            // Memory
            if (memory && memoryEventNumber == EventManager.inst.currentMemory)
            {
                EventManager.inst.subtitleNum = subtitleNum;
                EventManager.inst.memoryPlaying = true;
                dialogueAudio.clip = memoryDialogue;
                dialogueAudio.Play();
                memoryDuration = memoryDialogue.length - endEarlyTimer;
                col.BroadcastMessage("EnterMemory", memoryDuration);
                col.BroadcastMessage("NightCheck", nightTime);
                col.BroadcastMessage("SetSwitch", switchObjects);
                col.BroadcastMessage("ExtraDim", extraDiminish);

                if (disableControls)
                {
                    col.BroadcastMessage("DisableControls", true);
                }

                if (newBGM != null)
                {
                    col.BroadcastMessage("SetBGM", newBGM);
                }

                if (memoryObj != null)
                {
                    memoryObj.SetActive(true);
                    startTimer = true;
                }

                Invoke("DelayedMemoryPlaying", 2f);
            }

            // Checkpoint
            if (checkpoint)
            {
                print("Checkpoint Triggered");
                EventManager.inst.currentCheckPoint = checkpointNo;
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

    void DelayedMemoryPlaying()
    {
        EventManager.inst.memoryPlaying = true;
    }
}
