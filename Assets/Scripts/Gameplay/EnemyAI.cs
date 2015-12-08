using UnityEngine;
using System.Collections;

// All enemy AI functions are handled here.
// Movement, player detection, animation, audio etc

public class EnemyAI : MonoBehaviour
{
	public float patrolSpeed = 2f;
	public float chaseSpeed = 5f;
	public float chaseWaitTime = 5f;
	public float chaseTimer = 5f;
	public float patrolWaitTime = 1f;
	public Transform[] patrolWayPoints;
	public float patrolTimer = 0f;

    AudioSource audio;
    public AudioClip gunShot;
    public AudioClip[] radioChatter;
    public AudioClip footSteps;
    public float audioTimer;
    public bool audioPlaying;
    public float audioLength;
    public float walkTimer;

	public float fieldOfViewAngle = 110f;

    public bool playerInLineOfSight;
    public bool playerInRange;
	private bool playerDead;
    
    public bool wayPointLooping;
    public bool staticEnemy;

	public int wayPointIndex;

	private NavMeshAgent nav;
	private Transform playerTransform;
	private Animator anim;
	private int playerHp;
	private bool playerCrouch;

	Vector3 lastPosition = Vector3.zero;
	private float speed;
    private float shootAnimTimer;
    private float switchIdletimer;

	// Death co-routine
	IEnumerator Death()
	{
        EventManager.inst.controlsDisabled = true;
        EventManager.inst.playerDead = true;
        EventManager.inst.enemyKillPos = this.gameObject.transform;
        yield return new WaitForSeconds(6f);
        EventManager.inst.resetLevel = true;
    }

	void Awake()
	{
        audio = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();        
	}

    void Start()
    {
        // TODO this is dodgey, fix later
        playerTransform = EventManager.inst.playerTrans;
        playerCrouch = EventManager.inst.playerCrouch;
        //playerDead = EventManager.inst.playerDead;
    }

	void FixedUpdate ()
	{
        // Reset radio chat status upon completion of play
        if (audioTimer >= audioLength + 1)
        {
            audioPlaying = false;
            audioTimer = 0;
        }

        RadioChatter();
        CalculateVelocity();
		PlayerDetection ();
        AnimationTriggers();
	}

    // Randomly pick an audioclip from radio chatter array to be played
    void RadioChatter()
    {
        if (!audioPlaying && radioChatter.Length > 0)
        {
            int rand = Random.Range(0, radioChatter.Length);

            audioLength = radioChatter[rand].length;

            audio.PlayOneShot(radioChatter[rand], 1f);

            audioPlaying = true;
        }

        if (audioPlaying)
        {
            audioTimer += Time.deltaTime;
        }    
    }

    // A velocity to determine which animation should be played
    void CalculateVelocity()
    {
        speed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
    }

    // Toggle bolean values for the animation controller to trigger animations
    void AnimationTriggers()
    {
        // Idle state
        if (speed < 0.01f && !playerDead)
        {
            anim.SetBool("stopping", true);
            anim.SetBool("walking", false);

            // While AI is idling increment transition timer
            switchIdletimer += (Time.deltaTime * Random.Range(0.1f, 3f));
            // Set the animator timer equal to script timer
            anim.SetFloat("idleTimer", switchIdletimer);
        }

        // Transition through idle anims
        // *** Set the required transition timer between idle
        // anims to ~1sec less than the value stated in this check
        // (in the animation controller)
        if (switchIdletimer > 15)
        {
            switchIdletimer = 0;
        }      

        // Walk state
        if (speed > 0.011f && !playerDead)
        {
            anim.SetBool("stopping", false);
            anim.SetBool("walking", true);

            walkTimer += Time.deltaTime;

            // Play footsteps SFX
            if (walkTimer > 1.5f)
            {
                audio.PlayOneShot(footSteps, 1f);
                walkTimer = 0;
            }
            
        }

        // Shoot state
        if (playerDead)
        {
            shootAnimTimer += Time.deltaTime;
        }

        if (shootAnimTimer > 2)
        {
            anim.SetBool("shooting", false);
        }
    }

