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
        disableCollider();
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (!inAreaOfTeleport) {
                inAreaOfTeleport = true;
                StartCoroutine(teleportingProcess(collision));
            }else if (inAreaOfTeleport && isInTeleportProcessing) {
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
        if(player == null) yield break;
        if(isTeleported) yield break;
        // Seta atributos de controle
        isInTeleportProcessing = true;
        //destinationTeleport.SetIsInTeleportProcessing(true);
        
        while (player.GetIsMoving()) { // espera até o player para de se mover
            yield return null;
        }

        // Executa animacao de teleport

        // Teleporta
        
        collision.transform.parent.position = GetPositionDestination();
        yield return new WaitUntil(() => isTeleported); // Tentart depois um yield return null

        // Executa animacao de finalizar teleport

        // Seta atributos que indicam finalizacao de teleport
        isInTeleportProcessing = false;
        //destinationTeleport.SetIsInTeleportProcessing(false);

        //ADENDO -> depois deve travar a possibilidade do player se movimentar
        //usando um isFreeForMove
        Debug.Log($"{gameObject.name}: Finalizou a Corrotina");
    }
    /*
    public void SetIsInTeleportProcessing(bool value) {
        isInTeleportProcessing = value;
    }
    public void SetInAreaOfTeleport(bool value) {
        inAreaOfTeleport = value;
    }
    */
    public Teleporter GetDestinationTeleport() {
        return destinationTeleport;
    }
    private Vector2 GetPositionDestination() {
        return destinationTeleport.transform.position;
    }
}
