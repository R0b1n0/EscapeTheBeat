using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScBeam : ScEnemies
{
    [SerializeField] float loadingTime;
    [SerializeField] int bulletPerShoot;
    [SerializeField] float fireRate;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float bulletSpeed;
    private int bulletLeft;
    private Transform playerTrans;
    private Vector3 positionOnshoot;
    private Vector2 attackDir;
    private float lifeSpan;
    private float lastAttackTime;


    private void Start()
    {
        myTrans = transform;
    }

    private void Update()
    {

        switch (mystate)
        {
            case mobstate.loading:
                lifeSpan -= Time.deltaTime;

                if (lifeSpan <= 0)
                {
                    mystate = mobstate.attacking;
                    ScScreenShake.Instance.Shake(0.35f, 0.15f);
                }
                break;

            case mobstate.attacking:
                Attack();
                break;

            case mobstate.idle:
                ScMobMan.Instance.GetBeam(this);
                gameObject.SetActive(false);
                break;
        }
    }

    private void Attack()
    {
        lastAttackTime += Time.deltaTime;
        if (bulletLeft > 0)
        {
            if (lastAttackTime > fireRate)
            {
                lastAttackTime = 0;
                bulletLeft--;
                lastShotBullet = ScBullMan.Instance.CanIGetABullet();
                lastShotBullet.Item1.SetActive(true);
                lastShotBullet.Item1.transform.position = myTrans.position;
                lastShotBullet.Item2.SetUpBullet(attackDir, bulletSpeed, false);
            }
        }
        else
            mystate = mobstate.idle;
        
    }

    public override void Shoot()
    {
        lifeSpan = loadingTime;
        mystate = mobstate.loading;
        bulletLeft = bulletPerShoot;
        lastAttackTime = 0;

        int seed = Random.Range(0, 2);
        //seed determine if the shot start from the (top/bottom) or the (right/left) area
        if (seed == 0) //(top/bottom)
        {
            positionOnshoot.Set(Random.Range(-24f,24f), 14 * RandomNum(), 0);
        }
        else  //(right/left)
        {
            positionOnshoot.Set(24* RandomNum(), Random.Range(-14f, 14f) , 0);
        }
        if (myTrans!=null)
        {
            myTrans.position = positionOnshoot;
            SetupLaserBeam(myTrans, playerTrans);
        }
        else
        {
            playerTrans = GameObject.Find("Player").transform;
            transform.position = positionOnshoot;
            SetupLaserBeam(transform, playerTrans);
        }
    }

    private void SetupLaserBeam(Transform start, Transform end)
    {
        lineRenderer.SetPosition(0, start.position);
        lineRenderer.SetPosition(1, start.position + ((end.position - start.position).normalized * 100));
        attackDir.Set(end.position.x-start.position.x, end.position.y - start.position.y);
    }

    private int RandomNum()
    {
        var result = Random.value;
        if (result < 0.5f)
            return -1;
        else
            return 1;
    }
}

public enum mobstate
{
    loading,
    attacking,
    idle
}
