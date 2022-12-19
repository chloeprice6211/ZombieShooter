using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] _rigidbodies;
    Animator _animator;

    private void Start()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();
        
        DeactivateRagdoll();
    }

    public void ActiveRagdoll()
    {
        _animator.enabled = false;

        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = false;
        }
    }
    void DeactivateRagdoll()
    {
        _animator.enabled = true;

        foreach(Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = true;
        }
    }
}
