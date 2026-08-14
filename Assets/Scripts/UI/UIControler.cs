using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControler : MonoBehaviour {
    //[Header("Buttons")]
    [Header("ShiftCounter")]
    [SerializeField] private GameObject shiftCounter;
    [SerializeField] private GameObject prefCounter;
    [SerializeField] private Counter[] listCounters;
    private float startDistCounter = 50;
    private float spaceDistCounters = 70;
    private float startLenghtRightShifitCounter = 535f;
    private float strentchShiftCounter = 70f;
    [Header("PauseMenu")]
    [SerializeField] private GameObject pauseMenu;

    void Awake() {
        createCounterByShifts(3);
    }
    private void createCounterByShifts(int numShifts) {
        RectTransform rect = shiftCounter.GetComponent<RectTransform>();
        float newValueRight = startLenghtRightShifitCounter - (strentchShiftCounter * (numShifts - 1));
        rect.offsetMax = new Vector2(-newValueRight, rect.offsetMax.y);

        for (int i = 0; i < numShifts; i++) {
            Instantiate(prefCounter, shiftCounter.transform);
        }
    }
    public void OnPauseGame() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Pause");
    }
    public void OnContinueGame() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Continue");
    }
    public void OnRestartGame() {
        Debug.Log("Restart");
        ManagerScenes.RestartLevel();
    }
    public void OnExitGame() {
        Debug.Log("Exit");
        ManagerScenes.ExitToHomeScreen();
    }
}
