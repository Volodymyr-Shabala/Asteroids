using System;
using UnityEngine;

public interface IEngineInputHandler
{ 
    void ListenToInput(Vector2 movementInput, float rotationInput);
    void ListenToInput(Func<Vector2> movementInput, Func<float> rotationInput);
}