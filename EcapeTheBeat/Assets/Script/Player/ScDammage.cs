using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEditor;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

public class ScDammage : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] int maxHp;
    [SerializeField] int dammagePerHit;
    [SerializeField] float recoceryDelay;
    [SerializeField] float recoveryEfficiency;
    [SerializeField] PlayableDirector timeline;
    [SerializeField] SpriteRenderer core;
    [SerializeField] ParticleSystem death;

    float currentHp;
    float lastHitTime;
    bool isDead;
    PlayerInput inputs;
    AudioSource myAudio;

    private void Start()
    {
        currentHp = maxHp;
        inputs = gameObject.GetComponent<PlayerInput>();
        myAudio = gameObject.GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("bullet"))
            {
                currentHp -= dammagePerHit;
                if (currentHp < 0 && !isDead)
                {
                    timeline.Stop();
                    inputs.SwitchCurrentActionMap("gameOver");

                    currentHp = 0;
                    isDead = true;
                    myAudio.Play();
                    Invoke("DeathAnim",1);
                }

                UpdateSlider();
                lastHitTime = 0;
            }
        }
    }

    private void Update()
    {
        lastHitTime += Time.deltaTime;
        if (lastHitTime > recoceryDelay && currentHp<maxHp && !isDead)
        {
            currentHp += (recoveryEfficiency * Time.deltaTime);
            UpdateSlider();
        }//player healing 
    }

    private void UpdateSlider()
    {
        slider.value = (currentHp / maxHp);
    }

    private void DeathAnim()
    {
        core.enabled = false;
        death.Play();
        Invoke("EndMenue", 3);
    }

    private void EndMenue()
    {
        ScGameOver.Instance.PlayerDead();
    }
}
