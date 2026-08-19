using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Device
{
    [Header("Gate")]
    [SerializeField] private Animator animatorTopGate;
    [SerializeField] private SpriteRenderer spriteTopGate;
    [SerializeField] private SpriteRenderer spriteBodyGate;
    protected override void enableDevice() { //Levantar portao
        base.enableDevice();
        animatorTopGate.SetBool("isON", true);
        spriteTopGate.sortingLayerName = "WalkBehind";
        spriteBodyGate.sortingLayerName = "Collision";
        enableCollider();
    }
    protected override void disableDevice() { //Abaixar portao
        base.disableDevice();
        animatorTopGate.SetBool("isON", false);
        spriteTopGate.sortingLayerName = "WalkInFront";
        spriteBodyGate.sortingLayerName = "WalkInFront";
        disableCollider();
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if (thereIsNotSomethingOnTop && collision.CompareTag("Player") && !isON) {
            thereIsNotSomethingOnTop = false;
            disableDevice();
            controler.updatedDeviceInNextMove += this.OnUpdateDeviceInNextMove;
        }
    }
}
