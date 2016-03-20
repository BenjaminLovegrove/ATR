using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Handle global variables, such as player checkpoints and game states

public class EventManager : MonoBehaviour
{
    public static EventManager inst;

    [Header("Player Positions/Objects")]
	public Transform playerTrans;
    public Transform enemyKillPos;
    public Transform[] playerCheckPoints;
    public Transform flashSpawn;
	public GameObject playerObj;
    public GameObject pauseMenuButtons;
    public RawImage fadeToBlack;

    [Header("Player States")]
	public bool playerCrouch;
    public bool playerDead;
    public bool playerJump;
    
    public bool resetLevel;
    public bool controlsDisabled;
    public bool memoryPlaying;
    
    [Header("Settings")]
    public float lookSensitivity;
    public float masterVolume;
    public bool invertY;
    public float memoryLookScalar;
    public float memoryMoveScalar;

    [Header("Hacks")]
    public bool developerMode; // Toggle this to enable/disable cheats
    public bool invisMode;
    public bool increaseSpeed;

    [Header("Games States")]
    public bool gamePaused;
    public int currentCheckPoint;
    public int currentMemory;   // This value is automatically assigned when a memory is triggered
    public string currentLevel;

    // Temp vals and keys
    private float lookSensTemp;

    // Set a small delay before the player is granted control
    IEnumerator ControlDisableCoRoutine()
    {
        controlsDisabled = true;
        fadeToBlack.CrossFadeAlpha(0, 3, false);
        yield return new WaitForSeconds (1f);
        controlsDisabled = false;
    }

	void Awake ()
	{
        developerMode = true; // *** Disable for release builds ***
        //developerMode = false;
        
        // Check singleton status
		if (inst == null)
		{
			inst = this;
			DontDestroyOnLoad(gameObject);
		}
		else Destroy(this.gameObject);
	}

    void Start()
    {
        //StartCoroutine("ControlDisableCoRoutine"); // Uncomment this when playing a level directly in the editor, or manually untoggle controlsDisabled in the inspector

        // Load option settings
        masterVolume = PlayerPrefs.GetFloat("Master Volume");
        lookSensitivity = PlayerPrefs.GetFloat("Mouse Sensitivity");

        flashSpawn = GameObject.FindGameObjectWithTag("FlashSpawn").transform;
        Cursor.visible = true;

        lookSensTemp = PlayerPrefs.GetFloat("Mouse Sensitivity") * 0.2f;
    }

    void Update()
    {
        if (playerDead)
        {
            controlsDisabled = true;
        }

        // Scale look sensitivity during memories
        if (memoryPlaying)
        {
            lookSensitivity = lookSensTemp * memoryLookScalar;
        }
        else lookSensitivity = lookSensTemp;
    }

	void FixedUpdate ()
	{


        LevelResetCheck();

        AudioListener.volume = masterVolume;

        if (!gamePaused && pauseMenuButtons != null)
        {
            pauseMenuButtons.SetActive(false);
        }
	}

    // Check for external level reset
    void LevelResetCheck()
    {
        if (resetLevel)
        {
            ResetPlayer();
        }
    }
    
    // Reset the player to the last checkpoint
    // Place an empty gameobject named "PlayerCheckPoints" with child objects containing
    // the desired world positions of checkpoints (in order).
    // Use triggers to set the value of the last achieved checkpoint - (currentCheckPoint).
    void ResetPlayer()
    {
        playerDead = false;
        resetLevel = false;
        Application.LoadLevel(currentLevel);
    }

    // Populate EventManager data upon loading a scene
    void OnLevelWasLoaded()
    {
        StartCoroutine("ControlDisableCoRoutine");
        InitialiseValues();
        Invoke("MovePlayer", 0.05f); // This delay is required so the way point data can be fetched first
    }

    // Move the player to the current checkpoint location
    void MovePlayer()
    {        
        playerTrans.position = playerCheckPoints[currentCheckPoint].position;
    }

    void InitialiseValues()
    {
        playerDead = false;
        resetLevel = false;

        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        playerObj = GameObject.Find("Player");
        pauseMenuButtons = GameObject.FindWithTag("PauseMenuObj");
        playerCheckPoints = GameObject.Find("PlayerCheckPoints").GetComponentsInChildren<Transform>();
        flashSpawn = GameObject.FindGameObjectWithTag("FlashSpawn").transform;
    }
}
