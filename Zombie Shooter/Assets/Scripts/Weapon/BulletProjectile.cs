using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    Rigidbody rb;

    public float speed;
    public float damage;
    public float explosionForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.AddExplosionForce(explosionForce, transform.position, 1);
        Destroy(gameObject);
    }
}
