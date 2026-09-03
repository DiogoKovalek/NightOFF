using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
    [SerializeField] private float starGrowScale = 1.4f;
    [SerializeField] private float starTimeToGrow = 0.5f;
    [SerializeField] private float starTimeToDecrease = 0.5f;
    [SerializeField] private GameObject TextClickToContinue;
    [SerializeField] private float textDelayShining = 0.8f;

    [Header("BlackScreen")]
    [SerializeField] private RectTransform BlackScreen;
    [SerializeField] private float timeToSlide = 0.7f;
    private bool isBlackScreenInCenter = true;
    private float widthCanvas;

    #region EVENTS
    public delegate void PausedGame();
    public event PausedGame pausedGame;
    public delegate void ContinuedGame();
    public event ContinuedGame continuedGame;
    public delegate void RestartedGame();
    public event RestartedGame restartedGame;
    public delegate void ExitedGame();
    public event ExitedGame exitedGame;
    public delegate void NextedLevel();
    public event NextedLevel nextedLevel;
    #endregion

    void Awake() {
        GameMenu.SetActive(true);
        LevelCompleteMenu.SetActive(false);

        // BlackScreen Começa no centro da tela
        BlackScreen.gameObject.SetActive(true);
        BlackScreen.anchoredPosition = Vector3.zero;
        isBlackScreenInCenter = true;

        // Pegar tamanho do Canvas
        CanvasScaler can = GetComponent<CanvasScaler>();
        if(can != null) widthCanvas = can.referenceResolution.x;
        else widthCanvas = 1920; // Padrao
    }

    void Start() {
        StartCoroutine(slideBlackScreen());
    }
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
        pausedGame?.Invoke();
    }
    public void OnContinueGame() {
        pauseMenu.SetActive(false);
        continuedGame?.Invoke();
    }
    public void OnRestartGame() {
        StartCoroutine(slideBlackScreen(() => restartedGame?.Invoke()));
        //restartedGame?.Invoke();
    }
    public void OnExitGame() {
        StartCoroutine(slideBlackScreen(() => exitedGame?.Invoke()));
        //exitedGame?.Invoke();
    }

    public IEnumerator slideBlackScreen(System.Action actionNext = null) {
        float t = 0;
        float startX = BlackScreen.anchoredPosition.x;
        float endX = 0;
        Vector3 position = Vector3.zero;

        if (isBlackScreenInCenter) {// Centro para esquerda
            startX = 0;
            endX = -widthCanvas;
            position.x = startX;
            BlackScreen.anchoredPosition = position;
        }
        else {// Esquerda para Centro
            startX = widthCanvas;
            endX = 0;
            position.x = startX;
            BlackScreen.anchoredPosition = position;
        }
        while (t < timeToSlide) {
            t += Time.deltaTime;
            position.x = Mathf.Lerp(startX, endX, t/timeToSlide);
            BlackScreen.anchoredPosition = position;
            yield return null;
        }
        position.x = endX;
        BlackScreen.anchoredPosition = position;

        isBlackScreenInCenter = !isBlackScreenInCenter;
        
        actionNext?.Invoke();
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
    public void OnLevelCompleteUI(byte numStars) {
        StartCoroutine(levelCompleteTrasition(numStars));
    }
    #endregion

    #region Level Complete
    private IEnumerator levelCompleteTrasition(byte numStars) {
        //Desativa tudo
        BlackBG.gameObject.SetActive(false);
        TextClickToContinue.SetActive(false);
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
        float countStars = 0;
        foreach(RectTransform star in Stars) {
            star.gameObject.SetActive(true);
            StartCoroutine(animationGrowDecrease(star, star.localScale * starGrowScale, starTimeToGrow, starTimeToDecrease));
            if(numStars > countStars) {
                countStars++;
                star.gameObject?.GetComponent<UIStar>().EnableStar();
            }
            yield return new WaitForSeconds(starsDelayToShow);
        }

        //Mostra Texto
        StartCoroutine(shiningObject(TextClickToContinue, textDelayShining));

        bool clickToContinue = false;
        while (!clickToContinue) {
            clickToContinue = InputManager.inputManager.GetCliclToContinue();
            yield return null;
        }

        //Inicia a animacao da barra escura

        //Troca nivel
        nextedLevel?.Invoke();
    }
    private IEnumerator animationGrowDecrease(RectTransform rect, Vector3 maxScale, float timerToGrow, float timerToDecrease, bool repeat = false) {
        //=====================================
        //=== Efeito de crescer e diminuir ====
        //=====================================

        Vector3 initialLocalScale = rect.localScale;
        Vector3 scale = initialLocalScale;

        //Cresce
        float t = 0;
        while (t < timerToGrow) {
            t += Time.deltaTime;
            scale = Vector3.Lerp(initialLocalScale, maxScale, t/timerToGrow);
            rect.localScale = scale;
            yield return null;
        }

        //Diminui
        t = 0; 
        while(t < timerToDecrease) {
            t += Time.deltaTime;
            scale = Vector3.Lerp(maxScale, initialLocalScale, t /timerToDecrease);
            rect.localScale = scale;
            yield return null;
        }
        scale = Vector3.Lerp(maxScale, initialLocalScale, t /timerToDecrease);
        rect.localScale = scale;
        yield return null;

        if(repeat) StartCoroutine(animationGrowDecrease(rect, maxScale, timerToGrow, timerToDecrease, repeat));
    }

    private IEnumerator shiningObject(GameObject obj, float timeDelay, bool repeat = true) {
        obj.SetActive(!obj.activeSelf);
        yield return new WaitForSeconds(timeDelay);
        if(repeat) StartCoroutine(shiningObject(obj, timeDelay, repeat));
    }
    #endregion
}
