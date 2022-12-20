using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalFlashlight : HelmetDevice
{
    [SerializeField] Light lightSource;

    AudioSource audioSource;
    [SerializeField] AudioClip toggleAudio;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void ActivateOrDeactivateDevice()
    {
        if (!isOn && batteryCharge <= minimalCharge) return;

        isOn = !isOn;

        if (isOn)
        {
            audioSource.PlayOneShot(toggleAudio);
            lightSource.enabled = true;
            StartCoroutine(ChargeDecayRoutine(this));
        }
        else
        {
            audioSource.PlayOneShot(toggleAudio);
            lightSource.enabled = false;
            StartCoroutine(ChargeGainRoutine());
        } 
    }
}
