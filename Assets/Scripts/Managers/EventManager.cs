using UnityEngine;
using System.Collections;

//Handle global variables, such as player checkpoints and game states

public class EventManager : MonoBehaviour
{
	public static EventManager inst;
	public int playerHp = 100;
	public Vector3 lastPlayerSighting;
	public Transform playerTrans;
	public GameObject playerObj;
	public bool playerCrouch = false;

    public Transform[] PlayerCheckPoints;
    public int currentCheckPoint;

    public bool resetLevel = false;

	void Awake ()
	{
		// Check for duplicate singletons
		if(inst == null)
		{
			inst = this;
			DontDestroyOnLoad(gameObject);
		}
		else Destroy(this);
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
    // Place an empty gameobject with the desired world position of the
    // checkpoint into the corresponding array value.
    // Use triggers to set the value of the last achieved checkpoint.
	void ResetPlayer ()
	{
        playerTrans = PlayerCheckPoints[currentCheckPoint];
        resetLevel = false;
	}
}
