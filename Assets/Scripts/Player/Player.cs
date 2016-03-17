﻿using UnityEngine;
using System.Collections;

// Script for Player Inputs and SFX

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class Player : MonoBehaviour
{
    private float currentSpeed;
	public float walkSpeed = 3.0f;
    public float crouchSpeed = 2.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 0.5f;
	private bool grounded = false;

    private float jumpTimer = 1.5f;
    public float jumpDelay;
    public float jumpCoolDown;
    	
    public Rigidbody playerRigid;
    AudioSource audio;
    
    public AudioClip[] standingWalkLeftSFX;
    public AudioClip[] standingWalkRightSFX;
    public AudioClip[] crouchWalkLeftSFX;
    public AudioClip[] crouchWalkRightSFX;
    public AudioClip crouchSFX;
    public AudioClip standSFX;
    public AudioSource footStepSFXSource;
    private float footStepTimer;
    private int currentWalkVal;
    private int currentCrouchVal;
    public float footStepInterval = 0.4f;
    private int footStepCount = 2;

    public GameObject[] enemyList;
    public AudioSource heartBeatSFX;
    public AudioSource backGroundMusic;
    public float nearestEnemyDistance;

    public GameObject fadeToBlackObj;
    public bool setActiveFade = false;
	
	void Awake ()
	{
		playerRigid.freezeRotation = true;
        audio = GetComponent<AudioSource>();
	}

    void Start()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Jump co-routine
    IEnumerator Jump()
    {
        Vector3 velocity = playerRigid.velocity;
        yield return new WaitForSeconds(jumpDelay);
        playerRigid.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
    }

    void Update()
    {
        PauseMenu();
    }

	void FixedUpdate ()
	{
        if (EventManager.inst.playerDead == true)
        {            
            if (!setActiveFade)
            {
                setActiveFade = true;
                fadeToBlackObj.SetActive(true);
                fadeToBlackObj.SendMessage("FadeOutReceiver");                
            }
        }

        PlayFootStepSFX();
        PlayerMovement();
        PlayHeartBeatSFX();
        Hacks(); // Comment this out for release builds
	}

    // Play the heartbeat SFX based on distance of the nearest enemy
    void PlayHeartBeatSFX()
    {
        if (!EventManager.inst.playerDead)
        {
            nearestEnemyDistance = float.MaxValue;

            for (int i = 0; i < enemyList.Length; i++)
            {
                float thisEnemyDistance = Vector3.Distance(playerRigid.position, enemyList[i].transform.position);

                if (thisEnemyDistance < nearestEnemyDistance)
                {
                    nearestEnemyDistance = thisEnemyDistance;
                }
            }

            float distanceMod = (((nearestEnemyDistance - 12) / 5) * -1);
            heartBeatSFX.volume = (Mathf.Lerp(0, 1, distanceMod) * EventManager.inst.masterVolume);
            heartBeatSFX.pitch = Mathf.Lerp(0, 1, distanceMod);
            float bgmMod = (((nearestEnemyDistance - 12) / 8) * -1);
            backGroundMusic.volume = (Mathf.Lerp(1, 0, bgmMod) * EventManager.inst.masterVolume);
        }
        else heartBeatSFX.volume = 0;
    }

    // Hacks for testing *** Comment these out for release builds ***
    void Hacks()
    {
        // Increase player speed
        if (Input.GetKeyDown(KeyCode.O) && !EventManager.inst.increaseSpeed)
        {
            print("Player speed increased");
            EventManager.inst.increaseSpeed = true;
            walkSpeed = 9f;
        }

        if (Input.GetKeyDown(KeyCode.P) && EventManager.inst.increaseSpeed)
        {
            print("Player speed set to normal");
            EventManager.inst.increaseSpeed = false;
            walkSpeed = 3f;
        }

        // Make player invisible to enemies
        if (Input.GetKeyDown(KeyCode.U) && !EventManager.inst.invisMode)
        {
            print("Player invisible to enemies");
            EventManager.inst.invisMode = true;
        }

        if (Input.GetKeyDown(KeyCode.I) && EventManager.inst.invisMode)
        {
            print("Player visible to enemies");
            EventManager.inst.invisMode = false;
        }
            
    }

    // Player movement on horizontal and vertical axis through player inputs
    void PlayerMovement()
    {
        // Raycast downward from the player to see if touching the ground
        RaycastHit hit;

        Vector3 ray1 = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
        Vector3 ray2 = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
        Debug.DrawRay(ray1, Vector3.down);
        Debug.DrawRay(ray2, Vector3.down);

        if (Physics.Raycast(ray1, Vector3.down, out hit))
        {
            // If the raycast hits the ground
            if (hit.distance < 3f)
            {
                grounded = true;
                EventManager.inst.playerJump = false;
            }
        } else if (Physics.Raycast(ray2, Vector3.down, out hit))
        {
            // If the raycast hits the ground
            if (hit.distance < 3f)
            {
                grounded = true;
                EventManager.inst.playerJump = false;
            }
        }

        // Movement if controls are not disabled and player is not dead
        if (!EventManager.inst.controlsDisabled && !EventManager.inst.playerDead)
        {
            if (grounded)
            {
                // Increment jump cooldown timer
                if (grounded)
                {
                    jumpTimer += Time.deltaTime;
                }

                // Calculate how fast we should be moving
                Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                targetVelocity = transform.TransformDirection(targetVelocity);
                targetVelocity *= currentSpeed;

                // Apply a force that attempts to reach our target velocity
                Vector3 velocity = playerRigid.velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;
                playerRigid.AddForce(velocityChange, ForceMode.VelocityChange);

                // Jump
                if (jumpTimer > 1.5f)
                {
                    if (canJump && Input.GetButton("Jump"))
                    {
                        EventManager.inst.playerJump = true;
                        jumpTimer = 0;
                        StartCoroutine("Jump");
                    }
                }

                // Crouch
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
                {
                    currentSpeed = crouchSpeed;
                    EventManager.inst.playerCrouch = true;
                }
                else
                {
                    EventManager.inst.playerCrouch = false;
                    currentSpeed = walkSpeed;
                }
                
                // Play Crouch SFX
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
                {
                    audio.PlayOneShot(crouchSFX);
                }

                // Play Crouch SFX
                if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1))
                {
                    audio.PlayOneShot(standSFX);
                }

            }
        }

        // Manual gravity
        playerRigid.AddForce(new Vector3(0, -gravity * playerRigid.mass, 0));

        grounded = false;
    }

    // Play footsteps using a Vertical input to determine if moving
    void PlayFootStepSFX()
    {
        if (!EventManager.inst.playerDead)
        {
            footStepTimer += Time.deltaTime * Mathf.Abs(Input.GetAxis("Vertical"));
            footStepTimer += Time.deltaTime * Mathf.Abs(Input.GetAxis("Horizontal"));

            // Reset walk array when you reach the end
            if (currentWalkVal == (standingWalkLeftSFX.Length))
            {
                currentWalkVal = 0;
            }

            if (currentWalkVal == (standingWalkRightSFX.Length))
            {
                currentWalkVal = 0;
            }

            // Reset crouch array when you reach the end
            if (currentCrouchVal == (crouchWalkLeftSFX.Length))
            {
                currentCrouchVal = 0;
            }

            if (currentCrouchVal == (crouchWalkRightSFX.Length))
            {
                currentCrouchVal = 0;
            }

            // Play footstep if timer is achieved
            if (footStepTimer > footStepInterval)
            {
                if (!EventManager.inst.playerCrouch)
                {
                    if (footStepCount % 2 == 0)
                    {
                        footStepSFXSource.clip = standingWalkLeftSFX[currentWalkVal];
                        footStepSFXSource.pitch = Random.Range(0.8f, 1.15f);
                        footStepSFXSource.Play();
                    }
                    else
                    {
                        footStepSFXSource.clip = standingWalkRightSFX[currentWalkVal];
                        footStepSFXSource.pitch = Random.Range(0.8f, 1.15f);
                        footStepSFXSource.Play();
                    }
                    currentWalkVal++;
                    footStepCount++;
                }
                // Play crouching footstep if not standing
                else
                {
                    if (footStepCount % 2 == 0)
                    {
                        footStepSFXSource.clip = crouchWalkLeftSFX[currentWalkVal];
                        footStepSFXSource.pitch = Random.Range(0.8f, 1.15f);
                        footStepSFXSource.Play();
                    }
                    else
                    {
                        footStepSFXSource.clip = crouchWalkRightSFX[currentWalkVal];
                        footStepSFXSource.pitch = Random.Range(0.8f, 1.15f);
                        footStepSFXSource.Play();
                    }
                    currentWalkVal++;
                    footStepCount++;
                }

                footStepTimer = 0;
            }
        }
    }
		
	float CalculateJumpVerticalSpeed ()
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

    // In game pause function
    void PauseMenu()
    {       
        if (Input.GetKey(KeyCode.Escape) && !EventManager.inst.gamePaused)
        {
            print("Game paused");
            Cursor.visible = true;
            Time.timeScale = 0;
            EventManager.inst.gamePaused = true;
            EventManager.inst.pauseMenuButtons.SetActive(true);
        }
    }

    void OnLevelWasLoaded()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
    }
}