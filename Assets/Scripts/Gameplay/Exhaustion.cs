using UnityEngine;
using System.Collections;

public class Exhaustion : MonoBehaviour {

    [Header("Breathing")]
    public float lowestSpeed;
    public float lowestSens;

    [Header("BGM")]
    public AudioSource BGM;
    public EndingTrigger audioSwitcher;

    [Header("Breathing")]
    public AudioSource breathing;
    public float maxVol;
    public float startPitch;
    public float endPitch;

    [Header("Distance")]
    public float totalDistance;
    public Transform endPoint;
    private float closestDist;
   
    void Start()
    {
        //Set closest dist to be above maxdist
        closestDist = totalDistance;
    }

	void Update () {
        //Set closest distance
        if (Vector3.Distance (transform.position, endPoint.position) < closestDist)
        {
            closestDist = Vector3.Distance(transform.position, endPoint.position);
        }

        //Lerp Values
        breathing.volume = Mathf.Lerp(maxVol, -0.15f, (closestDist / totalDistance));
        breathing.pitch = Mathf.Lerp(endPitch, startPitch, (closestDist / totalDistance));
        if (audioSwitcher.switchedTrack)
        {
            BGM.volume = Mathf.Lerp(0, 0.45f, (closestDist / totalDistance));
        }
        EventManager.inst.memoryMoveScalar = Mathf.Lerp(lowestSpeed, 1, (closestDist / totalDistance));
        EventManager.inst.memoryLookScalar = Mathf.Lerp(lowestSens, 1, (closestDist / totalDistance));

    }
}
