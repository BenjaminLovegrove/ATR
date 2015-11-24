﻿using UnityEngine;
using System.Collections;

// Player Inputs and SFX

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

    private float jumpTimer;
    public float jumpDelay;
    public float jumpCoolDown;
	
    public Rigidbody playerRigid;
    AudioSource audio;
    
    public AudioClip walkingSFX;
    private float footStepTimer;
    public float footStepInterval = 0.75f;	
	
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

	void FixedUpdate ()
	{

        PlayFootStepSFX();

        // Increment jump cooldown timer
        if (grounded)
        {
            jumpTimer += Time.deltaTime;
        }

        if (EventManager.inst.playerDead == false)
        {
            if (grounded)
            {
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
		playerRigid.AddForce(new Vector3 (0, -gravity * playerRigid.mass, 0));
		
		grounded = false;
	}

    // Play footsteps using a Vertical input as a scalar
    // TODO: add a random pool of Footstep SFX to play from, also dependant on what area the player is in
    void PlayFootStepSFX()
    {
        footStepTimer += Time.deltaTime * Mathf.Abs(Input.GetAxis("Vertical"));

        if (footStepTimer > footStepInterval)
        {
            audio.PlayOneShot(walkingSFX);
            footStepTimer = 0;
        }
    }
	
	void OnCollisionStay ()
	{
		grounded = true;    
	}
	
	float CalculateJumpVerticalSpeed ()
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}