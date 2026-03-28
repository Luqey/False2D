using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSwitch : MonoBehaviour
{

    private Light2D myLight;
    [SerializeField] private bool isOn;

    [SerializeField] private float numberOfShifts;
    private float valueChange;

    private void Start()
    {
        myLight = GetComponent<Light2D>();
        valueChange = 1/numberOfShifts;

        if (isOn)
        {
            myLight.intensity = 1;
        }
        else
        {
            myLight.intensity = 0f;
        }
    }

    public IEnumerator LightChange(bool isPos)
    {
        for (int i = 0; i < numberOfShifts; i++)
        {
            if (isPos)
            {
                myLight.intensity += valueChange;
                yield return new WaitForSeconds(.15f);
            }
            else
            {
                myLight.intensity -= valueChange;
                yield return new WaitForSeconds(.15f);
            }
        }
        isOn = !isOn;
    }
}
