using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scspray : ScEnemies
{
    [SerializeField] Transform destination;
    [SerializeField] Transform canonOutPut;
    [SerializeField] float travelTimeIn;
    [SerializeField] float howLongShouldIShoot;
    [SerializeField] float shootCoolDown;
    [SerializeField] float lowANgle;
    [SerializeField] float highANgle;
    [SerializeField] AnimationCurve rotationCurve;

    float inwardIterate;
    float shootingTime;
    float lastShotTime;
    bool aimingUpward;
    Vector2 shootingDir;
    Vector3 opPos;

    private void Start()
    {
        opPos = transform.position;
        myTrans = transform;
        mystate = mobstate.idle;
    }

    private void Update()
    {
        switch (mystate)
        {
            case mobstate.loading:
                //get in the frame 
                inwardIterate += Time.deltaTime;
                myTrans.position = Vector3.Lerp(myTrans.position, destination.position,  Time.deltaTime * 2);

                if (inwardIterate > travelTimeIn)
                {
                    mystate = mobstate.attacking;
                    inwardIterate = 0;
                }
                break;

            case mobstate.attacking:

                shootingTime += Time.deltaTime;

                if (shootingTime > howLongShouldIShoot)
                {
                    inwardIterate += Time.deltaTime;
                    if (inwardIterate < travelTimeIn)
                        myTrans.position = Vector3.Lerp(myTrans.position, opPos, Time.deltaTime * 3);
                    else
                        mystate = mobstate.idle;
                }
                else
                {
                    ShootAround();
                }
                break;
        }
    }

    private void ShootAround()
    {
        lastShotTime += Time.deltaTime;
        if (lastShotTime > shootCoolDown)
        {
            myTrans.rotation = Quaternion.Euler(0,0, myTrans.rotation.z + (rotationCurve.Evaluate(Time.time) * 90));

            lastShotBullet = ScBullMan.Instance.CanIGetABullet();
            //shootingDir = (canonOutPut.position - myTrans.position).normalized;
            lastShotBullet.Item1.SetActive(true);
            lastShotBullet.Item1.transform.position = myTrans.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * rotationCurve.Evaluate(Time.time) * 90), Mathf.Sin(Mathf.Deg2Rad * rotationCurve.Evaluate(Time.time) * 90), 0);
            lastShotBullet.Item2.SetUpBullet(lastShotBullet.Item1.transform.position - myTrans.position, 0.5f, false);
            lastShotTime = 0;
        }
    }

    public override void Shoot()
    {
        inwardIterate = 0;
        shootingTime = 0;
        lastShotTime = 0;
        mystate = mobstate.loading;
    }
}
