using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float healthAmount = 100f;
    public float currentHealth;

    [SerializeField] Transform attackPos;
    [SerializeField] LayerMask playerMask;

    NavMeshAgent _agent;
    Ragdoll _ragdoll;

    GameObject _player;

    Collider[] colliders;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        currentHealth = healthAmount;
        _ragdoll = GetComponent<Ragdoll>();

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (!_agent.enabled) return;
        ChasePlayer();
    }

    void ChasePlayer()
    {
        if(Vector3.Distance(transform.position, _player.transform.position) < 2)
        {
            Debug.Log("ATTACKED");
            Attack();
        }
        else
        {
            _agent.SetDestination(_player.transform.position);
        }
    }

    void Attack()
    {
       Collider[] playerColliders = Physics.OverlapSphere(attackPos.position, 3, 1 << 8);

        if(playerColliders.Length > 0)
        {
            Debug.Log(playerColliders.Length);
        }
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
