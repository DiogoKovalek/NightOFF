using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControler : MonoBehaviour {
    [Header("Panels")]
    [SerializeField] private GameObject GameMenu;
    [SerializeField] private GameObject LevelCompleteMenu;
    [Header("Stars")]
    [SerializeField] private UIStar[] listStars;
    [Header("ShiftCounter")]
    [SerializeField] private GameObject shiftCounter;
    [SerializeField] private GameObject prefCounter;
    private Counter[] listCounters;
    private byte numShiftsForTrade = 0;
    private byte numShifts = 0;
    private float startLenghtRightShifitCounter = 535f;
    private float strentchShiftCounter = 70f;
    [Header("PauseMenu")]
    [SerializeField] private GameObject pauseMenu;
    [Header("Level Complete")]
    [SerializeField] GameObject BlackBG;
    private void createCounterByShifts(byte numShifts) {
        int numCounters = numShifts - 1; 
        // caso queira exatamente o numero de shifts, substitua numCountes por numShifts
        //Com exexao no numShiftsForTrade = numShifts
        RectTransform rect = shiftCounter.GetComponent<RectTransform>();
        float newValueRight = startLenghtRightShifitCounter - (strentchShiftCounter * (numCounters - 1));
        rect.offsetMax = new Vector2(-newValueRight, rect.offsetMax.y);

        listCounters = new Counter[numCounters];
        for (int i = 0; i < numCounters; i++) {
            GameObject obj = Instantiate(prefCounter, shiftCounter.transform);
            listCounters[i] = obj.GetComponent<Counter>();
        }
        numShiftsForTrade = numShifts;
    }
    public void OnPauseGame() {
        pauseMenu.SetActive(true);
        InputManager.inputManager.TradeActionMap(ACTION_MAP.PAUSE);
        Time.timeScale = 0f;
        Debug.Log("Pause");
    }
    public void OnContinueGame() {
        pauseMenu.SetActive(false);
        InputManager.inputManager.TradeActionMap(ACTION_MAP.PLAYER);
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

    #region EVENTS
    public void OnCompleteStarInUI(byte index) {
        listStars[index-1].EnableStar();
    }
    public void OnStartCounterShifts(byte numShifts) {
        createCounterByShifts(numShifts);
    }
    public void OnIncrementOneShift() {
        numShifts++;
        if(numShifts < numShiftsForTrade) {
            listCounters[numShifts - 1].EnableCounter(); 
        }
        else {
            foreach(var o in listCounters) o.DisableCounter();
            numShifts = 0;
        }
    }
    public void OnLevelCompleteUI() {
        
    }
    #endregion
}
