using UnityEngine;
using System.Collections;

// Script for Player Inputs and SFX

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float footStepInterval;
	public float walkSpeed;
    public float crouchSpeed;
	public float gravity;
	public float maxVelocityChange;
    
    //[Header("Jump")]
    //public float jumpHeight = 0.5f;
    //public float jumpDelay;
    //public float jumpCoolDown;	

    private float currentSpeed;
    private float jumpTimer = 1.5f;
    private float nearestEnemyDistance;
    private bool grounded;
    private bool touchingTerrain;

    private GameObject[] enemyList;
    private float backgroundMaxVol;
    private int footStepCount;
    private float hackMoveSpeed;
    private float footStepTimer;
    private int currentWalkVal;
    private int currentCrouchVal;
    private float walkingVolume;

    private AudioSource audio;
    public AudioSource footStepSFXSource;
    public AudioSource heartBeatSFX;
    public AudioSource backGroundMusic;
    public AudioClip[] standingWalkLeftHardSFX;
    public AudioClip[] standingWalkRightHardSFX;
    public AudioClip[] standingWalkLeftSoftSFX;
    public AudioClip[] standingWalkRightSoftSFX;
    public AudioClip[] crouchWalkLeftSFX;
    public AudioClip[] crouchWalkRightSFX;
    public AudioClip[] jumpSFX;
    public AudioClip crouchSFX;
    public AudioClip standSFX;
    private Rigidbody playerRigid;

	void Awake ()
	{
        walkSpeed = 3f;
        crouchSpeed = 1f;
        playerRigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();    
        hackMoveSpeed = 1f;
        backgroundMaxVol = backGroundMusic.volume;
        playerRigid.freezeRotation = true;
        walkingVolume = footStepSFXSource.volume;           
	}

    void Start()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Jump co-routine
    //IEnumerator Jump()
    //{
    //    Vector3 velocity = playerRigid.velocity;
    //    yield return new WaitForSeconds(jumpDelay);
    //    playerRigid.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
    //}

    void Update()
    {
        Hacks(); // *** Disable this for release builds (EventManager)***
    }

    void FixedUpdate()
    {
        PlayFootStepSFX();
        PlayerMovement();
        PlayHeartBeatSFX();
    }

    // Play the heartbeat SFX based on distance of the nearest enemy
    void PlayHeartBeatSFX()
    {

        if (!EventManager.inst.controlsDisabled)
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

                if (nearestEnemyDistance < 50 && !EventManager.inst.memoryPlaying)
                {
                    EventManager.inst.enemyNear = true;
                    float distanceMod = (10 / nearestEnemyDistance);
                    heartBeatSFX.volume = (Mathf.Lerp(0, 1, distanceMod));
                    heartBeatSFX.pitch = Mathf.Lerp(0, 1, distanceMod);
                    float bgmMod = (((nearestEnemyDistance - 18) / 8) * -1);
                    if (!EventManager.inst.memoryPlaying && !EventManager.inst.atEndTerrain)
                    {
                        backGroundMusic.volume = (Mathf.Lerp(backgroundMaxVol, 0, bgmMod));
                    }
                } else
                {
                    heartBeatSFX.volume = 0;
                }                
            }
        }
    }

    // Hacks for testing *** disable these out for release builds ***
    void Hacks()
    {        
        if (EventManager.inst.developerMode)
        {
            // Increase player speed
            if (Input.GetKeyDown(KeyCode.O))
            {
                print("Player speed increased");
                EventManager.inst.increaseSpeed = true;
                hackMoveSpeed = 3f;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                print("Player speed set to normal");
                EventManager.inst.increaseSpeed = false;
                hackMoveSpeed = 1f;
            }

            // Make player invisible to enemies
            if (Input.GetKeyDown(KeyCode.U))
            {
                print("Player invisible to enemies");
                EventManager.inst.invisMode = true;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                print("Player visible to enemies");
                EventManager.inst.invisMode = false;
            }
        }            
    }

    // Player movement on horizontal and vertical axis through player inputs
    void PlayerMovement()
    {
        // Update and scale the player move speed when vals are changed externally
        currentSpeed = walkSpeed * EventManager.inst.prevMoveScalar * hackMoveSpeed;

        // Normalise strafe movespeed
        if ((playerRigid.velocity.x + playerRigid.velocity.z) > walkSpeed)
        {
            playerRigid.velocity = playerRigid.velocity.normalized * walkSpeed;
        }

        // Raycast downward from the player to see if touching the ground
        RaycastHit hit;

        Vector3 ray1 = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
        Vector3 ray2 = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
        Vector3 rayGround = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Debug.DrawRay(ray1, Vector3.down);
        Debug.DrawRay(ray2, Vector3.down);

        if (Physics.Raycast(rayGround, Vector3.down, out hit))
        {
            // Check which footstep array SFX should be used
            if (hit.collider.tag == "Terrain")
            {
                touchingTerrain = true;
            }
            else touchingTerrain = false;
        }

        //Grounded raycasts
        if (Physics.Raycast(ray1, Vector3.down, out hit))
        {
            // If the raycast hits the ground
            if (hit.distance < 3f)
            {
                grounded = true;
                EventManager.inst.playerJump = false;
            }
        }

        if (Physics.Raycast(ray2, Vector3.down, out hit))
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

                float newHor = Input.GetAxis("Horizontal");
                float newVir = Input.GetAxis("Vertical");
                RaycastHit hit2;

                //Stop player climbing walls
                if (newVir < 0)
                {
                    if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z), -transform.forward, out hit2, 1f))
                    {
                        if (hit2.collider.tag == "Terrain")
                        {
                            newVir = 0;
                        }                        
                    }
                }
                else if (newVir > 0)
                {
                    if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z), transform.forward, out hit2, 1f))
                    {
                        if (hit2.collider.tag == "Terrain")
                        {
                            newVir = 0;
                        }  
                    }
                }

                if (newHor < 0)
                {
                    if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z), -transform.right, out hit2, 1f))
                    {
                        if (hit2.collider.tag == "Terrain")
                        {
                            newHor = 0;
                        }  
                    }
                }
                else if (newHor > 0)
                {
                    if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z), transform.right, out hit2, 1f))
                    {
                        if (hit2.collider.tag == "Terrain")
                        {
                            newHor = 0;
                        }
                    }
                }

                // Calculate how fast we should be moving
                Vector3 targetVelocity = new Vector3(newHor, 0, newVir);
                targetVelocity = transform.TransformDirection(targetVelocity);
                targetVelocity *= currentSpeed;

                // Apply a force that attempts to reach our target velocity
                Vector3 velocity = playerRigid.velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;

                Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.35f, transform.position.z), transform.forward, Color.red);
                Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.35f, transform.position.z), -transform.forward, Color.blue);
                Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.35f, transform.position.z), transform.right);
                Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.35f, transform.position.z), -transform.right, Color.yellow);

                playerRigid.AddForce(velocityChange, ForceMode.VelocityChange);

                // Jump
                //if (jumpTimer > 1.5f)
                //{
                //    //if (canJump && Input.GetButton("Jump"))
                //    if (Input.GetButton("Jump"))
                //    {
                //        footStepSFXSource.Play();
                //        EventManager.inst.playerJump = true;
                //        jumpTimer = 0;
                //        StartCoroutine("Jump");
                //    }
                //}

                //// Play jump SFX
                //if (Input.GetKeyDown(KeyCode.Space))
                //{
                //    if (touchingTerrain)
                //    {
                //        audio.PlayOneShot(jumpSFX[0]);
                //    }
                //    else audio.PlayOneShot(jumpSFX[1]);                  
                //}

                // Crouch
                if (!EventManager.inst.memoryPlaying)
                {
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
                    {
                        walkSpeed = crouchSpeed;
                        EventManager.inst.playerCrouch = true;
                    }
                    else
                    {
                        walkSpeed = 3f;
                        EventManager.inst.playerCrouch = false;
                    }

                    // Fail safe for not crouching
                    if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1))
                    {
                        walkSpeed = 3f;
                        EventManager.inst.playerCrouch = false;
                    }

                    //// Over write for during memory
                    //if (EventManager.inst.memoryPlaying)
                    //{
                    //    walkSpeed = 3f;
                    //    EventManager.inst.playerCrouch = false;
                    //}
                    
                    //if (!EventManager.inst.firstEncounterPlaying)
                    //{
                    //    EventManager.inst.playerCrouch = false;
                    //    walkSpeed = 3f;
                    //}

                    // Play Crouching SFX
                    if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.LeftControl))
                    {
                        audio.PlayOneShot(crouchSFX, 1f);
                    }

                    // Play Standing SFX
                    if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1))
                    {
                        audio.PlayOneShot(standSFX, 1f);
                    }
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
        if (!EventManager.inst.playerDead && !EventManager.inst.memoryPlaying)
        {
            // Normalise the increment footstep timer if strafing
            if ((Mathf.Abs(Input.GetAxis("Vertical")) > 0 && Mathf.Abs(Input.GetAxis("Horizontal")) > 0))
            {
                footStepTimer += Time.deltaTime * Mathf.Abs(Input.GetAxis("Vertical")) * Mathf.Abs(Input.GetAxis("Horizontal"));
            }
            // Otherwise use a single axis
            else
            {
                footStepTimer += Time.deltaTime * Mathf.Abs(Input.GetAxis("Vertical"));
                footStepTimer += Time.deltaTime * Mathf.Abs(Input.GetAxis("Horizontal"));
            }
            
            // Reset walk array when you reach the end
            if (currentWalkVal == (standingWalkLeftHardSFX.Length))
            {
                currentWalkVal = 0;
            }

            if (currentWalkVal == (standingWalkRightHardSFX.Length))
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
                    footStepSFXSource.volume = walkingVolume;
                    // Cycle between left and right footstep SFX arrays
                    if (footStepCount % 2 == 0)
                    {
                        // Left step
                        if (!touchingTerrain)
                        {
                            footStepSFXSource.clip = standingWalkLeftHardSFX[currentWalkVal];
                        }
                        
                        if (touchingTerrain)
                        {
                            footStepSFXSource.clip = standingWalkLeftSoftSFX[currentWalkVal];
                        }
                        
                        footStepSFXSource.pitch = Random.Range(0.8f, 1.15f);
                        footStepSFXSource.Play();
                    }
                    else
                    {
                        // Right step
                        if (!touchingTerrain)
                        {
                            footStepSFXSource.clip = standingWalkRightHardSFX[currentWalkVal];
                        }

                        if (touchingTerrain)
                        {
                            footStepSFXSource.clip = standingWalkRightSoftSFX[currentWalkVal];
                        }

                        footStepSFXSource.pitch = Random.Range(0.8f, 1.15f);
                        footStepSFXSource.Play();
                    }
                    currentWalkVal++;
                    footStepCount++;
                }
                // Play crouching footstep if not standing
                else
                {
                    //change to lower vol for crouched
                    footStepSFXSource.volume = walkingVolume * 0.5f;

                    // Cycle between left and right footstep SFX arrays
                    if (footStepCount % 2 == 0)
                    {
                        // Left step
                        if (!touchingTerrain)
                        {
                            footStepSFXSource.clip = standingWalkLeftHardSFX[currentWalkVal];
                        }

                        if (touchingTerrain)
                        {
                            footStepSFXSource.clip = standingWalkLeftSoftSFX[currentWalkVal];
                        }

                        footStepSFXSource.pitch = Random.Range(0.8f, 1.15f);
                        footStepSFXSource.Play();
                    }
                    else
                    {
                        // Right step
                        if (!touchingTerrain)
                        {
                            footStepSFXSource.clip = standingWalkRightHardSFX[currentWalkVal];
                        }

                        if (touchingTerrain)
                        {
                            footStepSFXSource.clip = standingWalkRightSoftSFX[currentWalkVal];
                        }

                        footStepSFXSource.pitch = Random.Range(0.8f, 1.15f);
                        footStepSFXSource.Play();
                        currentWalkVal++;
                        footStepCount++;
                    }

                    //Below is when we had edited seperate footsteps for crouched. Use the same ones at a lower volume now.
                    /*
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
                    footStepCount++;*/
                }
                footStepTimer = 0;
            }
        }
    }
	
	// Calculate jump speed and height
    //float CalculateJumpVerticalSpeed ()
    //{
    //    return Mathf.Sqrt(2 * jumpHeight * gravity);
    //}

    void OnLevelWasLoaded()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
    }
}