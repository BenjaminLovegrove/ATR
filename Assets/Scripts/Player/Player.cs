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
    
    public AudioClip[] standingWalk;
    public AudioClip[] crouchingWalk;
    private float footStepTimer;
    public int currentWalkSFX;
    public int currentCrouchSFX;
    public float footStepInterval = 0.75f;

    public float alphaFadeValue;
    public Texture fadeTexture;
	
	void Awake ()
	{
		playerRigid.freezeRotation = true;
		playerRigid.useGravity = false;
        audio = GetComponent<AudioSource>();
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
        PlayFootStepSFX();
        PlayerMovement();
	}

    // Player movement on horizontal and vertical axis through player inputs
    void PlayerMovement()
    {
        // Raycast downward from the player to see if touching the ground
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
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
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
                {
                    currentSpeed = crouchSpeed;
                    EventManager.inst.playerCrouch = true;
                }
                else
                {
                    EventManager.inst.playerCrouch = false;
                    currentSpeed = walkSpeed;
                }
            }
        }

        // Manual gravity
        playerRigid.AddForce(new Vector3(0, -gravity * playerRigid.mass, 0));

        grounded = false;
    }

    // Play footsteps using a Vertical input as a scalar
    // TODO: add a random pool of Footstep SFX to play from, also dependant on what area the player is in
    void PlayFootStepSFX()
    {        
        footStepTimer += Time.deltaTime * Mathf.Abs(Input.GetAxis("Vertical"));

        // Reset walk array when you reach the end
        if (currentWalkSFX == (standingWalk.Length))
        {
            currentWalkSFX = 0;
        }

        // Reset crouch array when you reach the end
        if (currentCrouchSFX == (crouchingWalk.Length))
        {
            currentCrouchSFX = 0;
        }

        // Play footstep if timer is achieved
        if (footStepTimer > footStepInterval)
        {
            if (!EventManager.inst.playerCrouch)
            {
                audio.PlayOneShot(standingWalk[currentWalkSFX]);
                currentWalkSFX++;
            }
            // Play crouching footstep
            else
            {
                audio.PlayOneShot(crouchingWalk[currentCrouchSFX]);
                currentCrouchSFX++;
            }
            
            footStepTimer = 0;
        }
    }
		
	float CalculateJumpVerticalSpeed ()
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

    void FadeIn()
    {
        //fader.material.renderer.color.a = 0f;

        Color color = GetComponent<Renderer>().material.color;
        color.a -= 0.1f;
        GetComponent<Renderer>().material.color = color;

        //Color tempcolor = gameobject.renderer.material.color;
        //tempcolor.a = Mathf.MoveTowards(0, 1, Time.deltaTime);
        //gameobject.renderer.material.color = tempcolor;
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

    void OnGUI()
    {
        alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / 5);
        GUI.color = new Color(0, 0, 0, alphaFadeValue);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
    }
}