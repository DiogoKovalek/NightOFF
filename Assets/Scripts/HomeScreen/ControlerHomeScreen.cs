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

    [Header("Background")]
    [SerializeField] private bool initDay = true;
    private bool isDay = true;
    [SerializeField] private GameObject Background;
    private const float timeSecondTradeDayNight = 0.15f;
    private const float timeToSwitchTurn = 12f;

    #region EVENTS
    public delegate void DeviceSwitched(bool isDay);
    public event DeviceSwitched deviceSwitched;
    #endregion
    void Awake() {
        isDay = initDay;
        conectEvents();
    }
    private void conectEvents() {
        if (devices != null) listDevices = devices?.GetComponentsInChildren<Device>();
        if (powerCables != null) listPowerCable = powerCables?.GetComponentsInChildren<PowerCableDevice>();
        if (solarPanels != null) listSolarPanel = solarPanels?.GetComponentsInChildren<SolarPanel>();

        if (listDevices != null) {
            foreach (var dev in listDevices) {
                deviceSwitched += dev.OnDeviceSwitch;
            }
        }
        if (listPowerCable != null) {
            foreach (var pow in listPowerCable) {
                deviceSwitched += pow.OnDeviceSwitch;
            }
        }
        if (listDevices != null) {
            foreach (var sol in listSolarPanel) {
                deviceSwitched += sol.OnDeviceSwitch;
            }
        }
    }

    void Start() {
        InputManager.inputManager.TradeActionMap(ACTION_MAP.MENU);
        sliderBackground?.StartSlide();
        if (panelPressEnter != null) StartCoroutine(shiningPressEnter());

        deviceSwitched?.Invoke(isDay);
        StartCoroutine(tradeDayNight());
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

    private IEnumerator tradeDayNight() {
        while (true) {
            yield return new WaitForSeconds(timeToSwitchTurn);

            Quaternion startRotation = Background.transform.rotation;
            Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, -180);
            float countTime = 0;

            while (countTime <= timeSecondTradeDayNight) {
                countTime += Time.deltaTime;
                float rot = countTime / timeSecondTradeDayNight;

                Background.transform.rotation = Quaternion.Lerp(startRotation, endRotation, rot);
                yield return null;
            }
            Background.transform.rotation = endRotation;
            isDay = !isDay;
            deviceSwitched?.Invoke(isDay);
        }
    }
}
