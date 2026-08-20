using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Device {
    [Header("Gate")]
    [SerializeField] private Animator animatorTopGate;
    [SerializeField] private SpriteRenderer spriteTopGate;
    [SerializeField] private SpriteRenderer spriteBodyGate;
    private bool isInStayForEnable = false;
    protected override void enableDevice() { //Levantar portao
        if (checkIfSomethingOnTop() && !isInStayForEnable) {
            controler.updatedDeviceInNextMove += this.OnUpdateDeviceInNextMove;
            isInStayForEnable = true;
            return;
        }
        isInStayForEnable = false;
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
    private bool checkIfSomethingOnTop() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0.01f, 0, 0), Vector2.right, 0.02f, LayerMask.GetMask("Player"));
        return hit.collider != null;
    }
}
