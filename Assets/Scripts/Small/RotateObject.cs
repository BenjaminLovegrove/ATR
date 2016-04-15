using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{
    public GameObject objToRotate;
    public int rotationSpeed;

    [Header("0, 0, -1")]
    public bool rotateBack;

    [Header("0, -1, 0")]
    public bool rotateDown;

    [Header("0, 1, 0")]
    public bool rotateUp;

    [Header("-1, 0, 0")]
    public bool rotateLeft;

    [Header("1, 0, 0")]
    public bool rotateRight;

    [Header("0, 0, 1")]
    public bool rotateForward;

    void Awake()
    {
        if (objToRotate == null)
        {
            objToRotate = this.gameObject;
        }
        else print("Error Assigning Game Obj");
    }
	
	void FixedUpdate ()
    {
        if (rotateBack)
        {
            objToRotate.transform.Rotate(Vector3.back * Time.deltaTime * rotationSpeed);
        }
        if (rotateDown)
        {
            objToRotate.transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);
        }
        if (rotateUp)
        {
            objToRotate.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        }
        if (rotateLeft)
        {
            objToRotate.transform.Rotate(Vector3.left * Time.deltaTime * rotationSpeed);
        }
        if (rotateRight)
        {
            objToRotate.transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
        }
        if (rotateForward)
        {
            objToRotate.transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        }
	}
}
