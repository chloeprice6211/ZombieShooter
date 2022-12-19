using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamagable
{
    public float healthAmount = 100f;
    float _currentHealth;

    [SerializeField] Image hpFillImage;
    NavMeshAgent _agent;

    [SerializeField] GameObject player;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currentHealth = healthAmount;

    }

    private void Update()
    {
        ChasePlayer();
    }

    void ChasePlayer()
    {
        _agent.SetDestination(player.transform.position);
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
