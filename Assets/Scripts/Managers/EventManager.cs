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

	public bool playerCrouch = false;
    public bool controlsDisabled = false;
    public bool playerDead = false;
    
    public int currentCheckPoint = 0;

    public bool resetLevel = false;

	void Awake ()
	{
		// Check for duplicate singletons
		if (inst == null)
		{
			inst = this;
			DontDestroyOnLoad(gameObject);
		}
		else Destroy(this);

        // Set player references if empty
        if (playerTrans == null)
        {
            playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        }

        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
        }
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
        Application.LoadLevel(0);
        playerTrans.position = playerCheckPoints[currentCheckPoint].position;
	}

    // Populate EventManager data upon loading a scene
    void OnLevelWasLoaded()
    {
        controlsDisabled = false;
        playerDead = false;
        resetLevel = false;
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        playerObj = GameObject.Find("Player");

        for (int i = 0; i < playerCheckPoints.Length; i++)
        {
            playerCheckPoints = GameObject.Find("PlayerCheckPoints").GetComponentsInChildren<Transform>();
        }
    }
}
