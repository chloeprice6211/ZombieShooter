using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NVG : HelmetDevice
{
    public Transform lens;
    Animation _animation;

    [SerializeField] AnimationClip activateNVGClip;
    [SerializeField] AnimationClip deactivateNVGClip;

    private void Awake()
    {
        _animation = lens.GetComponent<Animation>();
    }

    public void ActivateOrDeactivate()
    {
        if (isOn)
        {
            _animation.Play(deactivateNVGClip.name);
        }
        else
        {
            _animation.Play(activateNVGClip.name);
        }

        base.ActivateOrDeactivateDevice();
        
    }
}
