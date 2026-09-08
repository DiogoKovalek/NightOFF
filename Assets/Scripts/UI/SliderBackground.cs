using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBackground : MonoBehaviour {
    [Header("BlackScreen")]
    [SerializeField] private RectTransform BlackScreen;
    [SerializeField] private float timeToSlide = 0.3f;
    private bool isBlackScreenInCenter = true;
    private float widthCanvas;

    void Awake() {
        // BlackScreen Começa no centro da tela
        BlackScreen.gameObject.SetActive(true);
        BlackScreen.anchoredPosition = Vector3.zero;
        isBlackScreenInCenter = true;

        // Pegar tamanho do Canvas
        CanvasScaler can = GetComponent<CanvasScaler>();
        if(can != null) widthCanvas = can.referenceResolution.x;
        else widthCanvas = 1920; // Padrao
    }

    // actionNext is the action that will execute when the slider end
    public void StartSlide(Action actionNext = null) {
        StartCoroutine(slider(actionNext));
    }

    private IEnumerator slider(Action actionNext = null) {
        float t = 0;
        float startX = BlackScreen.anchoredPosition.x;
        float endX = 0;
        Vector3 position = Vector3.zero;
        BlackScreen.gameObject.SetActive(true);

        if (isBlackScreenInCenter) {// Centro para esquerda
            startX = 0;
            endX = -widthCanvas;
            position.x = startX;
            BlackScreen.anchoredPosition = position;
        }
        else {// Esquerda para Centro
            startX = widthCanvas;
            endX = 0;
            position.x = startX;
            BlackScreen.anchoredPosition = position;
        }
        while (t < timeToSlide) {
            t += Time.deltaTime;
            position.x = Mathf.Lerp(startX, endX, t/timeToSlide);
            BlackScreen.anchoredPosition = position;
            yield return null;
        }
        position.x = endX;
        BlackScreen.anchoredPosition = position;

        isBlackScreenInCenter = !isBlackScreenInCenter;
        if(!isBlackScreenInCenter) BlackScreen.gameObject.SetActive(false);

        actionNext?.Invoke();
    }
}
