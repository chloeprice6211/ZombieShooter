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
        rb.AddForce(Vector3.forward, ForceMode.Impulse);
    }
    private void Update()
    {
        
    }
}
