using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanel : MonoBehaviour
{
    [SerializeField] private IconUp iconUp;
    public void OnDeviceSwitch(bool isDay) {
        if (isDay) {
            iconUp.StartMovement(true);
        }
    }
}
