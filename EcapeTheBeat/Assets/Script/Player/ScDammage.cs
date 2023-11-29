using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScDammage : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] int maxHp;
    [SerializeField] int dammagePerHit;
    [SerializeField] float recoceryDelay;
    [SerializeField] float recoveryEfficiency;

    float currentHp;
    float lastHitTime;

    private void Start()
    {
        currentHp = maxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("bullet"))
            {
                currentHp -= dammagePerHit;
                if (currentHp < 0)
                {
                    currentHp = 0;
                }

                UpdateSlider();
                lastHitTime = 0;
            }
        }
    }

    private void Update()
    {
        lastHitTime += Time.deltaTime;
        if (lastHitTime > recoceryDelay && currentHp<maxHp)
        {
            currentHp += (recoveryEfficiency * Time.deltaTime);
            UpdateSlider();
        }//player healing 
    }

    private void UpdateSlider()
    {
        slider.value = (currentHp / maxHp);
    }
}
