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
    [SerializeField] private RectTransform BlackBG;
    [SerializeField] private float blackBGStartY = -1095;
    [SerializeField] private float blackBGEndY = 0;
    [SerializeField] private float blackBGTimeTransitionUp = 0.4f;
    [SerializeField] private RectTransform[] Stars;
    [SerializeField] private float starsDelayToShow = 1;
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
        StartCoroutine(levelCompleteTrasition());
    }
    #endregion

    #region Level Complete
    private IEnumerator levelCompleteTrasition() {
        //Desativa tudo
        BlackBG.gameObject.SetActive(false);
        foreach(RectTransform star in Stars) {
            star.gameObject.SetActive(false);
        }

        //Seta posicao do BlackBG
        BlackBG.anchoredPosition = Vector2.zero;

        //Ativa  o menu de level complete
        GameMenu.SetActive(false);
        LevelCompleteMenu.SetActive(true);

        //Transicao do plano de fundo preto
        BlackBG.gameObject.SetActive(true);
        float t = 0;
        Vector3 blackBGPosition = new Vector3(BlackBG.anchoredPosition.x, blackBGStartY, BlackBG.anchoredPosition.y); 
        while (t < blackBGTimeTransitionUp) {
            t += Time.deltaTime;
            blackBGPosition.y = Mathf.Lerp(blackBGStartY, blackBGEndY, t/blackBGTimeTransitionUp);
            BlackBG.anchoredPosition = blackBGPosition;
            yield return null;
        }
        blackBGPosition.y = blackBGEndY;
        BlackBG.anchoredPosition = blackBGPosition;
        yield return null;

        //Estrelas

        foreach(RectTransform star in Stars) {
            star.gameObject.SetActive(true);
            yield return new WaitForSeconds(starsDelayToShow);
        }
    }
    private IEnumerator animationGrowDecrease(RectTransform rect, float timerToGrow, float timerToDecrease, bool repeat = false) {
        //=====================================
        //=== Efeito de crescer e diminuir ====
        //=====================================

        //Cresce
        float t = 0;

        //Diminui
        t = 0; 
        yield return null;
    }
    #endregion
}
