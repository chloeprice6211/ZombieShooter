using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public AudioSource ImpactAudioSource;

    public AudioClip bodyShotClip;
    public AudioClip headShotClip;
    public AudioClip killSound;

    public List<Weapon> Weapons;

    private void Awake()
    {
        _instance = this;
    }
}
