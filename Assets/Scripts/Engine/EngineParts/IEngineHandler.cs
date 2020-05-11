using UnityEngine;

public interface IEngineHandler
{ 
    void ReceiveInput(Vector2 movementInput, float rotationInput);
}