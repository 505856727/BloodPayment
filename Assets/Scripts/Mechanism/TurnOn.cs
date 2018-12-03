using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOn : MonoBehaviour
{
    public GameObject targetToTurnOn;
    public bool isOn = true;
    public GameObject OnEnableObject;
    public GameObject OffEnableObject;

    public void turnOn()
    {
        isOn = !isOn;
        targetToTurnOn.SetActive(isOn);

        if(OnEnableObject && OffEnableObject)
        {
            OnEnableObject.SetActive(isOn);
            OffEnableObject.SetActive(!isOn);
        }
    }
}
