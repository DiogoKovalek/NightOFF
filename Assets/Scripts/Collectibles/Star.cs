using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour, ICollect {
    [Header("animation Lerp")]
    [SerializeField] private GameObject spriteStar;
    [SerializeField] private bool isAnimationLerp;
    [SerializeField] private float rayDistLerp = 1f;
    [SerializeField] private float speedLerp = 2;
    private float t = 0;
    private Vector3 startPos;
    private Vector3 targetPos;

    [Header("SFX")]
    [SerializeField] private AudioClip collectSFX;
    void Start() {
        startPos = spriteStar.transform.position;
        targetPos = startPos + Vector3.up * rayDistLerp;
    }
    void Update() {
        if (isAnimationLerp) animationLerp();
    }

    private void animationLerp() {
        t = Mathf.PingPong(speedLerp * Time.time, 1f);
        spriteStar.transform.position = Vector2.Lerp(startPos, targetPos, t);
    }
    public void collected(Player player) {
        AudioManager.audioManager.playSFX(collectSFX);
        Destroy(this.gameObject);
    }

}
