using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScPlayerBullMan : MonoBehaviour
{
    public static ScPlayerBullMan Instance;

    private List <GameObject> bullets = new List <GameObject> ();


    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else 
            Instance = this;

    }
    public GameObject CanIGetABullet()
    {
        if (bullets.Count > 0)
        {
            var result = bullets[0];
            bullets.RemoveAt (0);
            Debug.Log ("bullet exist");
            return result;
        }
        else 
            return null;
    }

    public void GetRetiredBullet(GameObject inactivBullet)
    {
        inactivBullet.SetActive (false);
        bullets.Add (inactivBullet);
        Debug.Log("bullet retired");
    }
}
