using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControlerQuestionary : MonoBehaviour
{
    [Header("Caminho das perguntas")]
    [Tooltip("nomeArquivo deve estar dentro de Resource, Sem o .json")]
    [SerializeField] private String nomeArquivoJSON;

    [Header("Placas")]
    [SerializeField] private PlacaPergunta placaPergunta;
    [SerializeField] private PlacaResposta[] placasResposta;
    [Header("Config")]
    private Question openQuestion;
    private int SelectIndex = 0;
    
    //Inputs
    private byte direction = 0;
    private bool selectAnswer = false;

    void Awake(){
        //Necessita ter um action Map
        InputManager.inputManager.TradeActionMap(ACTION_MAP.MINI_GAME);
        CarregarJSON(nomeArquivoJSON);
        openQuestion = sortearQuestion();
    }

    void Start() {
        placasResposta[SelectIndex].AscenderTodasAsLuzes();
        escreverAsQuestoes();
        placaPergunta.AscenderTodasAsLuzes();
    }

    void Update() {

        //Se for alterar o InputManager, deve alterar aqui
        direction = InputManager.inputManager.GetMoveDirection();
        selectAnswer = InputManager.inputManager.GetClickToSelectAnswer();

        if (selectAnswer) {
            Debug.Log($"Selecionado {SelectIndex}");
        }
        if(direction != 0) {
            navigationQuestion();
        }
    }

    private void CarregarJSON(String nomeArquivoJSON) {
        if(ListQuestions.listQuestion != null) return;

        TextAsset arquivo = Resources.Load<TextAsset>(nomeArquivoJSON);
        if(arquivo != null) {
            QuestionWrapper dados = JsonUtility.FromJson<QuestionWrapper>(arquivo.text);
            ListQuestions.listQuestion = dados.perguntas;
        }
        else {
            Debug.LogError($"Arquivo {nomeArquivoJSON} não foi encontrado");
        }
    }
    private Question sortearQuestion() {
        return ListQuestions.listQuestion[UnityEngine.Random.Range(0,ListQuestions.listQuestion.Count)];
    }
    private void escreverAsQuestoes() {
        placaPergunta.Sobrescrever(openQuestion.enunciado);
        List<int> positionsFree = new List<int>() {0,1,2,3};
        for(int i = 0; i < openQuestion.alternativas.Length; i++) {
            int randomListIndex = UnityEngine.Random.Range(0, positionsFree.Count);
            int indexToWrite = positionsFree[randomListIndex];
            positionsFree.RemoveAt(randomListIndex);
            placasResposta[indexToWrite].Sobrescrever(openQuestion.alternativas[i]);

            if(i == 0) placasResposta[indexToWrite].SetIsCorrect(true);
        }
    }

    private void navigationQuestion() { // Não esta flexivel para qualquer grupo de questoes
        if(SelectIndex < 0 || SelectIndex > 4 || direction == 0) return;
        placasResposta[SelectIndex].ApagarTodasAsLuzes();

        int i = SelectIndex; //Auxiliar para trocar o SelectIndex
        switch (direction) {
            case 1:
                if(i == 0 || i == 1) i += 2;
                else i -= 2;
                break;
            case 2:
                if(i == 1 || i == 3) i -= 1;
                else i += 1;
                break;
            case 3:
                if(i == 2 || i == 3) i -= 2;
                else i += 2;
                break;
            case 4:
                if(i == 0 || i == 2) i += 1;
                else i -= 1;
                break;
        }

        SelectIndex = i;
        placasResposta[SelectIndex].AscenderTodasAsLuzes();
    }
}