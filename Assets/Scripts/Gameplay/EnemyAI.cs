using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public float patrolSpeed = 2f;							// The nav mesh agent's speed when patrolling.
	public float chaseSpeed = 5f;							// The nav mesh agent's speed when chasing.
	public float chaseWaitTime = 5f;						// The amount of time to wait when the last sighting is reached.
	public float chaseTimer = 5f;
	public float patrolWaitTime = 1f;						// The amount of time to wait when the patrol way point is reached.
	public Transform[] patrolWayPoints;						// An array of transforms for the patrol route.
	public float patrolTimer = 0f;							// A timer for the patrolWaitTime.

	public float fieldOfViewAngle = 110f;        			// Number of degrees, centred on forward, for the enemy see.
	public bool playerInSight;                  		    // Is the player in line of sight
	public bool playerInRange;								// Is the player within max detection range

	public Vector3 personalLastSighting;        		    // Last place this enemy spotted the player.
	public Vector3 previousSighting;						// Last known global position of the player

	public int wayPointIndex;

	// Reference vars
	private NavMeshAgent nav;
	private Transform playerTransform;
	private Animator animator;
	private int playerHp;
	private bool playerCrouch;


	// Death coroutine
	IEnumerator Death()
	{
		// Put shooting, death sounds and other shit in here later
		
		yield return new WaitForSeconds(2f);
		
		Application.LoadLevel (0);
	}


	void Awake()
	{
		// Set references
		animator = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();
		playerTransform = EventManager.inst.playerTrans;
		playerHp = EventManager.inst.playerHp;
		playerCrouch = EventManager.inst.playerCrouch;
	}

	void Start()
	{
		animator.SetBool ("Move", true);
	}

	void FixedUpdate ()
	{
		PlayerDetection ();
		EnemySight ();

		// Create a vector from the enemy to the player and store the angle between it and forward.
//		Vector3 direction = playerTransform - enemyPos;
//
//		float angle = Vector3.Angle(direction, transform.forward);
//
//		if(angle < fieldOfViewAngle * 0.5f)
//		{
//			RaycastHit hit;
//
//			if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit))
//			{
//				if(hit.collider.gameObject.tag == "Player")
//				{
//					playerInSight = true;
//					
//					EventManager.inst.lastPlayerSighting = playerTransform.transform.position;
//				}
//			}
//		}
	}


	void PlayerDetection()
	{
		// If the player is in range and line of sight and is NOT crouching
		if (playerInSight && playerInRange && playerCrouch == false)
		{
			// ... shoot.
			StartCoroutine("Death");
			print ("Firing!");
			Shooting ();
		}
		// If the player has been sighted and isn't dead...
		else if(personalLastSighting != EventManager.inst.lastPlayerSighting && playerHp > 0f)
			// ... chase.
			Chasing();
		
		// Otherwise...
		else
			// ... patrol.
			Patrolling();
	}

	void EnemySight()
	{
		// If the last global sighting of the player has changed...
		if(EventManager.inst.lastPlayerSighting != previousSighting)
			// ... then update the personal sighting to be the same as the global sighting.
			personalLastSighting = EventManager.inst.lastPlayerSighting;
		
		// Set the previous sighting to the be the sighting from this frame.
		previousSighting = EventManager.inst.lastPlayerSighting;		
	}

	
	void Shooting ()
	{
		// Stop the enemy where it is.
		nav.Stop();
		animator.SetBool ("Move", false);
		animator.SetBool ("Shoot", true);
	}

	void Chasing ()
	{
		// Create a vector from the enemy to the last sighting of the player.
		Vector3 sightingDeltaPos = personalLastSighting - transform.position;
		
		// If the the last personal sighting of the player is not close...
		if(sightingDeltaPos.sqrMagnitude > 4f)
			// ... set the destination for the NavMeshAgent to the last personal sighting of the player.
			nav.destination = personalLastSighting;
		
		// Set the appropriate speed for the NavMeshAgent.
		nav.speed = chaseSpeed;
		
		// If near the last personal sighting...
		if(nav.remainingDistance < nav.stoppingDistance)
		{
			// ... increment the timer.
			chaseTimer += Time.deltaTime;
			
			// If the timer exceeds the wait time...
			if(chaseTimer >= chaseWaitTime)
			{
				// ... reset last global sighting, the last personal sighting and the timer.
				//EventManager.inst.lastPlayerSighting = EventManager.inst.lastPlayerSighting;
				personalLastSighting = EventManager.inst.lastPlayerSighting;
				chaseTimer = 0f;
			}
		}
		else
			// If not near the last sighting personal sighting of the player, reset the timer.
			chaseTimer = 0f;
	}
	
	
	void Patrolling ()
	{
		// Set an appropriate speed for the NavMeshAgent.
		nav.speed = patrolSpeed;
		
		// If near the next waypoint or there is no destination...
		if(nav.destination == EventManager.inst.lastPlayerSighting || nav.remainingDistance < nav.stoppingDistance)
		{
			// ... increment the timer.
			patrolTimer += Time.deltaTime;
			
			// If the timer exceeds the wait time...
			if(patrolTimer >= patrolWaitTime)
			{
				// ... increment the wayPointIndex.
				if(wayPointIndex == patrolWayPoints.Length - 1)
					wayPointIndex = 0;
				else
					wayPointIndex++;
				
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

	void OnTriggerEnter (Collider col)
	{
		// If the player has entered the trigger sphere
		if(col.gameObject.tag == "Player")
		{
			print ("Player within detection range");

			playerInRange = true;
			playerInSight = false;
			
			// Create a vector from the enemy to the player and store the angle between it and forward.
			Vector3 direction = col.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);

			if(angle < fieldOfViewAngle * 0.5f)
			{
				RaycastHit hit;

				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit))
				{
					// ... and if the raycast hits the player...
					if(hit.collider.gameObject.tag == "Player")
					{

						playerInSight = true;
						
						// Set the last global sighting is the players current position.
						EventManager.inst.lastPlayerSighting = playerTransform.transform.position;
					}
				}
			}
		}
	}
	
	
	void OnTriggerExit (Collider other)
	{
		// If the player leaves the trigger area
		if (other.gameObject.tag == "Player")

			//print ("Player exited enemy collider");
			playerInSight = false;
	}
		
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

