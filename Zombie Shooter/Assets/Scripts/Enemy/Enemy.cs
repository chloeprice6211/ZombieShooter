using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float healthAmount = 100f;
    public float currentHealth;

    NavMeshAgent _agent;
    Ragdoll _ragdoll;

    GameObject player;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        currentHealth = healthAmount;
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

        GameManager.Instance.ImpactAudioSource.PlayOneShot(GameManager.Instance.killSound);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth < 1)
        {
            OnDeath();
        }
    }
}
