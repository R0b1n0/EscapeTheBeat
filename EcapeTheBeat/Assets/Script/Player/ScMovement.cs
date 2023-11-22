using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScMovement : MonoBehaviour
{
    [SerializeField] float speed; // Unit per seconde
    [SerializeField] int dashLenght; // Unit per seconde

    private Vector3 movementDir;
    private Transform myTrans;
    private float translationPerFrame;

    private void Start()
    {
        translationPerFrame = Time.fixedDeltaTime * speed;
        myTrans = transform;
    }

    private void FixedUpdate()
    {
        MoveAround();
    }

    public void GetMovementDirection(Vector2 direction)
    {
        movementDir = direction.normalized;
    }
    public void GetDashInstruction(Vector2 direction)
    {
        Dash(direction.normalized);
    }
    private void MoveAround()
    {
        myTrans.position = myTrans.position + (movementDir* translationPerFrame);
    }
    private void Dash(Vector3 direction)
    {
        myTrans.position = myTrans.position + (direction * dashLenght);
    }
}
