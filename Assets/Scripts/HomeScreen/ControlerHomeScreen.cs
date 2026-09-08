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

    void Start() {
        InputManager.inputManager.TradeActionMap(ACTION_MAP.MENU);
        sliderBackground?.StartSlide();
        if (panelPressEnter != null) StartCoroutine(shiningPressEnter());
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
