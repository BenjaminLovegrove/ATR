﻿using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

// Player Camera

public class PlayerCam : MonoBehaviour
{
    public Camera playerCam;
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
    private float sensitivityX;
    private float sensitivityY;
    private float lookScalar;
    private float minimumX = -360F;
    private float maximumX = 360F;
    private float minimumY = -60F;
    private float maximumY = 60F;
    private float crouch = 2;
    private GlobalFog fog;
    private float rotationY;

    public Transform cameraPosNeutral;
    public Transform cameraPosCrouch;
    public Transform cameraPosHeadBob;
    public Transform cameraPosFE;
    public Transform currentPos;
    public Transform cameraDead;
    
    public Transform[] memoryCameraEnd;

    public float headBobInterval;
    private float headBobTimer;    
    private bool headBobbed;
    private bool inMemory = false;
    private float deathDelay = 0;

    void Awake()
    {
        playerCam = gameObject.GetComponentInChildren<Camera>();
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
        CameraSettings();
	}

    void CameraSettings()
    {
        sensitivityX = EventManager.inst.lookSensitivity; // No need to modify within this script
        sensitivityY = EventManager.inst.lookSensitivity; // These need to be in Fixed in order to sync with the time scale
        lookScalar = EventManager.inst.prevLookScalar;

        if (EventManager.inst.memoryPlaying || EventManager.inst.credits)
        {
            MemoryCam();
        }

        if (EventManager.inst.playerDead)
        {
            DeathCamera();
        }

        if (!EventManager.inst.controlsDisabled)
        {
            CameraMovement();
            MouseMovement();
        } else
        {
            HackyMouseMovement();
        }

        if (EventManager.inst.firstEncounterPlaying)
        {
            // Lerp camera to a slightly crouching pos during first memory
            currentPos.position = Vector3.Lerp(currentPos.position, cameraPosFE.position, Time.deltaTime * 3);
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
        if (!EventManager.inst.memoryPlaying && !EventManager.inst.playerDead)
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
            if (!EventManager.inst.memoryPlaying && !EventManager.inst.playerDead && !EventManager.inst.controlsDisabled)
            {
                currentPos.position = Vector3.Lerp(currentPos.position, cameraPosHeadBob.position, Time.deltaTime * 3);   
            }                       
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
            if (!EventManager.inst.playerDead)
            {
                if (axes == RotationAxes.MouseXAndY)
                {
                    float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX * lookScalar;

                    // Invert Y if set to do so
                    if (EventManager.inst.invertY)
                    {
                        rotationY += Input.GetAxis("Mouse Y") * sensitivityY * lookScalar * -1;
                    }
                    // Otherwise Y is normal
                    if ((!EventManager.inst.invertY))
                    {
                        rotationY += Input.GetAxis("Mouse Y") * sensitivityY * lookScalar;
                    }

                    rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                    transform.localEulerAngles = new Vector3(0, rotationX, 0);
                    playerCam.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
                }
                else if (axes == RotationAxes.MouseX)
                {
                    transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX * lookScalar, 0);
                }
                else
                {
                    rotationY += Input.GetAxis("Mouse Y") * sensitivityY * lookScalar;
                    rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                    //transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
                    playerCam.transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
                }
            }
        }
    }
   
    // Keep updating rotations but dont move cam
    void HackyMouseMovement()
    {
        if (EventManager.inst.controlsDisabled)
        {
            if (!EventManager.inst.playerDead)
            {
                if (axes == RotationAxes.MouseXAndY)
                {
                    float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX * lookScalar;

                    // Invert Y if set to do so
                    if (EventManager.inst.invertY)
                    {
                        rotationY += Input.GetAxis("Mouse Y") * sensitivityY * lookScalar * -1;
                    }
                    // Otherwise Y is normal
                    if ((!EventManager.inst.invertY))
                    {
                        rotationY += Input.GetAxis("Mouse Y") * sensitivityY * lookScalar;
                    }

                    rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                }
                else if (axes == RotationAxes.MouseX)
                {
                    transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX * lookScalar, 0);
                }
                else
                {
                    rotationY += Input.GetAxis("Mouse Y") * sensitivityY * lookScalar;
                    rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);


                }
            }
        }
    }

    // Face enemy that killed the player
    void DeathCamera()
    {
        deathDelay += Time.deltaTime;

        if (deathDelay > 0.6f)
        {
            // Lower the camera
            currentPos.position = Vector3.Lerp(currentPos.position, cameraDead.position, Time.deltaTime * 2);

            fog.heightDensity = Mathf.Lerp(fog.heightDensity, 5, Time.deltaTime);
        }


        // Face the enemy that killed the player
        Vector3 enemyDirection = EventManager.inst.enemyKillPos.position - transform.position;
        playerCam.transform.rotation = Quaternion.Lerp(currentPos.rotation, Quaternion.LookRotation(enemyDirection), Time.deltaTime * 1.5f);
    }
    //public void SetDirection(Vector3 directionVector, Boolean setYaw, Boolean setPitch)
    //{
    //    Vector3 d = directionVector.NormalisedCopy;
    //    if (setPitch)
    //        mPitch = Math.ASin(d.y);
    //    if (setYaw)
    //        mYaw = Math.ATan2(-d.x, -d.z);//+Math.PI/2.0;
    //    mChanged = setYaw || setPitch;
    //}
    void MemoryCam()
    {
        
        Vector3 dir = memoryCameraEnd[EventManager.inst.currentMemory].position - currentPos.position;
        dir.Normalize();
        float pitch = Mathf.Asin(dir.y) * Mathf.Rad2Deg;
        float yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        float et = transform.localEulerAngles.y;
        //float ep = playerCam.transform.localEulerAngles.x;
        et = Mathf.MoveTowardsAngle(et, yaw, Time.deltaTime * 1f);
        //et = Mathf.Lerp(ep, Quaternion.LookRotation(memoryCameraEnd[EventManager.inst.currentMemory].position - currentPos.position).eulerAngles.x, Time.deltaTime);
        rotationY = Mathf.MoveTowardsAngle(rotationY, pitch, Time.deltaTime * 1f);
        transform.localEulerAngles = new Vector3(0, et, 0);

        transform.rotation = Quaternion.Lerp(currentPos.rotation, Quaternion.LookRotation(memoryCameraEnd[EventManager.inst.currentMemory].position - currentPos.position), Time.deltaTime * 1f);
    }
}