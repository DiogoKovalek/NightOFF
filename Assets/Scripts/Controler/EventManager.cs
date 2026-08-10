using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private Controler controler;
    [SerializeField] private Player player;

    void Awake() {
        
    }

    private void initEvents() {
        player.collectedAnything += controler.OnCollectAnything;
        player.interactedAnything += controler.OnInteractAnything;
        player.playerMoved += controler.OnPlayerMove;
    }
}
