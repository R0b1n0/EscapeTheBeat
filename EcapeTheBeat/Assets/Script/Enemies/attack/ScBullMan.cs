using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBullMan : MonoBehaviour
{
    public static ScBullMan Instance;


    [SerializeField] GameObject bulletGoRef;
    private List <GameObject> bullets = new List <GameObject> ();
    private Dictionary<GameObject,ScBullet> ScBulletList = new Dictionary<GameObject,ScBullet> ();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }
    }
    public (GameObject,ScBullet) CanIGetABullet()
    {
        if (bullets.Count > 0)
        {
            var result = bullets[0];
            bullets.RemoveAt (0);
            return (result, ScBulletList[result]);
        }
        else
        {
            var tempo = Instantiate(bulletGoRef);
            var scriptref = tempo.GetComponent<ScBullet>();

            ScBulletList.Add(tempo, scriptref);
            return (tempo, scriptref);
        }

    }

    public void GetRetiredBullet(GameObject inactivBullet)
    {
        inactivBullet.SetActive (false);
        bullets.Add (inactivBullet);
    }
}