using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSwitch : MonoBehaviour
{
    private LightController myController;
    private Light2D myLight;
    private bool isOn;

    private float valueChange;
    private float baseIntensity;

    private float intensity;

    private void Start()
    {
        myController = GetComponentInParent<LightController>();
        myLight = GetComponent<Light2D>();

        intensity = myLight.intensity;
        baseIntensity = intensity;

        if (myController.myRoomOccupied)
        {
            myLight.intensity = intensity;
        }
        else
        {
            myLight.intensity = 0f;
        }
    }

    public void RoomShiftFunc(bool entering, float shifts)
    {
        valueChange = intensity/shifts;
        if (entering)
        {
            myLight.intensity -= valueChange;
        }
        else
        {
            myLight.intensity += valueChange;
        }
    }

    public void SetNewIntensity(float newIntensity)
    {
        if (!isOn)
        {
            myLight.intensity = newIntensity;
            isOn = true;
        }
        else
        {
            myLight.intensity = baseIntensity;
            isOn = false;
        }
    }
}
