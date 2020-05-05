using UnityEngine;

public interface IRotatableZAxis
{
    // Rigidbody2D Rigidbody { get; set; }
    // float RotationForce { get; set; }

    void Rotate(float amount);
}