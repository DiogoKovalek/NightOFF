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

    void Awake(){
        //Necessita ter um action Map
        InputManager.inputManager.TradeActionMap(ACTION_MAP.MINI_GAME);
        CarregarJSON(nomeArquivoJSON);
        openQuestion = sortearQuestion();
        escreverAsQuestoes();
    }

    void Start() {

        
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

    
}