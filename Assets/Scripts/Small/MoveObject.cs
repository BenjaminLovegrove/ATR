using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour
{
    public GameObject objToMove;
    public int moveSpeed;
    public enum MoveDirection
    {
        FORWARD, BACKWARD, LEFT, RIGHT
    }

    public MoveDirection moveDirection;

    void Awake()
    {
        if (objToMove == null)
        {
            objToMove = this.gameObject;
        }
    }

	void Update ()
    {
        switch (moveDirection)
        {
            case MoveDirection.FORWARD:
                objToMove.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                break;
            case MoveDirection.BACKWARD:
                objToMove.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
                break;
            case MoveDirection.LEFT:
                objToMove.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                break;
            case MoveDirection.RIGHT:
                objToMove.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                break;
        }
	}
}
