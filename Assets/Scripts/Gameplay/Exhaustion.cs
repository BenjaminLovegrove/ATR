using UnityEngine;
using System.Collections;

public class Exhaustion : MonoBehaviour {

    public AudioSource breathing;
    public float totalDistance;
    public float maxVol;
    public Transform endPoint;
    private float closestDist;
   
    void Start()
    {
        closestDist = totalDistance;
    }

	void Update () {
        if (Vector3.Distance (transform.position, endPoint.position) < closestDist)
        {
            closestDist = Vector3.Distance(transform.position, endPoint.position);
        }

        print(Vector3.Distance(transform.position, endPoint.position));

        breathing.volume = Mathf.Lerp(maxVol, 0, (closestDist / totalDistance));
	}
}
