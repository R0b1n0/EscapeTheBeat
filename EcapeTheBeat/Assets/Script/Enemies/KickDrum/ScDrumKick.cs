using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScDrumKick : ScEnemies
{
    [SerializeField] int bulletCountPerShoot;

    private float angleGapOnShoot;

    void Start()
    {
        angleGapOnShoot = 360 / bulletCountPerShoot;
        myTrans = transform;
        ScOrchestra.Instance.drumKickEvent.AddListener(Shoot);
    }

    public override void Shoot()
    {
        for (int i=0; i< bulletCountPerShoot; i++)
        {
            lastShotBullet = ScBullMan.Instance.CanIGetABullet();

            lastShotBullet.Item1.SetActive(true);
            lastShotBullet.Item1.transform.position = myTrans.position + new Vector3(Mathf.Cos(i * angleGapOnShoot), Mathf.Sin(i * angleGapOnShoot), 0);
            lastShotBullet.Item2.SetUpBullet(lastShotBullet.Item1.transform.position - myTrans.position, 0.25f, false);
        }

    }
}
