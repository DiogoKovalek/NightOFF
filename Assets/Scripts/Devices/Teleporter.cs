using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : Device {
    [Header("Teleporter")]
    [SerializeField] private Teleporter destinationTeleport;
    private bool isInTeleportProcessing = false;
    private bool isFinishTeleport = false;

    void Awake() {
        // Checar se os teleportes estao linkados entre si
        if(destinationTeleport == null || destinationTeleport?.GetDestinationTeleport() != this) {
            Debug.LogError("Os teleporters nao estão linkados corretamente");
        }
    }
    protected override void enableDevice() {
        base.enableDevice();
        enableCollider();
    }
    protected override void disableDevice() {
        base.disableDevice();
        enableCollider();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !isInTeleportProcessing) {
            isInTeleportProcessing = true;
            destinationTeleport.SetIsInTeleportProcessing(true);


            collider.transform.position = GetPositionDestination();


        }
    }
    public void SetIsInTeleportProcessing(bool value) {
        isInTeleportProcessing = value;
    }
    public void SetIsFinishTeleport(bool value) {
        isFinishTeleport = value;
    }
    public Teleporter GetDestinationTeleport() {
        return destinationTeleport;
    }
    private Vector2 GetPositionDestination() {
        return destinationTeleport.transform.position;
    }
}
