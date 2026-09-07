using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzPlaca : MonoBehaviour{
    [Header("Sprite Render")]
    [SerializeField] private SpriteRenderer sprRen;

    [Header("Cores")]
    [SerializeField] private Sprite sprCinza;
    [SerializeField] private Sprite sprAmarelo;
    [SerializeField] private Sprite sprVermelho;
    [SerializeField] private Sprite sprVerde;

    [Header("Conf")]
    [SerializeField] private COR_LUZ corLuz;
    private Sprite sprOfCorLuz;

    void Awake() {
        if(sprRen == null) {
            sprRen = GetComponent<SpriteRenderer>();
        }
        SwitchColor(corLuz);
    } 

    private void OnLuz() {
        sprRen.sprite = sprOfCorLuz;
    }
    private void OffLuz() {
        sprRen.sprite = sprCinza;
    }
    public void EnableStaticColor(bool isOn) {
        if (isOn) OnLuz();
        else OffLuz();
    }

    public void SwitchColor(COR_LUZ novaCor) {
        corLuz = novaCor;
        switch (novaCor) {
            case COR_LUZ.AMARELO:
                sprOfCorLuz = sprAmarelo;
                break;
            case COR_LUZ.VERMELHO:
                sprOfCorLuz = sprVermelho;
                break;
            case COR_LUZ.VERDE:
                sprOfCorLuz = sprVerde;
                break;
        }
    }
}

public enum COR_LUZ {
    AMARELO,
    VERMELHO,
    VERDE
}
