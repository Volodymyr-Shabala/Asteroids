using UnityEngine;

public interface IEngine : IMovable, IRotatableZAxis
{
    void Push(Vector2 force, float rotation);
}