using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    #region Movement
    [SerializeField] private float distPoints = 1; // Distancia entre cada centro do quadrado para o player ficar no centro
    [SerializeField] private float speedMovement = 8;
    private bool isMoving = false;
    private Vector2 targetMove;
    #endregion

    void Start() {

    }
    void Update() {
        //Movement
        byte direction = InputManager.inputManager.GetMoveDirection();

        if (direction != 0 && !isMoving) {
            defineTargetForMove(direction);
        }
        if (isMoving) {
            movement();
        }
    }

    private void defineTargetForMove(byte direction) {
        switch (direction) {
            case 1: // UP
                targetMove = (Vector2)transform.position + new Vector2(0, distPoints);
                break;
            case 2: // RIGHT
                targetMove = (Vector2)transform.position + new Vector2(distPoints, 0);
                break;
            case 3: // DOWN
                targetMove = (Vector2)transform.position + new Vector2(0, -distPoints);
                break;
            case 4: // LEFT
                targetMove = (Vector2)transform.position + new Vector2(-distPoints, 0);
                break;
            default: // NONE
                return;
        }
        isMoving = true;
    }
    private void movement() {
        transform.position = Vector2.MoveTowards(transform.position, targetMove, speedMovement*Time.deltaTime);
        if((Vector2) transform.position == targetMove) {
            isMoving = false;
        }
    }
}
