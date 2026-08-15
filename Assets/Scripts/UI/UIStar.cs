using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStar : MonoBehaviour {
    [SerializeField] private UnityEngine.UI.Image image;
    [SerializeField] private Sprite spriteStarEnable;
    [SerializeField] private Sprite spriteStarDisable;

    public void EnableStar() {
        image.sprite = spriteStarEnable;
    }
    public void DisableStar() {
        image.sprite = spriteStarDisable;
    }
}
