using UnityEngine;
using System.Collections;

public class Exhaustion : MonoBehaviour {

    [Header("Player Slowing")]
    public float lowestSpeed;
    public float lowestSens;

    [Header("BGM")]
    public AudioSource BGM;
    private float bgmMaxVol;
    public EndingTrigger audioSwitcher;

    [Header("Breathing")]
    public AudioSource breathing;
    public float maxVol;
    public float startPitch;
    public float endPitch;
    private AudioSource ambientBreathing;
    private float ambientBreathingVol;
    private float targBreathingVol;

    [Header("Distance")]
    public float totalDistance;
    public Transform endPoint;
    private float closestDist;
   
    void Start()
    {
        //Set closest dist to be above maxdist
        ambientBreathing = GameObject.Find("BreathingSFX").GetComponent<AudioSource>();
        ambientBreathingVol = ambientBreathing.volume;
        closestDist = totalDistance;
        bgmMaxVol = BGM.volume;
    }

	void Update () {
        //Set closest distance
        if (Vector3.Distance (transform.position, endPoint.position) < closestDist)
        {
            closestDist = Vector3.Distance(transform.position, endPoint.position);
        }

        //Lerp Values
        float lerpValue = (closestDist / totalDistance);
        targBreathingVol = Mathf.Lerp(maxVol, -0.15f, lerpValue);

        if (targBreathingVol > ambientBreathingVol)
        {
            breathing.volume = targBreathingVol;
            ambientBreathing.Pause();
        }

        breathing.pitch = Mathf.Lerp(endPitch, startPitch, lerpValue);

        if (audioSwitcher.switchedTrack)
        {
            BGM.volume = Mathf.Lerp(0.15f, bgmMaxVol, lerpValue);
            EventManager.inst.memoryMoveScalar = Mathf.Lerp(lowestSpeed, 1, lerpValue);
            EventManager.inst.memoryLookScalar = Mathf.Lerp(lowestSens, 1, lerpValue);
        }

    }
}
