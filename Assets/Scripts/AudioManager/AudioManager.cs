using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager audioManager;
    [SerializeField] private AudioSource audioMusic;
    [SerializeField] private AudioSource audioSFX;

    void Awake() {
        if (audioManager == null) {
            audioManager = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    public void playSFX(AudioClip clip) {
        if (clip != null && audioSFX != null) audioSFX.PlayOneShot(clip);
    }
    public void stopSFX() {
        if(audioSFX != null) audioSFX.Stop();
    }

    public void pauseMusic() {
        if (audioMusic != null) audioMusic.Pause();
    }
    public void despauseMusic() {
        if (audioMusic != null) audioMusic.Play();
    }
}
