using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private Controler controler;
    [SerializeField] private Player player;
    [SerializeField] private UIControler uiControler;

    void Awake() {
        initEvents();
    }

    private void initEvents() {
        player.collectedStar += controler.OnCollectStar;
        player.interactedAnything += controler.OnInteractAnything;
        player.playerMoved += controler.OnPlayerMove;

        controler.startedCounterShifts += uiControler.OnStartCounterShifts;
        controler.incrementedOneShift += uiControler.OnIncrementOneShift;
    }
}
