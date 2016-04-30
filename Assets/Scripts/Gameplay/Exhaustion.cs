using UnityEngine;
using System.Collections;

public class Exhaustion : MonoBehaviour {

    [Header("Player Slowing")]
    public float lowestSpeed;
    public float lowestSens;
    public PlayerCam playerCamScr;
    public float headBobMod;
    private float startHeadBob;

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

    [Header("Ambience")]
    public AudioSource ambientOcean;
    private float ambientOceanMaxVol;
    public AudioSource endingWind;
    public float endingWindMaxVol;
    
   
    void Start()
    {
        //Set closest dist to be above maxdist
        ambientBreathing = GameObject.Find("BreathingSFX").GetComponent<AudioSource>();
        ambientBreathingVol = ambientBreathing.volume;
        closestDist = totalDistance;
        bgmMaxVol = BGM.volume;
        startHeadBob = playerCamScr.headBobInterval;
        ambientOceanMaxVol = ambientOcean.volume;
    }

	void Update () {

        //Set closest distance
        if (Vector3.Distance (transform.position, endPoint.position) < closestDist)
        {
            closestDist = Vector3.Distance(transform.position, endPoint.position);
        }

        //Lerp Values
        float lerpValue = (closestDist / totalDistance);
        targBreathingVol = Mathf.Lerp(maxVol, 0f, lerpValue * 1.5f);
        breathing.volume = targBreathingVol;
        ambientBreathing.volume = Mathf.Lerp(0f, ambientBreathingVol, lerpValue);
        ambientOcean.volume = Mathf.Lerp(ambientOceanMaxVol * 1.5f, ambientOceanMaxVol, lerpValue * 10);
        endingWind.volume = Mathf.Lerp(maxVol, 0f, lerpValue * 1.5f);


        breathing.pitch = Mathf.Lerp(endPitch, startPitch, lerpValue);
        playerCamScr.headBobInterval = Mathf.Lerp(playerCamScr.headBobInterval * headBobMod, playerCamScr.headBobInterval, lerpValue);

        if (audioSwitcher.switchedTrack)
        {
            BGM.volume = Mathf.Lerp(0f, bgmMaxVol + 0.2f, lerpValue);
            EventManager.inst.memoryMoveScalar = Mathf.Lerp(lowestSpeed, 1, lerpValue);
            EventManager.inst.memoryLookScalar = Mathf.Lerp(lowestSens, 1, lerpValue);
        }

    }
}
