using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float healthAmount = 100f;
    float _currentHealth;

    [SerializeField] Image hpFillImage;
    NavMeshAgent _agent;
    Ragdoll _ragdoll;

    [SerializeField] GameObject player;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currentHealth = healthAmount;
        _ragdoll = GetComponent<Ragdoll>();

        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        if (!_agent.enabled) return;

        ChasePlayer();
    }

    void ChasePlayer()
    {
        _agent.SetDestination(player.transform.position);
    }

    public void OnDeath()
    {
        _ragdoll.ActiveRagdoll();
        _agent.enabled = false;
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
