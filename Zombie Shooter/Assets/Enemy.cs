using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamagable
{
    public float healthAmount = 100f;

    [SerializeField] Image hpFillImage;

    public void OnDeath()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;

        hpFillImage.fillAmount = healthAmount / 100;

        if(healthAmount < 1)
        {
            OnDeath();
        }
    }
}
