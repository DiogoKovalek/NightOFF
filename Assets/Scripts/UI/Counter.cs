using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image image;
    [SerializeField] private Sprite spriteEnable;
    [SerializeField] private Sprite spriteDisable;

    void Awake() {
        DisableCounter();
    }
    public void EnableCounter() {
        image.sprite = spriteEnable;
    }

    public void DisableCounter() {
        image.sprite = spriteDisable;
    }
}
