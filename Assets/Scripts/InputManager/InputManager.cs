using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour {
    //Singleton
    public static InputManager inputManager;
    private PlayerInput playerInput;
    private byte moveDirection = 0; // 0:NULL 1:UP 2:RIGHT 3:DOWN 4:LEFT

    #region TOUCH CONTROLS
    private Vector2 startPosition;
    private Vector2 endPosition;
    private bool isPressed = false;
    private float distForSwipe = 100f;

    private InputAction touchPositionAction;

    #endregion

    #region EVENTS

    #endregion

    void Awake() {
        if (inputManager == null) {
            inputManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }

        playerInput = GetComponent<PlayerInput>();
        touchPositionAction = playerInput.actions["TouchPosition"];
    }
    public void OnMove(InputValue value) {
        Vector2 vetor = value.Get<Vector2>().normalized;

        if (vetor == Vector2.zero) {
            moveDirection = 0;
            return;
        }

        moveDirection = transformVector2InMoveDirection(vetor);
    }
    public void OnPrimaryTouch(InputValue value) {
        if (value.isPressed && !isPressed) { // Iniciou o toque
            isPressed = true;
            startPosition = touchPositionAction.ReadValue<Vector2>();
        }
        else if(!value.isPressed && isPressed){ // Finalizou o toque
            isPressed = false;
            endPosition = touchPositionAction.ReadValue<Vector2>();
            checkSwipe();
        }
    }
    public byte GetMoveDirection() {
        byte value = moveDirection;
        moveDirection = 0; // Sempre seta para 0 o valor do input para que nao se repita mais de uma vez
        return value;
    }

    private byte transformVector2InMoveDirection(Vector2 vectorNormalized) {
        if(vectorNormalized.sqrMagnitude < 0.98) {
            return 0;
        }
        byte move;
        if (Mathf.Abs(vectorNormalized.x) > Mathf.Abs(vectorNormalized.y)) {
            move = vectorNormalized.x > 0 ? (byte)2 : (byte)4;
        }
        else {
            move = vectorNormalized.y > 0 ? (byte)1 : (byte)3;
        }
        return move;
    }
    private void checkSwipe() {
        Vector2 direction = endPosition - startPosition;
        if(direction.magnitude < distForSwipe) return;

        moveDirection = transformVector2InMoveDirection(direction.normalized);
    }
}
