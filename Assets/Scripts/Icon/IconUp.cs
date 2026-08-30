using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconUp : MonoBehaviour {
    [SerializeField] private GameObject spriteIcon;
    [SerializeField] private float timeEnabled = 1;
    [SerializeField] private float percentToStartLostAlpha = 0.80f; // 0 a 1
    [SerializeField] private float distMoveUp = 0.5f;
    private Animator animator;
    private SpriteRenderer sprRen;
    private Color colorSprite;
    private float timeAlpha; // == timeEnabled - timeEnabled*percentToStartLostAlpha
    private bool isActive = false;
    private Vector2 initialPosition;
    private Vector2 targetPosition;

    private float t = 0;
    void Awake() {
        if (spriteIcon != null) {
            animator = spriteIcon?.GetComponent<Animator>();
            sprRen = spriteIcon?.GetComponent<SpriteRenderer>();
            colorSprite = sprRen.color;
            initialPosition = spriteIcon.transform.position;
            targetPosition = (Vector2) spriteIcon.transform.position + Vector2.up * distMoveUp;
        }
        timeAlpha = timeEnabled*percentToStartLostAlpha;
    }
    void Update() {
        if (isActive) {
            t += Time.deltaTime;
            spriteIcon.transform.position = Vector2.Lerp(initialPosition, targetPosition, t / timeEnabled);
            if (t >= timeAlpha) {
                //Tem algum erro aqui
                colorSprite.a = Mathf.Lerp(1,0, (t - timeAlpha) / (timeEnabled - timeAlpha));
                sprRen.color = colorSprite;
            }
            if (t >= timeEnabled) {
                StopMovement();
            }
        }
    }
    public void StartMovement(bool isON) {
        gameObject.SetActive(true);
        t = 0;
        colorSprite.a = 1;
        sprRen.color = colorSprite;
        spriteIcon.transform.position = initialPosition;
        isActive = true;
        if(animator != null) animator?.SetBool("isON", isON);
    }
    private void StopMovement() {
        isActive = false;
        gameObject.SetActive(false);
    }
}
