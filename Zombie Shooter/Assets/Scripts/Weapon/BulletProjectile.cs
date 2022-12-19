using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    Rigidbody rb;

    public float speed;
    public float damage;
    public float explosionForce;

    IDamagable _impactedObject;

    [SerializeField] List<Transform> impactParticle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.layer)
        {
            case 0:
                Instantiate(impactParticle[0], transform.position, Quaternion.identity);
                break;
            case 10:
                Instantiate(impactParticle[1], transform.position, Quaternion.identity);
                break;
        }

        if(collision.gameObject.TryGetComponent(out _impactedObject))
        {
            _impactedObject.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
