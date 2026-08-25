using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerCableDevice : MonoBehaviour {
    [Header("Positions")]
    [SerializeField] private PowerCableBody body;
    private Transform startP;
    private Transform endP;
    private Transform curveP;
    
    [Header("Icon Energy")]
    [SerializeField] private GameObject prefIconEnergy;
    [SerializeField] private Transform listOfIcons;
    [SerializeField] private float delayForSpawIcon = 1f;
    private float td = 0;
    private List<IconMovePowerCable> listIcons = new List<IconMovePowerCable>();
    private bool isOn = false;

    void Awake() {
        body.GetParans(out startP, out endP, out curveP);
    }
    void Update() {
        if (isOn) {
            td += Time.deltaTime;
            if(td > delayForSpawIcon) {
                td = 0;
                IconMovePowerCable icon = null;
                foreach(var i in listIcons) {
                    if(!i.IsActive()){
                        icon = i;
                        continue;
                    }
                }
                if(icon == null){
                    GameObject obj = Instantiate(prefIconEnergy, startP.localPosition, prefIconEnergy.transform.rotation);
                    obj.transform.SetParent(listOfIcons);
                    icon = obj.GetComponent<IconMovePowerCable>();
                    listIcons.Add(icon);
                }
                icon.StartMovement(startP.localPosition, endP.localPosition, curveP.localPosition);
            }
        }
    }
    public void OnDeviceSwitch(bool isDay) {
        if(isDay) OnEnablePowerCable();
        else OnDisablePowerCable();
    }
    private void OnEnablePowerCable() {
        isOn = true;
    }
    private void OnDisablePowerCable() {
        isOn = false;
    }

}