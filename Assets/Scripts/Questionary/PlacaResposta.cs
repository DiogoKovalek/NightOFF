using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaResposta : Placas
{
    private bool isCorrect = false;

    public bool GetIsCorrect() {
        return isCorrect;
    }
    public void SetIsCorrect(bool isCorrect) {
        this.isCorrect = isCorrect;
    }
}
