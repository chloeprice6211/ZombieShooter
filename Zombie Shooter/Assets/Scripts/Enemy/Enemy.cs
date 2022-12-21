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
    Animator _animator;

    GameObject _player;

    private void Start()
    {
        _animator = GetComponent<Animator>();
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
            transform.LookAt(_player.transform);
            Attack();
        }
        else
        {
            _animator.SetBool("canAttack", false);
            _agent.SetDestination(_player.transform.position);
        }
    }

    void Attack()
    {
       Collider[] playerColliders = Physics.OverlapSphere(attackPos.position, 1, 1 << 8);

        if(playerColliders.Length > 0)
        {
            _animator.SetBool("canAttack", true);
        }
    }

    public void OnAttackEnd()
    {
        Debug.Log("attack ended");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(attackPos.position, 1);
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
