using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerHomeScreen : MonoBehaviour {
    [Header("Slider Background")]
    [SerializeField] private SliderBackground sliderBackground;

    [Header("Press Enter")]
    [SerializeField] private GameObject panelPressEnter;
    [SerializeField] private float delayShining;

    [Header("Settings")]
    private bool isFreeForStart = true;
    private bool isClickToStartGame = false;

    [Header("Devices")]
    [SerializeField] private GameObject devices;
    [SerializeField] private GameObject powerCables;
    [SerializeField] private GameObject solarPanels;
    private Device[] listDevices;
    private PowerCableDevice[] listPowerCable;
    private SolarPanel[] listSolarPanel;

    #region EVENTS
    public delegate void DeviceSwitched(bool isDay);
    public event DeviceSwitched deviceSwitched;
    #endregion
    void Awake() {
        conectEvents();
    }
    private void conectEvents() {
        if(devices != null) listDevices = devices?.GetComponentsInChildren<Device>();
        if(powerCables != null) listPowerCable = powerCables?.GetComponentsInChildren<PowerCableDevice>();
        if(solarPanels != null) listSolarPanel = solarPanels?.GetComponentsInChildren<SolarPanel>();

        if(listDevices != null){
            foreach(var dev in listDevices){
                deviceSwitched += dev.OnDeviceSwitch;
            }
        }
        if(listPowerCable != null){
            foreach(var pow in listPowerCable) {
                deviceSwitched += pow.OnDeviceSwitch;
            }
        }
        if(listDevices != null) {
            foreach (var sol in listSolarPanel) {
               deviceSwitched += sol.OnDeviceSwitch; 
            }
        }
    }

    void Start() {
        InputManager.inputManager.TradeActionMap(ACTION_MAP.MENU);
        sliderBackground?.StartSlide();
        if (panelPressEnter != null) StartCoroutine(shiningPressEnter());

        deviceSwitched?.Invoke(true);
    }

    void Update() {
        if (isFreeForStart) {
            isClickToStartGame = InputManager.inputManager.GetClickToStartGame();
            if (isClickToStartGame) {
                sliderBackground.StartSlide(() => ManagerScenes.StartGame());
            }
        }
    }

    private IEnumerator shiningPressEnter() {
        while (true) {
            panelPressEnter.SetActive(!panelPressEnter.activeSelf);
            yield return new WaitForSeconds(delayShining);
        }
    }
}
