using UnityEngine;
public interface IMovable{
    public void SetSpeed(float value);
    public void SetDirection(Vector2 value);
    public void Movement(bool isMovement);
}