using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    [SerializeField] AudioSource outdoorAudioSource;
    [SerializeField] AudioSource indoorAudioSource;

    public float minVolumeCap = .2f;
    public float maxVolumeCap = .7f;

    public bool isOutdoor;


    public void SetOutdoorAmbientSound()
    {
        StartCoroutine(LerpVolumeRoutine(outdoorAudioSource, indoorAudioSource));
    }
    public void SetIndoorAmbientSound()
    {
        StartCoroutine(LerpVolumeRoutine(indoorAudioSource, outdoorAudioSource));
    }

    IEnumerator LerpVolumeRoutine(AudioSource toIncreaseSource, AudioSource toReduceSource)
    {
        while(toIncreaseSource.volume <= maxVolumeCap)
        {
            toIncreaseSource.volume += Time.deltaTime /2;
            toReduceSource.volume -= Time.deltaTime /2;

            yield return null;
        }
    }
}
