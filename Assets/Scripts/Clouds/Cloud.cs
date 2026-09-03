using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour, IMovable {
    private float speed;
    private Vector2 direction;
    private float limit;
    private bool isFreeForMove = false;

    void Update() {
        if (isFreeForMove) {
            transform.position = (Vector2) transform.position + speed * direction * Time.deltaTime;
            if(transform.position.x <= limit) gameObject.SetActive(false);
        }
    }

    public void Movement(bool isMovement) {
        isFreeForMove = isMovement;
    }
    public void SetDirection(Vector2 value) {
        direction = value;
    }
    public void SetSpeed(float value) {
        speed = value;
    }
    public void SetLimit(float value) {
        limit = value;
    }
}
