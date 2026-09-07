using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerQuestionary : MonoBehaviour
{
    [Header("Caminho das perguntas")]
    [Tooltip("nomeArquivo deve estar dentro de Resource, Sem o .json")]
    [SerializeField] private String nomeArquivoJSON;

    [Header("Placas")]
    [SerializeField] private GameObject PlacaPergunta;
    [SerializeField] private GameObject[] PlacaRespostas;
    private LuzPlaca[] listLuzesPlacaPergunta;
    private LuzPlaca[][] listLuzesPlaca;

    void Awake(){
        if(PlacaPergunta != null) listLuzesPlacaPergunta = PlacaPergunta.GetComponentsInChildren<LuzPlaca>();

        listLuzesPlaca = new LuzPlaca[PlacaRespostas.Length][];
        for(int i = 0; i < PlacaRespostas.Length; i++) {
            if(PlacaRespostas[i] == null) continue;
            listLuzesPlaca[i] = PlacaRespostas[i].GetComponentsInChildren<LuzPlaca>();
        }
        CarregarJSON(nomeArquivoJSON);
    }

    void Start() {
        foreach(var luz in listLuzesPlaca[1])luz.SwitchColor(COR_LUZ.AMARELO);
        foreach(var luz in listLuzesPlaca[2])luz.SwitchColor(COR_LUZ.VERDE);
        foreach(var luz in listLuzesPlaca[3])luz.SwitchColor(COR_LUZ.VERMELHO);
        AscenderTodasAsLuzes(listLuzesPlacaPergunta);
        foreach(var listLuz in listLuzesPlaca)AscenderTodasAsLuzes(listLuz);
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