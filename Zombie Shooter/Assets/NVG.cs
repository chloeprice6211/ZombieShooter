using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NVG : MonoBehaviour
{
    public Transform lens;
    Animation _animation;

    [SerializeField] AnimationClip activateNVGClip;
    [SerializeField] AnimationClip deactivateNVGClip;

    public bool isActive;


    private void Awake()
    {
        _animation = lens.GetComponent<Animation>();
    }

    public void ActivateOrDeactivate()
    {
        isActive = !isActive;

        if (isActive)
        {
            _animation.Play(deactivateNVGClip.name);
        }
        else
        {
            _animation.Play(activateNVGClip.name);
        }
        
    }
}
