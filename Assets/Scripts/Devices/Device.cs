using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device : MonoBehaviour {
    [Header("Device")]
    [SerializeField] protected BoxCollider2D collider;
    [SerializeField] protected Animator anim;
    [SerializeField] protected bool isActiveInDay = true;
    [SerializeField] protected bool isON = true;
    public virtual void OnDeviceSwitch(bool isDay) {
        if(!(isActiveInDay ^ isDay)) enableDevice();
        else disableDevice();
    }
    protected virtual void enableDevice() {
        isON = true;
        anim?.SetBool("isON", true);
    }
    protected virtual void disableDevice() {
        isON = false;
        anim?.SetBool("isON", false);
    }
}
