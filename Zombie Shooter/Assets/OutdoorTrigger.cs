using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorTrigger : MonoBehaviour
{
    public Weather weather;

    private void OnTriggerEnter(Collider other)
    {
        if (!weather.isOutdoor) return;
        weather.isOutdoor = false;

        weather.SetOutdoorAmbientSound();
    }
}
