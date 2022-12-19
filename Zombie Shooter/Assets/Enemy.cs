using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamagable
{
    public float healthAmount = 100f;
    float _currentHealth;

    [SerializeField] Image hpFillImage;

    private void Start()
    {
        _currentHealth = healthAmount;
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        hpFillImage.fillAmount = _currentHealth / healthAmount;

        if(_currentHealth < 1)
        {
            OnDeath();
        }
    }
}
