using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScEnemies : MonoBehaviour
{
    protected mobstate mystate;
    protected (GameObject, ScBullet) lastShotBullet;

    protected Transform myTrans;
    public virtual void Shoot()
    { }
}
