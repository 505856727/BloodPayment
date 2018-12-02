using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOn : MonoBehaviour
{
    public GameObject targetToTurnOn;
    public bool isOn = true;

    public void turnOn()
    {
        isOn = !isOn;
        targetToTurnOn.SetActive(isOn);
    }
}
