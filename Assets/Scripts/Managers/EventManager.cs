using UnityEngine;
using System.Collections;

// Handle global variables, such as player checkpoints and game states

public class EventManager : MonoBehaviour
{
    public static EventManager inst;

	public Transform playerTrans;
	public GameObject playerObj;
	public bool playerCrouch = false;

    public Transform[] playerCheckPoints;
    public int currentCheckPoint;

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
        Cursor.visible = false;
    }

	void FixedUpdate ()
	{
        // Check for external level reset
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
        Application.LoadLevel(0);
        resetLevel = false;
        playerTrans.position = playerCheckPoints[currentCheckPoint].position;
        //print(currentCheckPoint);
        
	}

    // Repopulate EventManager data upon loading a scene
    void OnLevelWasLoaded()
    {
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        playerObj = GameObject.Find("Player");

        for (int i = 0; i < playerCheckPoints.Length; i++)
        {
            playerCheckPoints = GameObject.Find("PlayerCheckPoints").GetComponentsInChildren<Transform>();
        }
    }
}
