using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device : MonoBehaviour {
    [Header("Device")]
    [SerializeField] protected new BoxCollider2D collider;
    protected Controler controler;
    [SerializeField] protected Animator anim;
    [SerializeField] protected bool isActiveInDay = true;
    protected bool isON = true;
    protected bool thereIsNotSomethingOnTop = true;
    public void linkControler(Controler controler) {
        this.controler = controler;
    }
    public virtual void OnDeviceSwitch(bool isDay) {
        if(!(isActiveInDay ^ isDay)) enableDevice();
        else disableDevice();
    }
    public virtual void OnUpdateDeviceInNextMove(bool isDay) {
        StartCoroutine(delayForThereIsNotSomethingOnTop());
        OnDeviceSwitch(isDay);
        controler.updatedDeviceInNextMove -= this.OnUpdateDeviceInNextMove;
    }
    public IEnumerator delayForThereIsNotSomethingOnTop() {
        yield return new WaitForSeconds(1f);
        thereIsNotSomethingOnTop = true;
    }
    
    protected virtual void enableDevice() {
        isON = true;
        anim?.SetBool("isON", true);
    }
    protected virtual void disableDevice() {
        isON = false;
        anim?.SetBool("isON", false);
    }
    protected void enableCollider() {
        collider.enabled = true;
    }
    protected void disbleCollider() {
        collider.enabled = false;
    }
}
