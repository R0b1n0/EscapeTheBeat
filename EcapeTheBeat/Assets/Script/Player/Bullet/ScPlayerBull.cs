using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScPlayerBull : MonoBehaviour
{
    [SerializeField] float speed;

    private Transform myTrans;
    private GameObject go;
    private Rigidbody2D rb;
    private Vector3 direction;

    private void Start()
    {
        myTrans = transform;
        go = gameObject;
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetUpBullet(Vector2 dir)
    {
        direction = dir;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(myTrans.transform.position + direction);
        MapBound();
    }

    private void MapBound()
    { 
        if (Mathf.Abs(myTrans.position.x) > 100 || Mathf.Abs(myTrans.position.y) > 60)
        {
            ScPlayerBullMan.Instance.GetRetiredBullet(go);
        }
    }
}
