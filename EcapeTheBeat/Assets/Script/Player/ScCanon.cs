using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScCanon : MonoBehaviour
{
    [SerializeField] Transform canonTrans;
    private Vector3 aimingDir;
    private Transform myTrans;
    private float canonAngle;

    private void Start()
    {
        myTrans = transform;
    }

    private void FixedUpdate()
    {
        TurnCanon();
    }

    public void GetAimingDirection(Vector2 aim)
    {
        aimingDir = aim.normalized;
    }

    public void TurnCanon()
    {
        if (aimingDir != Vector3.zero)
        {
            GetAngle();
            myTrans.rotation = Quaternion.Euler(0,0, canonAngle);
        }
    }

    private void GetAngle()
    {
        canonAngle = Vector2.Angle(Vector2.right, aimingDir) - 90;

        if (aimingDir.y < 0)
            canonAngle = 180 - canonAngle;
    }
}
