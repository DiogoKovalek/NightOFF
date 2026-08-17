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
        Debug.Log("esta");
        if (thereIsNotSomethingOnTop && collision.CompareTag("Player") && !isON) {
            Debug.Log("Conectou");
            thereIsNotSomethingOnTop = false;
            enableDevice();
            controler.updatedDeviceInNextMove += this.OnUpdateDeviceInNextMove;
        }
    }
    /*
    private IEnumerator stayForDisableDevice() {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Player"));
        filter.useLayerMask = true;
        filter.useTriggers = true;

        List<Collider2D> colisions = new List<Collider2D>();
        Vector2 pos = (Vector2) transform.position + collider.offset;

        while (!isFreeForDisableDevice) {
            yield return null; 

        }
        disableDevice();
        coroutineDisableDevice = null;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Entrou");
        if (collision.CompareTag("Player") && !isON && coroutineDisableDevice != null) {
            isFreeForDisableDevice = false;
            enableDevice();
            coroutineDisableDevice = StartCoroutine(stayForDisableDevice());
        }
    }
    */
}
