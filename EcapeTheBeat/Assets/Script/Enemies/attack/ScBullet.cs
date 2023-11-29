using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBullet : MonoBehaviour
{
    private float speed;

    private Transform myTrans;
    private GameObject go;
    private Vector3 direction;
    private int Xbound = 25;
    private int Ybound = 15;

    private void Start()
    {
        myTrans = transform;
        go = gameObject;
    }

    public void SetUpBullet(Vector2 dir, float bullSpeed, bool enhancedBoundries)
    {
        direction = dir.normalized;
        speed = bullSpeed;
        if (enhancedBoundries) 
        {
            Xbound = 30;
            Ybound = 30;
        }
        else
        {
            Xbound = 25;
            Ybound = 15;
        }
    }

    private void FixedUpdate()
    {
        myTrans.position = (myTrans.transform.position + direction * speed);
        MapBound();
    }

    private void MapBound()
    { 

        if (Mathf.Abs(myTrans.position.x) > Xbound || Mathf.Abs(myTrans.position.y) > Ybound)
        {
            ScBullMan.Instance.GetRetiredBullet(go);
        }
    }
}
