using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScSnare : ScEnemies
{
    [SerializeField] int bulletCountPerShoot;
    [SerializeField] float bulletSpeed;
    [SerializeField] int minAngle;
    [SerializeField] int maxAngle;

    
    private float angleGapOnShoot;

    void Start()
    {
        angleGapOnShoot = (maxAngle - minAngle) / bulletCountPerShoot;
        myTrans = transform;
        //ScOrchestra.Instance.snareEvent.AddListener(Shoot);
    }

    public override void Shoot()
    {
        for (int i = 0; i < bulletCountPerShoot; i++)
        {
            lastShotBullet = ScBullMan.Instance.CanIGetABullet();

            lastShotBullet.Item1.SetActive(true);
            lastShotBullet.Item1.transform.position = myTrans.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * ((i * angleGapOnShoot)+minAngle)), Mathf.Sin(Mathf.Deg2Rad * ((i * angleGapOnShoot) + minAngle)), 0);
            lastShotBullet.Item2.SetUpBullet(lastShotBullet.Item1.transform.position - myTrans.position, bulletSpeed, false);
        }

    }
}
