using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScMovement : MonoBehaviour
{
    [SerializeField] float speed; // Unit per seconde
    [SerializeField] int dashLenght; // Unit per seconde
    [SerializeField] LineRenderer lineRenderer;

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
        ResizeDashTrail();

    }
    private void Update()
    {
        CheckMapBound();
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
        lineRenderer.SetPosition(0,myTrans.position);
        myTrans.position = myTrans.position + (direction * dashLenght);
        lineRenderer.SetPosition(1, myTrans.position);
    }


    private void CheckMapBound()
    {
        if (myTrans.position.x < -23)
            myTrans.position = new Vector3(-23f, myTrans.position.y,0);

        if (myTrans.position.x > 23)
            myTrans.position = new Vector3(23f, myTrans.position.y, 0);

        if (myTrans.position.y > 13)
            myTrans.position = new Vector3(myTrans.position.x, 13, 0);

        if (myTrans.position.y < -13)
            myTrans.position = new Vector3(myTrans.position.x, -13, 0);

    }
    private void ResizeDashTrail()
    {
        lineRenderer.SetPosition(0, Vector3.Lerp(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1), Time.deltaTime*2) );
    }
}
