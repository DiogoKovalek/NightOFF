using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;
    [SerializeField] private AudioSource audioSFX;

    void Awake() {
        if(audioManager == null) {
            audioManager = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    public void playSFX(AudioClip clip) {
        if(clip != null) audioSFX.PlayOneShot(clip);
    }
}
