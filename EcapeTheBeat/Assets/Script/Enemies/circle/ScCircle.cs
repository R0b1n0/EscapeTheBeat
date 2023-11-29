using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ScCircle : ScEnemies
{
    [SerializeField] float circleDuration;
    [SerializeField] float minCircle;
    [SerializeField] float circleApparitionPace;
    [SerializeField] int ballPerCircle;

    float lastCircleBirth;
    float angleGapBetweenBalls;
    float attackBegins;
    Vector3 ballSpawnpos;
    float offset;

    private List<List<Transform>> circles = new List<List<Transform>>();

    private void Start()
    {
        angleGapBetweenBalls = 360f / ballPerCircle;
        mystate = mobstate.idle;
        offset = 20;
    }

    private void FixedUpdate()
    {
        if (mystate == mobstate.attacking)
        {
            lastCircleBirth += Time.deltaTime;

            // create a new circle 
            if (lastCircleBirth > circleApparitionPace) 
            {
                offset += offset;
                lastCircleBirth = 0f;
                circles.Add(new List<Transform>());
                for (int i = 0; i < ballPerCircle; i++)
                {
                    var tempo = ScBullMan.Instance.CanIGetABullet();
                    tempo.Item1.SetActive(true);
                    tempo.Item2.SetUpBullet(Vector2.zero, 0, true);
                    ballSpawnpos.Set(Mathf.Cos(((i * angleGapBetweenBalls)+offset) * Mathf.Deg2Rad), Mathf.Sin(((i * angleGapBetweenBalls) + offset) * Mathf.Deg2Rad), 0);
                    tempo.Item1.transform.position = ballSpawnpos * 28;
                    circles[circles.Count - 1].Add(tempo.Item1.transform);
                }
            }
            //move Circles
            if (circles.Count != 0)
            {
                foreach (var circle in circles)
                {
                    foreach (var balls in circle)
                    {
                        balls.position = Vector3.Lerp(balls.position, balls.position.normalized* minCircle, Time.deltaTime*2);
                    }
                }
            }

            attackBegins += Time.deltaTime;
            if (attackBegins > circleDuration)
            {
                mystate = mobstate.idle;
                foreach (var circle in circles)
                {
                    foreach (var balls in circle)
                    {
                        balls.GetComponent<ScBullet>().SetUpBullet(balls.position.normalized, 2, false);
                    }
                }
                circles.Clear();
            }

        }

        
    }

    public override void Shoot()
    {
        lastCircleBirth = 100;
        offset = 20;
        attackBegins = 0;
        mystate = mobstate.attacking;
    }
}
