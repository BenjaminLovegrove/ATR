﻿using UnityEngine;
using System.Collections;

// Player Camera

public class PlayerCam : MonoBehaviour
{
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationY = 0F;
    public float crouch = 2;
   
    public Transform cameraPosNeutral;
    public Transform cameraPosCrouch;
    public Transform cameraPosHeadBob;
    public Transform currentPos;
    public Transform cameraDead;

    public float headBobInterval;
    private float headBobTimer;    
    private bool headBobbed;

	void FixedUpdate ()
	{
        CameraLerping();
        MouseMovement();
	}

    void CameraLerping()
    {
        // Face enemy that killed the player
        if (EventManager.inst.playerDead)
        {
            transform.LookAt(EventManager.inst.enemyKillPos);
            //Vector3 lookAtEnemy = new Vector3(currentPos.position.x, EventManager.inst.enemyKillPos.position.y, currentPos.position.z);
            //currentPos.position = Vector3.Lerp(currentPos.position, lookAtEnemy, Time.deltaTime * 3);
        }

        // Head bob
        headBobTimer += Time.deltaTime;

        if (!EventManager.inst.controlsDisabled)
        {
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

        // Lerp camera for player death
        if (EventManager.inst.playerDead)
        {
            currentPos.position = Vector3.Lerp(currentPos.position, cameraDead.position, Time.deltaTime);
        }
    }

    // Move camera to mouse
    void MouseMovement()
    {
        if (!EventManager.inst.controlsDisabled)
        {
            if (axes == RotationAxes.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
        }
    }
}