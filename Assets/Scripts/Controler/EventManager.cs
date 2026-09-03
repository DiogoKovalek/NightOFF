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
    [SerializeField] private GameObject solarPanel;
    private Device[] listDevices;
    private PowerCableDevice[] listPowerCable;
    private SolarPanel[] listSolarPanel;
    void Awake() {
        if(device != null) listDevices = device?.GetComponentsInChildren<Device>();
        if(powerCables != null) listPowerCable = powerCables?.GetComponentsInChildren<PowerCableDevice>();
        if(solarPanel != null) listSolarPanel = solarPanel?.GetComponentsInChildren<SolarPanel>();
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
        controler.levelCompletedUI += uiControler.OnLevelCompleteUI;

        uiControler.pausedGame += controler.OnPauseGame;
        uiControler.continuedGame += controler.OnContinueGame;
        uiControler.restartedGame += controler.OnRestartGame;
        uiControler.exitedGame += controler.OnExitGame;
        uiControler.nextedLevel += controler.OnNextLevel;

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
        if(listDevices != null) {
            foreach (var sol in listSolarPanel) {
               controler.deviceSwitched += sol.OnDeviceSwitch; 
            }
        }
    }
}
