using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOff_Area : MonoBehaviour
{
    public Light light;
    bool lightsOn = false;

    private void Start()
    {
        light = GetComponent<Light>();
    }


    public void toggleLights()
    {
        if (lightsOn) light.enabled = true;
        else light.enabled = false;
        lightsOn = !lightsOn;
    }
}
