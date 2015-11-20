﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class Player : MonoBehaviour
{
	
	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	private bool grounded = false;
	public Rigidbody playerRigid;
    AudioSource audio;
    //public AudioClip footSteps;
    private bool walking = false;
    AudioClip walkingSFX;
	
	
	
	void Awake ()
	{
		playerRigid.freezeRotation = true;
		playerRigid.useGravity = false;
        audio = GetComponent<AudioSource>();
	}
	
	void FixedUpdate ()
	{
		if (grounded)
		{
			// Calculate how fast we should be moving
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;
			
			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = playerRigid.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			playerRigid.AddForce(velocityChange, ForceMode.VelocityChange);
			
			// Jump
			if (canJump && Input.GetButton("Jump"))
			{
				playerRigid.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}


            // Play footsteps sfx
            //if (Input.GetButton("Vertical") && walking == false)
            //if (Input.GetButton("Vertical"))
            //{
            //    //audio.loop = true;
            //    //walking = true;
            //    audio.Play();
                
            //}
            //else audio.Stop();

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                audio.loop = true;
                //audio.clip = walkingSFX;
                audio.Play();
                // walking = true;
                // audio.PlayOneShot(footSteps);
            }
            //else audio.Stop();
            //else walking = false;

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                audio.Stop();
            }

        }
		
		// Manual gravity
		playerRigid.AddForce(new Vector3 (0, -gravity * playerRigid.mass, 0));
		
		grounded = false;
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