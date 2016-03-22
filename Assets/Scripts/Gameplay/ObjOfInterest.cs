using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]

public class ObjOfInterest : MonoBehaviour
{
    // Script to attach to game objects that you wish to play a one off sound when the player looks in that direction
    // Uses the same 3 point detection as enemy AI
    // Adjust the collider size and field of view according to the distance and angle you want the audio to trigger
    // You also need to be considerate of the objects orientation

    public float fieldOfViewAngle;
    public AudioClip playSFX;
    private bool playerInRange;
    private bool playerInLineOfSight;
    private AudioSource audio;

    IEnumerator ActivateAudio()
    {
        audio.clip = playSFX;
        if (!audio.isPlaying)
        {
            audio.Play();
        }        
        yield return new WaitForSeconds(playSFX.length * 0.99f);
        Destroy(this);
    }

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

	void Update ()
    {
        PlayerDetection();
	}

    void PlayerDetection()
    {
        if (playerInLineOfSight && playerInRange)
        {
            StartCoroutine("ActivateAudio");
        }
    }

    // 3 POINT PLAYER DETECTION
    // First checks if the player is within range of the Enemy vision - large trigger collider attached to enemy
    // Then checks if the player is within the field of view - field of view variable
    // Lastly checks if the player is in direct line of sight - raycast
    void OnTriggerStay(Collider col)
    {
        // If the player has entered the trigger sphere
        if (col.gameObject.tag == "Player")
        {
            playerInRange = true;

            // Create a vector from the object to the player and store the angle between it and forward.
            Vector3 direction = col.transform.position - transform.position;

            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, direction.normalized, out hit))
                {
                    // If the raycast hits the player
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        playerInLineOfSight = true;
                    }
                    else playerInLineOfSight = false;
                }
            }            
        }
    }

    // If the player leaves the trigger area
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
