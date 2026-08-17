using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private Controler controler;
    [SerializeField] private Player player;
    [SerializeField] private UIControler uiControler;
    [SerializeField] private GameObject device;
    private Device[] listDevices;

    void Awake() {
        listDevices = device?.GetComponentsInChildren<Device>();
        initEvents();
    }

    private void initEvents() {
        player.collectedStar += controler.OnCollectStar;
        player.interactedAnything += controler.OnInteractAnything;
        player.playerMoved += controler.OnPlayerMove;
        player.thisIslastedMovement += controler.OnThisIsLastMovement;

        controler.compleatedStartInUI += uiControler.OnCompleteStarInUI;
        controler.startedCounterShifts += uiControler.OnStartCounterShifts;
        controler.incrementedOneShift += uiControler.OnIncrementOneShift;

        foreach(var dev in listDevices) controler.deviceSwitched += dev.OnDeviceSwitch;
    }
}
