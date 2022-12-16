using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletScriptable", menuName = "ScriptableObjects/Bullet")]
public class BulletScriptable : ScriptableObject
{
    public float damage;
    public BulletType bulletType;
}
public enum BulletType
{
    Light,
    Medium,
    Heavy
}
