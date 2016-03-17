using UnityEngine;
using System.Collections;

// Handle global variables, such as player checkpoints and game states

public class EventManager : MonoBehaviour
{
    public static EventManager inst;

	public Transform playerTrans;
    public Transform enemyKillPos;
    public Transform[] playerCheckPoints;
    public Transform flashSpawn;

	public GameObject playerObj;
    public GameObject pauseMenuButtons;    

	public bool playerCrouch = false;
    public bool playerDead = false;
    public bool playerJump = false;
    public bool gamePaused = false;
    public bool resetLevel = false;
    public bool controlsDisabled = true;
    public bool controlDisableDelay = true;
    public float controlDelay = 3f;
    
    // Option Settings
    public float mouseSensitivty;
    public float masterVolume;
    public bool invertY;

    // Hacks
    public bool invisMode = false;
    public bool increaseSpeed = false;
    
    public int currentCheckPoint = 0;
    public int currentMemory;                   // This value is automatically assigned when a memory is triggered
    public int currentLevel = 0;                // City = 2, Test = 3, Credits = 4  

	void Awake ()
	{
        //enemyMaterial.SetFloat("_Mode", 2.0f);
		// Check for duplicate singletons
		if (inst == null)
		{
			inst = this;
			DontDestroyOnLoad(gameObject);
		}
		else Destroy(this.gameObject);
	}

    void Start()
    {
        // Load option settings
        masterVolume = PlayerPrefs.GetFloat("Master Volume");
        mouseSensitivty = PlayerPrefs.GetFloat("Mouse Sensitivity");

        flashSpawn = GameObject.FindGameObjectWithTag("FlashSpawn").transform;
        controlDisableDelay = true;
        Cursor.visible = true;
    }

	void FixedUpdate ()
	{
        AudioListener.volume = masterVolume;
        ControlsDisabledDelay();
        LevelResetCheck();

        if (!gamePaused && pauseMenuButtons != null)
        {
            pauseMenuButtons.SetActive(false);
        }
	}
    
    // Set a small delay before the player is granted control of the character at start
    void ControlsDisabledDelay()
    {
        if (controlDisableDelay)
        {
            controlsDisabled = true;
            controlDelay -= Time.deltaTime;

            if (controlDelay < 0f)
            {
                controlsDisabled = false;
                controlDisableDelay = false;
            }
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
	void ResetPlayer ()
	{
        controlDisableDelay = true;
        controlsDisabled = false;
        playerDead = false;        
        resetLevel = false;
        Application.LoadLevel(currentLevel);      
	}

    // Populate EventManager data upon loading a scene
    void OnLevelWasLoaded()
    {
        controlDisableDelay = true;
        controlsDisabled = false;
        playerDead = false;
        resetLevel = false;

        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        playerObj = GameObject.Find("Player");
        pauseMenuButtons = GameObject.FindWithTag("PauseMenuObj");
        playerCheckPoints = GameObject.Find("PlayerCheckPoints").GetComponentsInChildren<Transform>();
        flashSpawn = GameObject.FindGameObjectWithTag("FlashSpawn").transform;
        Invoke("MovePlayer", 0.05f);
    }

    // Move the player to the current checkpoint location
    void MovePlayer()
    {        
        playerTrans.position = playerCheckPoints[currentCheckPoint].position;
    }
}
