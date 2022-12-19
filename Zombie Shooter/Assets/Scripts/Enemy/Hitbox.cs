using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour, IDamagable
{
    public Enemy enemy;
    public float multiplier = 1f;

    public void TakeDamage(float damage)
    {
        enemy.TakeDamage(damage * multiplier);
    }
}
