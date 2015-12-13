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
    public int currentMemory;                   // personally assign a unique number for each memory interaction for the entire game
    public int currentLevel = 0;                // City = 2, Test = 3    

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
        // Hide cursor
        Cursor.visible = false;
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

        // Set player references if empty
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        playerObj = GameObject.Find("Player");
        pauseMenuButtons = GameObject.Find("PauseMenuObj");
        playerCheckPoints = GameObject.Find("PlayerCheckPoints").GetComponentsInChildren<Transform>();

        // Move the player to the current checkpoint location
        playerTrans.position = playerCheckPoints[currentCheckPoint].position;
    }
}