    // If the Enemy has successfully detected the player
	void PlayerDetection()
	{
		// If the player is in range and line of sight and is not already dead
		if (playerInLineOfSight && playerInRange)
		{
			//print ("Firing!");
			StartCoroutine("Death");          

            // Shoot animation and audio
			if (!playerDead)
			{
                //Vector3 direction = transform.position - playerTransform.position;
                //Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
                //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.3f * Time.deltaTime);

                //Vector3 dirToPlayer = transform.position - playerTransform.position;
                //Transform startPos = transform;
                //Transform endPos = playerTransform;
                //transform.rotation = Quaternion.Lerp(startPos.rotation, endPos.rotation, 0.3f);

                //transform.LookAt(Quaternion.Lerp(Vector3.forward, dirToPlayer));

                transform.LookAt(playerTransform);
                audio.PlayOneShot(gunShot, 1f);
                anim.SetBool("stopping", false);
                anim.SetBool("walking", false);
                anim.SetBool("shooting", true);
			}
			playerDead = true;
			nav.Stop();
		}
		else
			Patrolling();
	}
	
	// Movement of the Enemy model
	void Patrolling ()
	{
        if (!staticEnemy)
        {
            // Set an appropriate speed for the NavMeshAgent.
            nav.speed = patrolSpeed;

            // If near the next waypoint or there is no destination
            if (nav.remainingDistance < nav.stoppingDistance)
            {
                patrolTimer += Time.deltaTime;

                // If the timer exceeds the wait time increment the wayPointIndex
                if (patrolTimer >= patrolWaitTime)
                {
                    // Waypoint looping
                    if (wayPointIndex == patrolWayPoints.Length - 1 && wayPointLooping)
                        wayPointIndex = 0;
                    else wayPointIndex++;

                    // Non waypoint looping
                    if (wayPointIndex == patrolWayPoints.Length - 1 && !wayPointLooping)
                    {
                        nav.Stop();
                    }

                    // Reset the timer.
                    patrolTimer = 0;
                }
            }
            else
                // If not near a destination, reset the timer.
                patrolTimer = 0;

            // Set the destination to the patrolWayPoint.
            nav.destination = patrolWayPoints[wayPointIndex].position;
        }
	}
    
    // 3 POINT PLAYER DETECTION
    // First checks if the player is within range of the Enemy vision - large trigger collider attached to enemy
    // Then checks if the player is within the field of view - field of view variable
    // Lastly checks if the player is in direct line of sight - raycast
	void OnTriggerStay (Collider col)
	{
		// If the player has entered the trigger sphere
		if(col.gameObject.tag == "Player")
		{
			//print ("Player within detection range");

			playerInRange = true;

            if (EventManager.inst.playerCrouch == false)
            {

                // Create a vector from the enemy to the player and store the angle between it and forward.
                Vector3 direction = col.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);
                
                if (angle < fieldOfViewAngle * 0.5f)
                {
                    //print ("Player within field of view");

                    RaycastHit hit;

                    if (Physics.Raycast(transform.position, direction.normalized, out hit))
                    {
                        // If the raycast hits the player
                        if (hit.collider.gameObject.tag == "Player")
                        {
                            //print ("Player within line of sight");
                            playerInLineOfSight = true;
                        }
                        else playerInLineOfSight = false;
                    }
                }
            }
		}

	}

    // If the player leaves the trigger area
	void OnTriggerExit (Collider other)
	{		
		if (other.gameObject.tag == "Player")
        {
	        //print ("Player exited detection range");
			playerInRange = false;
        }
	}

    // This is for the inner collider that detects the player
    // regardless if the player is crouching
    void OnTriggerEnter(Collider col)
    {
        if (playerInRange && col.tag == "Player")
        {
            Vector3 direction = col.transform.position - transform.position;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, direction.normalized, out hit))
            {
                // If the raycast hits the player
                if (hit.collider.gameObject.tag == "Player")
                {
                    //print ("Player within line of sight");
                    playerInLineOfSight = true;
                }
                else playerInLineOfSight = false;
            }
        }
    }

	// Movement between way points
	float CalculatePathLength (Vector3 targetPosition)
	{
		// Create a path and set it based on a target position.
		NavMeshPath path = new NavMeshPath();
		if(nav.enabled)
			nav.CalculatePath(targetPosition, path);
		
		// Create an array of points which is the length of the number of corners in the path + 2.
		Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
		
		// The first point is the enemy's position.
		allWayPoints[0] = transform.position;
		
		// The last point is the target position.
		allWayPoints[allWayPoints.Length - 1] = targetPosition;
		
		// The points inbetween are the corners of the path.
		for(int i = 0; i < path.corners.Length; i++)
		{
			allWayPoints[i + 1] = path.corners[i];
		}
		
		// Create a float to store the path length that is by default 0.
		float pathLength = 0;
		
		// Increment the path length by an amount equal to the distance between each waypoint and the next.
		for(int i = 0; i < allWayPoints.Length - 1; i++)
		{
			pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
		}
		
		return pathLength;
	}
	
}

