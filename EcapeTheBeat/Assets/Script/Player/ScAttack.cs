using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScAttack : MonoBehaviour
{
    [SerializeField] List<ScCanon> canonList;
    private Vector3 aimingDir;
    private Transform myTrans;
    private float canonAngle;
    public static playerState myState;

    private void Start()
    {
        myTrans = transform;
        myState = playerState.idle;
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
            myTrans.rotation = Quaternion.Euler(0, 0, canonAngle);
        }
    }

    private void GetAngle()
    {
        canonAngle = Vector2.Angle(Vector2.right, aimingDir) - 90;

        if (aimingDir.y < 0)
            canonAngle = 180 - canonAngle;
    }

    public void IsAttacking(bool isAttacking)
    {
        if (isAttacking)
        {
            myState = playerState.attacking;
        }
        else
        {
            myState = playerState.idle;
        }
    }

    private void Attack()
    {
        switch (myState)
        {
            case playerState.attacking:
                foreach (ScCanon canon in canonList)
                {
                    canon.Shoot();
                }
                break;
        }
    }

}

public enum playerState
{
    idle, 
    attacking,
    onCoolDown
}
