using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconUp : MonoBehaviour {
    [SerializeField] private GameObject spriteIcon;
    [SerializeField] private float timeEnabled = 2;
    private Animator animator;
    private bool isActive = false;

    private float t = 0;
    void Awake() {
        animator = spriteIcon?.GetComponent<Animator>();
    }
    void Update() {
        if (isActive) {
            t += Time.deltaTime;
            if(t >= timeEnabled) {
                StopMovement();
            }
        }
    }
    public void StartMovement(bool isON) {
        t = 0;
        spriteIcon.transform.position = Vector2.zero;
        gameObject.SetActive(true);
        isActive = true;
        animator?.SetBool("isON", isON);
    }
    private void StopMovement() {
        isActive = false;
        gameObject.SetActive(false);
    }
}
