using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControlerQuestionary : MonoBehaviour {
    [Header("Caminho das perguntas")]
    [Tooltip("nomeArquivo deve estar dentro de Resource, Sem o .json")]
    [SerializeField] private String nomeArquivoJSON;

    [Header("Placas")]
    [SerializeField] private PlacaPergunta placaPergunta;
    [SerializeField] private PlacaResposta[] placasResposta;

    [Header("Slider Bar")]
    //Para transformar em asset, essa parte pode ser removida e depois
    //removida toda parte que ela é chamada
    [SerializeField] private SliderBackground sliderBackground;

    [Header("Text Press Enter")]
    [SerializeField] private GameObject PanelTextPressEnter;
    [SerializeField] private float DelayForShining;

    [Header("Config")]
    [SerializeField] private float delaySuspense1;
    [SerializeField] private float delaySuspense2;
    [SerializeField] private float delayForAbleEnter;
    private Question openQuestion;
    private int SelectIndex = 0;

    //Inputs
    private byte direction = 0;
    private bool selectAnswer = false;
    private bool freeForNavigation = true;
    private bool isAbleEnter = false;

    
    [Header("SFX")]
    [SerializeField] private AudioClip drunsSFX;
    [SerializeField] private AudioClip aplausesSFX;
    [SerializeField] private AudioClip loseSFX;

    void Awake() {
        //Necessita ter um action Map
        InputManager.inputManager.TradeActionMap(ACTION_MAP.MINI_GAME);
        CarregarJSON(nomeArquivoJSON);
        openQuestion = sortearQuestion();
    }

    void Start() {
        placasResposta[SelectIndex].AscenderTodasAsLuzes();
        escreverAsQuestoes();
        placaPergunta.AscenderTodasAsLuzes();

        sliderBackground.StartSlide();
    }

    void Update() {

        //Se for alterar o InputManager, deve alterar aqui
        direction = InputManager.inputManager.GetMoveDirection();
        selectAnswer = InputManager.inputManager.GetClickToSelectAnswer();

        if (freeForNavigation) {
            if (selectAnswer) {
                freeForNavigation = false;
                StartCoroutine(checkIfCorrect());
            }
            if (direction != 0) {
                navigationQuestion();
            }
        }
        else if (isAbleEnter) {
            if (selectAnswer) {
                sliderBackground.StartSlide(() => ManagerScenes.NextLevel());
            }
        }
    }

    private void CarregarJSON(String nomeArquivoJSON) {
        if (ListQuestions.listQuestion != null) return;

        TextAsset arquivo = Resources.Load<TextAsset>(nomeArquivoJSON);
        if (arquivo != null) {
            QuestionWrapper dados = JsonUtility.FromJson<QuestionWrapper>(arquivo.text);
            ListQuestions.listQuestion = dados.perguntas;
        }
        else {
            Debug.LogError($"Arquivo {nomeArquivoJSON} não foi encontrado");
        }
    }
    private Question sortearQuestion() {
        Debug.Log(ListQuestions.listQuestion.Count);
        return ListQuestions.ChangeQuestion(UnityEngine.Random.Range(0, ListQuestions.listQuestion.Count));
    }
    private void escreverAsQuestoes() {
        placaPergunta.Sobrescrever(openQuestion.enunciado);
        List<int> positionsFree = new List<int>() { 0, 1, 2, 3 };
        for (int i = 0; i < openQuestion.alternativas.Length; i++) {
            int randomListIndex = UnityEngine.Random.Range(0, positionsFree.Count);
            int indexToWrite = positionsFree[randomListIndex];
            positionsFree.RemoveAt(randomListIndex);
            placasResposta[indexToWrite].Sobrescrever(openQuestion.alternativas[i]);

            if (i == 0) placasResposta[indexToWrite].SetIsCorrect(true);
        }
    }
    private IEnumerator checkIfCorrect() {
        //musica
        AudioManager.audioManager.pauseMusic();
        AudioManager.audioManager.playSFX(drunsSFX);

        bool isCorrect = false;
        //Ascender todas as luzes
        foreach (var placa in placasResposta) placa.AscenderTodasAsLuzes();
        yield return new WaitForSeconds(delaySuspense1);

        AudioManager.audioManager.stopSFX();
        for (int i = 0; i < placasResposta.Length; i++) {
            placasResposta[i].ApagarTodasAsLuzes();
            bool auxCorrect = placasResposta[i].GetIsCorrect();
            placasResposta[i].TrocarTodasCores(auxCorrect ? COR_LUZ.VERDE : COR_LUZ.VERMELHO);
            if (i == SelectIndex) isCorrect = auxCorrect;
        }
        yield return new WaitForSeconds(delaySuspense2);

        //SFX
        if(isCorrect) AudioManager.audioManager.playSFX(aplausesSFX);
        else AudioManager.audioManager.playSFX(loseSFX);

        foreach (var placa in placasResposta) placa.AscenderTodasAsLuzes();
        yield return new WaitForSeconds(delayForAbleEnter);

        StartCoroutine(shiningTextPressEnter());
        isAbleEnter = true;
    }

    private IEnumerator shiningTextPressEnter() {
        while (true) {
            PanelTextPressEnter.SetActive(!PanelTextPressEnter.activeSelf);
            yield return new WaitForSeconds(DelayForShining);
        }
    }

    private void navigationQuestion() { // Não esta flexivel para qualquer grupo de questoes
        if (SelectIndex < 0 || SelectIndex > 4 || direction == 0) return;
        placasResposta[SelectIndex].ApagarTodasAsLuzes();

        int i = SelectIndex; //Auxiliar para trocar o SelectIndex
        switch (direction) {
            case 1:
                if (i == 0 || i == 1) i += 2;
                else i -= 2;
                break;
            case 2:
                if (i == 1 || i == 3) i -= 1;
                else i += 1;
                break;
            case 3:
                if (i == 2 || i == 3) i -= 2;
                else i += 2;
                break;
            case 4:
                if (i == 0 || i == 2) i += 1;
                else i -= 1;
                break;
        }

        SelectIndex = i;
        placasResposta[SelectIndex].AscenderTodasAsLuzes();
    }
}