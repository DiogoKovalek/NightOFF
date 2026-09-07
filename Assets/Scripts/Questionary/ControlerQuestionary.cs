using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerQuestionary : MonoBehaviour
{
    [Tooltip("nomeArquivo deve estar dentro de Resource, Sem o .json")]
    [SerializeField] private String nomeArquivoJSON;
    void Start(){
        CarregarJSON(nomeArquivoJSON);
    }

    private void CarregarJSON(String nomeArquivoJSON) {
        if(ListQuestions.listQuestion != null) return;

        Debug.Log("Aqui");
        TextAsset arquivo = Resources.Load<TextAsset>(nomeArquivoJSON);
        if(arquivo != null) {
            QuestionWrapper dados = JsonUtility.FromJson<QuestionWrapper>(arquivo.text);
            ListQuestions.listQuestion = dados.perguntas;
        }
        else {
            Debug.LogError($"Arquivo {nomeArquivoJSON} não foi encontrado");
        }
    }
}