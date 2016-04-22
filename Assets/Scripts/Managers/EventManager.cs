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
	public GameObject playerObj;

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
    public float prevLookScalar;
    public float prevMoveScalar;

    [Header("Hacks")]
    public bool developerMode; // Toggle this to enable/disable cheats
    public bool invisMode;
    public bool increaseSpeed;

    [Header("Games States")]
    public bool gamePaused;
    public int currentCheckPoint;
    public int currentMemory;   // This value is automatically assigned when a memory is triggered
    public int subtitleNum;
    public string currentLevel;
    public bool credits;
    public bool atEndTerrain;
    public bool firstEncounter;

    private RawImage fadeToBlack;

    // Set a small delay before the player is granted control
    IEnumerator FadeInCoRoutine()
    {
        controlsDisabled = true;
        fadeToBlack.CrossFadeAlpha(0, 5, false);
        yield return new WaitForSeconds(1f);
        controlsDisabled = false;
    }

	void Awake ()
	{
        credits = false;
        firstEncounter = false;
        Cursor.lockState = CursorLockMode.Confined; // Keeps the cursor bound to the game window
        fadeToBlack = GameObject.Find("FadeToBlack").GetComponent<RawImage>();
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
        StartCoroutine("FadeInCoRoutine");
        // Load option settings
        masterVolume = PlayerPrefs.GetFloat("Master Volume");
        lookSensitivity = PlayerPrefs.GetFloat("Mouse Sensitivity");
        Cursor.visible = true;        
    }

    void Update()
    {
        // Lerp look and move scalars between vals
        if (prevLookScalar != memoryLookScalar)
        {
            prevLookScalar = Mathf.Lerp(prevLookScalar, memoryLookScalar, Time.deltaTime * 2);
        }

        if (prevMoveScalar != memoryMoveScalar)
        {
            prevMoveScalar = Mathf.Lerp(prevMoveScalar, memoryMoveScalar, Time.deltaTime * 2);
        }

        AudioListener.volume = masterVolume;

        if (playerDead)
        {
            controlsDisabled = true;
        }
    }

	void FixedUpdate ()
	{
        LevelResetCheck();
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
        fadeToBlack = GameObject.Find("FadeToBlack").GetComponent<RawImage>();
        Cursor.visible = false;
        memoryPlaying = false;
        fadeToBlack.CrossFadeAlpha(0, 3, false);
        controlsDisabled = false;
        InitialiseValues();
        Invoke("MovePlayer", 0.05f); // This delay is required so the checkpoint data can be fetched first
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
        playerCheckPoints = GameObject.Find("PlayerCheckPoints").GetComponentsInChildren<Transform>();
    }
}
