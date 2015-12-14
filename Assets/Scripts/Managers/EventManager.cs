using UnityEngine;
using System.Collections;

// Handle global variables, such as player checkpoints and game states

public class EventManager : MonoBehaviour
{
    public static EventManager inst;

	public Transform playerTrans;
    public Transform enemyKillPos;
    public Transform[] playerCheckPoints;

	public GameObject playerObj;
    public GameObject pauseMenuButtons;

	public bool playerCrouch = false;
    public bool controlsDisabled = false;
    public bool playerDead = false;
    public bool playerJump = false;
    public bool gamePaused = false;
    public bool resetLevel = false;
    
    public int currentCheckPoint = 0;
    public int currentMemory;                   // This value is automatically assigned when a memory is triggered
    public int currentLevel = 0;                // City = 2, Test = 3, Credits = 4  

	void Awake ()
	{
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
        Cursor.visible = false;
    }

	void FixedUpdate ()
	{
        LevelResetCheck();

        // TODO - this is hacky and throwing nulls like a mofo - FIX!
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
	void ResetPlayer ()
	{
        controlsDisabled = false;
        playerDead = false;        
        resetLevel = false;
        Application.LoadLevel(currentLevel);      
	}

    // Populate EventManager data upon loading a scene
    void OnLevelWasLoaded()
    {
        controlsDisabled = false;
        playerDead = false;
        resetLevel = false;

        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        playerObj = GameObject.Find("Player");
        pauseMenuButtons = GameObject.FindWithTag("PauseMenuObj");
        playerCheckPoints = GameObject.Find("PlayerCheckPoints").GetComponentsInChildren<Transform>();

        // Move the player to the current checkpoint location
        playerTrans.position = playerCheckPoints[currentCheckPoint].position;
    }
}
