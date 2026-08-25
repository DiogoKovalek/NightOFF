using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private Controler controler;
    [SerializeField] private Player player;
    [SerializeField] private UIControler uiControler;
    [SerializeField] private GameObject device;
    [SerializeField] private GameObject powerCables;
    private Device[] listDevices;
    private PowerCableDevice[] listPowerCable;

    void Awake() {
        if(device != null) listDevices = device?.GetComponentsInChildren<Device>();
        if(powerCables != null) listPowerCable = powerCables?.GetComponentsInChildren<PowerCableDevice>();
        initEvents();
    }

    private void initEvents() {
        player.collectedStar += controler.OnCollectStar;
        player.interactedComputer += controler.OnInteractComputer;
        player.playerMoved += controler.OnPlayerMove;
        player.thisIslastedMovement += controler.OnThisIsLastMovement;

        controler.compleatedStartInUI += uiControler.OnCompleteStarInUI;
        controler.startedCounterShifts += uiControler.OnStartCounterShifts;
        controler.incrementedOneShift += uiControler.OnIncrementOneShift;

        if(listDevices != null){
            foreach(var dev in listDevices){
                dev.linkControler(controler);
                controler.deviceSwitched += dev.OnDeviceSwitch;
            }
        }
        if(listPowerCable != null){
            foreach(var pow in listPowerCable) {
                controler.deviceSwitched += pow.OnDeviceSwitch;
            }
        }
    }
}
