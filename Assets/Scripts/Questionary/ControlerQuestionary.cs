using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlerQuestionary : MonoBehaviour
{
    [Header("Caminho das perguntas")]
    [Tooltip("nomeArquivo deve estar dentro de Resource, Sem o .json")]
    [SerializeField] private String nomeArquivoJSON;

    [Header("Placas")]
    [SerializeField] private GameObject PlacaPergunta;
    [SerializeField] private GameObject[] PlacaRespostas;
    //Lista de luz
    private LuzPlaca[] listLuzesPlacaPergunta;
    private LuzPlaca[][] listLuzesPlaca;
    //Lista de textos
    private TextMeshPro perguntaText;
    private TextMeshPro[] respostasText;

    void Awake(){
        //Placa Pergunta
        if(PlacaPergunta != null){ 
            listLuzesPlacaPergunta = PlacaPergunta.GetComponentsInChildren<LuzPlaca>();
            perguntaText = PlacaPergunta.GetComponentInChildren<TextMeshPro>();
        }

        //Placa Resposta
        listLuzesPlaca = new LuzPlaca[PlacaRespostas.Length][];
        respostasText = new TextMeshPro[PlacaRespostas.Length];
        for(int i = 0; i < PlacaRespostas.Length; i++) {
            if(PlacaRespostas[i] == null) continue;
            listLuzesPlaca[i] = PlacaRespostas[i].GetComponentsInChildren<LuzPlaca>();
            respostasText[i] = PlacaRespostas[i].GetComponentInChildren<TextMeshPro>();

        }
        CarregarJSON(nomeArquivoJSON);
    }

    void Start() {

        //Teste de luz =========================================================
        foreach(var luz in listLuzesPlaca[1])luz.SwitchColor(COR_LUZ.AMARELO);
        foreach(var luz in listLuzesPlaca[2])luz.SwitchColor(COR_LUZ.VERDE);
        foreach(var luz in listLuzesPlaca[3])luz.SwitchColor(COR_LUZ.VERMELHO);
        AscenderTodasAsLuzes(listLuzesPlacaPergunta);
        foreach(var listLuz in listLuzesPlaca)AscenderTodasAsLuzes(listLuz);
        //======================================================================

        //Teste de Text ========================================================
        perguntaText.text = "Pergunta";
        respostasText[0].text = "Resposta 1";
        respostasText[1].text = "Resposta 2";
        respostasText[2].text = "Resposta 3";
        respostasText[3].text = "Resposta 4";
        //======================================================================

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

    #region Luz controler
    private void AscenderTodasAsLuzes(LuzPlaca[] luzes) {
        foreach(var luz in luzes) {
            luz.EnableStaticColor(true);
        }
    }
    #endregion
}