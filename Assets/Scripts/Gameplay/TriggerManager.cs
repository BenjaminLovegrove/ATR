using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

// Script to handle trigger events that can occur throughout the game
// Set appropriate bools on the corresponding game object

public class TriggerManager : MonoBehaviour
{

    [Header("Trigger Settings")]
    bool triggered = false;

    [Header("Memory Trigger")]
    public bool memory;
    public float memoryDuration;

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
                col.BroadcastMessage("EnterMemory", memoryDuration);
            }

            // Checkpoint
			if (checkpoint)
            {
				EventManager.inst.currentCheckPoint ++;
			}

            if (enemyTrigger)
            {
                enemy.SetActive(true);
            }

            // Fog
            if (fogChange)
            {
                startFog = fog.heightDensity;
                fogLerp = 0;

            }
        }
	}

    void Update()
    {
        // Change fog
        if (fogLerp < 1)
        {
            fogLerp += Time.deltaTime / 2;
            fog.heightDensity = Mathf.Lerp(startFog, targFog, fogLerp);
        }

    }
}
