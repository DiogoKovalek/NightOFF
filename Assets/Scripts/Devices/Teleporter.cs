using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : Device {
    [Header("Teleporter")]
    [SerializeField] private Teleporter destinationTeleport;
    private static bool isInTeleportProcessing = false;
    private static bool inAreaOfTeleport = false;
    private static bool isTeleported = false;

    void Awake() {
        // Checar se os teleportes estao linkados entre si
        if (destinationTeleport == null || destinationTeleport?.GetDestinationTeleport() != this) {
            Debug.LogError("Os teleporters nao estão linkados corretamente");
        }
    }
    protected override void enableDevice() {
        base.enableDevice();
        enableCollider();
    }
    protected override void disableDevice() {
        base.disableDevice();
        disableCollider();
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (!inAreaOfTeleport) {
                inAreaOfTeleport = true;
                StartCoroutine(teleportingProcess(collision));
            }
            else if (inAreaOfTeleport && isInTeleportProcessing) {
                isTeleported = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (inAreaOfTeleport && !isInTeleportProcessing) {
                inAreaOfTeleport = false;
                isTeleported = false;
            }
        }
    }

    private IEnumerator teleportingProcess(Collider2D collision) {
        Player player = collision.gameObject.GetComponentInParent<Player>();
        if (player == null) yield break;
        if (isTeleported) yield break;
        // Seta atributos de controle
        isInTeleportProcessing = true;
        player.SetIsFreeForMove(false);

        while (player.GetIsMoving()) { // espera até o player para de se mover
            yield return null;
        }

        Animator playerAnimator = player.GetPlayerAnimator();
        // Executa animacao de teleport
        playerAnimator.SetTrigger("teleporting");
        yield return new WaitForSeconds(0.583f);
        // Teleporta  
        collision.transform.parent.position = GetPositionDestination();
        yield return new WaitUntil(() => isTeleported); // Tentart depois um yield return null

        // Executa animacao de finalizar teleport
        playerAnimator.SetTrigger("teleporting");
        yield return new WaitForSeconds(0.583f);
        
        // Seta atributos que indicam finalizacao de teleport
        isInTeleportProcessing = false;
        player.SetIsFreeForMove(true);
    }
    public Teleporter GetDestinationTeleport() {
        return destinationTeleport;
    }
    private Vector2 GetPositionDestination() {
        return destinationTeleport.transform.position;
    }
}
