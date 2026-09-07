using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Placas : MonoBehaviour
{
    protected LuzPlaca[] listLuzes;
    protected TextMeshPro textPlaca;
    void Awake() {
        listLuzes = GetComponentsInChildren<LuzPlaca>();
        textPlaca = GetComponentInChildren<TextMeshPro>();
    }
    public void Sobrescrever(String str) {
        textPlaca.text = str;
    }

    #region Luz controler
    public void AscenderTodasAsLuzes(LuzPlaca[] luzes) {
        foreach(var luz in luzes) {
            luz.EnableStaticColor(true);
        }
    }
    #endregion
}
