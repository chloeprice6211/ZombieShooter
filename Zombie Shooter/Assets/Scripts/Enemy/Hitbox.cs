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
        if (enemy.currentHealth < 1) return;

        switch (type)
        {
            case HitboxType.Head:
                _multiplier = 2;
                GameManager.Instance.ImpactAudioSource.PlayOneShot(GameManager.Instance.headShotClip);
                break;
            case HitboxType.Body:
                GameManager.Instance.ImpactAudioSource.PlayOneShot(GameManager.Instance.bodyShotClip);
                _multiplier = 1;
                break;
            case HitboxType.Hand:
                GameManager.Instance.ImpactAudioSource.PlayOneShot(GameManager.Instance.bodyShotClip);
                _multiplier = .35f;
                break;
            case HitboxType.Leg:
                GameManager.Instance.ImpactAudioSource.PlayOneShot(GameManager.Instance.bodyShotClip);
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
