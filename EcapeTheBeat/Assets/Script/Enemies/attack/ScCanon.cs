using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScCanon : MonoBehaviour
{
    [SerializeField] Transform ShootingSpot;
    [SerializeField] GameObject bullet;
    private Transform myTrans;
    private Vector2 shootingDir;

    private void Start()
    {
        myTrans = transform;
    }

    public void Shoot()
    {
        /*var tempo = ScBullMan.Instance.CanIGetABullet();
        shootingDir = (ShootingSpot.position - myTrans.position).normalized;
        if (tempo != null)
        {
            tempo.SetActive(true);
            tempo.transform.position = ShootingSpot.position;
            tempo.GetComponent<ScBullet>().SetUpBullet(shootingDir);
        }
        else
        {
            var tempoSc = Instantiate(bullet, ShootingSpot.position, Quaternion.identity).GetComponent<ScBullet>();
            tempoSc.SetUpBullet(shootingDir);
        }*/
    }
}
