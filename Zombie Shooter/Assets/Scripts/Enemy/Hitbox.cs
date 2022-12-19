using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour, IDamagable
{
    public Enemy enemy;
    private float _multiplier;
    public HitboxType type;

    public void TakeDamage(float damage)
    {
        switch (type)
        {
            case HitboxType.Head:
                _multiplier = 2;
                break;
            case HitboxType.Body:
                _multiplier = 1;
                break;
            case HitboxType.Hand:
                _multiplier = .35f;
                break;
            case HitboxType.Leg:
                _multiplier = .5f;
                break;
        }
        enemy.TakeDamage(damage * _multiplier);
    }

    
}
public enum HitboxType
{
    Head,
    Body,
    Hand,
    Leg
}
