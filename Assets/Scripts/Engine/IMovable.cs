using UnityEngine;

public interface IMovable
{
    // Rigidbody2D Rigidbody { get; set; }
    // float MoveForce { get; set; }

    void Move(Vector2 dir);
}