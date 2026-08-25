using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerCableDevice : MonoBehaviour {
    [Header("Positions")]
    
    [Header("Icon Energy")]
    [SerializeField] private GameObject prefIconEnergy;
    [SerializeField] private Transform listOfIcons;
    [SerializeField] private float speedIcons = 3;
    public void OnDeviceSwitch(bool isDay) {
        if(isDay) OnEnablePowerCable();
        else OnDisablePowerCable();
    }
    private void OnEnablePowerCable() {
        Debug.Log("On");
    }
    private void OnDisablePowerCable() {
        Debug.Log("Off");
    }
}