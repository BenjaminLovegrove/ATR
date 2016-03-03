﻿using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

// Player Camera

public class PlayerCam : MonoBehaviour
{
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float mouseSensitivity = 15F;
    private float minimumX = -360F;
    private float maximumX = 360F;
    private float minimumY = -60F;
    private float maximumY = 60F;
    private float rotationY = 0F;
    private float crouch = 2;
    private GlobalFog fog;

    public Transform cameraPosNeutral;
    public Transform cameraPosCrouch;
    public Transform cameraPosHeadBob;
    public Transform currentPos;
    public Transform cameraDead;
    
    public Transform[] memoryCameraEnd;

    public float headBobInterval;
    private float headBobTimer;    
    private bool headBobbed;

    void Awake()
    {
        fog = gameObject.GetComponentInChildren<GlobalFog>();
    }

    // PreJump cam movement co-routine
    IEnumerator PreJump()
    {
        currentPos.position = Vector3.Lerp(currentPos.position, cameraPosCrouch.position, Time.deltaTime * 10);
        yield return new WaitForSeconds(0.3f);
        currentPos.position = Vector3.Lerp(currentPos.position, cameraPosNeutral.position, Time.deltaTime * 3);
    }

	void FixedUpdate ()
	{
        mouseSensitivity = EventManager.inst.mouseSensitivty;

        if (EventManager.inst.playerDead)
        {
            DeathCamera();
        }

        if (!EventManager.inst.controlsDisabled)
        {
            CameraMovement();
            MouseMovement();
        }
	}

    void CameraMovement()
    {
        headBobTimer += Time.deltaTime;

        // Pre cam movement
        if (EventManager.inst.playerJump)
        {
            StartCoroutine("PreJump");
        }

        // Head bob
        if (headBobTimer > headBobInterval)
        {
            headBobbed = true;
            currentPos.position = Vector3.Lerp(currentPos.position, cameraPosNeutral.position, Time.deltaTime * 6);
        }

        if (headBobTimer > headBobInterval * 1.5f)
        {
            currentPos.position = Vector3.Lerp(currentPos.position, cameraPosNeutral.position, Time.deltaTime * 6);
            headBobbed = false;
            headBobTimer = 0;
        }       

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {            
               currentPos.position = Vector3.Lerp(currentPos.position, cameraPosHeadBob.position, Time.deltaTime * 3);              
        }
        
        // Return to neutral cam position if movement ceases
        else
        {
            currentPos.position = Vector3.Lerp(currentPos.position, cameraPosNeutral.position, Time.deltaTime * 6);
            headBobbed = false;
            headBobTimer = 0;
        }

        // Lerp camera for player crouch
        if (EventManager.inst.playerCrouch)
        {
            currentPos.position = Vector3.Lerp(currentPos.position, cameraPosCrouch.position, Time.deltaTime * 3);
        }
        else
        {
            currentPos.position = Vector3.Lerp(currentPos.position, cameraPosNeutral.position, Time.deltaTime * 3);
        }
    }

    // Move camera to mouse
    void MouseMovement()
    {
        if (!EventManager.inst.controlsDisabled)
        {
            if (axes == RotationAxes.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

                rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
        }
    }

    // Face enemy that killed the player
    void DeathCamera()
    {     
        // Lower the camera
        currentPos.position = Vector3.Lerp(currentPos.position, cameraDead.position, Time.deltaTime * 2);

        fog.heightDensity = Mathf.Lerp(fog.heightDensity, 5, Time.deltaTime);

        // Face the enemy that killed the player
        Vector3 enemyDirection = EventManager.inst.enemyKillPos.position - transform.position;
        transform.rotation = Quaternion.Lerp(currentPos.rotation, Quaternion.LookRotation(enemyDirection), Time.deltaTime * 1.5f);    
    }

    // Sendmessage reciever for entering a memory
    void EnterMemory()
    {
        transform.rotation = Quaternion.Lerp(currentPos.rotation, memoryCameraEnd[EventManager.inst.currentMemory].rotation, Time.deltaTime * 1f);
    }
}