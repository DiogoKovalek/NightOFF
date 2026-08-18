using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : Device {

    protected override void enableDevice() {
        base.enableDevice();
        disbleCollider();
    }
    protected override void disableDevice() {
        base.disableDevice();
        enableCollider();
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if (thereIsNotSomethingOnTop && collision.CompareTag("Player") && !isON) {
            thereIsNotSomethingOnTop = false;
            enableDevice();
            controler.updatedDeviceInNextMove += this.OnUpdateDeviceInNextMove;
        }
    }
}
