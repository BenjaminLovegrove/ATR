using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

// Script to handle trigger events that can occur throughout the game
// Set appropriate bools on the corresponding game object

public class TriggerManager : MonoBehaviour
{

    [Header("Trigger Settings")]
    bool triggered = false;

    // The duration in which the memory lasts is set via this float
    // The unique memory encounter is set via the int
    // The int values will be unique to each scene
    [Header("Memory Trigger")]
    public bool memory;
    public float memoryDuration;
    public int memoryEventNumber;

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

    void Start()
    {
        fog = GameObject.Find("Player").GetComponent<GlobalFog>();
    }

    void OnTriggerEnter (Collider col)
    {
	    if (col.gameObject.tag == "Player" && !triggered)
        {
			triggered = true;

            // Memory
            if (memory)
            {
                print("Memory Triggered");
                col.BroadcastMessage("EnterMemory", memoryDuration);
                EventManager.inst.currentMemory = memoryEventNumber;
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
        }
	}

    void FixedUpdate()
    {
        // Change fog
        if (fogLerp < 1)
        {
            fogLerp += Time.deltaTime / 2;
            fog.heightDensity = Mathf.Lerp(startFog, targFog, fogLerp);
        }

    }
}
