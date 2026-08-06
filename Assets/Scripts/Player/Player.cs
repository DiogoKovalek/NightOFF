using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Collision")]
    [SerializeField] private LayerMask collisionLayer;
    [Header("Sprite Player")]
    #region Sprite Player
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private float jumpHeight = 0.5f;
    private Vector2 playerSpriteInitialPos = new Vector2(0, -0.25f);
    private SpriteRenderer playerSpriteRender;
    private Animator playerAnimator;
    #endregion

    [Header("Movement Varibles")]
    #region Movement
    [SerializeField] private float distPoints = 1; // Distancia entre cada centro do quadrado para o player ficar no centro
    [SerializeField] private float speedMovement = 8;
    private bool isMoving = false;
    private Vector2 targetMove;
    private Vector2 startMovePos;
    #endregion
    void Awake() {
        if(playerSprite != null) {
            playerSpriteRender = playerSprite.GetComponent<SpriteRenderer>();
            playerAnimator = playerSprite.GetComponent<Animator>();
        }
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
                playerSpriteRender.flipX = true; 
                break;
            case 2: // RIGHT
                targetMove = (Vector2)transform.position + new Vector2(distPoints, 0);
                playerSpriteRender.flipX = true;
                break;
            case 3: // DOWN
                targetMove = (Vector2)transform.position + new Vector2(0, -distPoints);
                playerSpriteRender.flipX = false;
                break;
            case 4: // LEFT
                targetMove = (Vector2)transform.position + new Vector2(-distPoints, 0);
                playerSpriteRender.flipX = false;
                break;
            default: // NONE
                return;
        }
        if (isColision(targetMove)) {
            targetMove = (Vector2) transform.position;
            return;
        }
        startMovePos = transform.position;
        isMoving = true;
        playerAnimator.SetBool("isJumping", true);
    }
    private void movement() {
        transform.position = Vector2.MoveTowards(transform.position, targetMove, speedMovement * Time.deltaTime);
        
        float distanceCovered = Vector2.Distance(startMovePos, transform.position);
        float progress = distanceCovered / distPoints;

        float currentJumpHeight = 4f * jumpHeight * progress * (1f - progress);

        playerSprite.transform.localPosition = playerSpriteInitialPos + new Vector2(0, currentJumpHeight);

        if ((Vector2)transform.position == targetMove) {
            isMoving = false;
            playerAnimator.SetBool("isJumping", false);
            playerSprite.transform.localPosition = playerSpriteInitialPos;
        }
    }
    private bool isColision(Vector2 point) {
        Vector2 direction = (targetMove - (Vector2) transform.position). normalized;
        RaycastHit2D hit = Physics2D.Raycast((Vector2) transform.position, direction, distPoints, collisionLayer);
        
        return hit.collider != null;
    }
    
}
