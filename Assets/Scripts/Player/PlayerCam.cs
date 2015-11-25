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
   
    public Transform cameraPosUpper;
    public Transform cameraPosLower;
    public Transform currentPos;
    public Transform cameraDead;

	void FixedUpdate ()
	{
        // Lerp camera for player crouch
        if (EventManager.inst.playerCrouch)
        {
            currentPos.position = Vector3.Lerp(currentPos.position, cameraPosLower.position, Time.deltaTime * 3);
        }
        else
        {
            currentPos.position = Vector3.Lerp(currentPos.position, cameraPosUpper.position, Time.deltaTime * 3);
        }

        // Lerp camera for player death
        if (EventManager.inst.playerDead)
        {
            currentPos.position = Vector3.Lerp(currentPos.position, cameraDead.position, Time.deltaTime);
        }

        // Move camera to mouse
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