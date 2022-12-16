using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public BulletScriptable bulletScriptable;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        rb.AddRelativeForce(Vector3.forward * 50, ForceMode.Impulse);
    }
    private void Update()
    {
        
    }
}
