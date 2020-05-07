using UnityEngine;

public interface IObject : IDestroyable{
    Rigidbody2D Rigidbody2D{ get; set; }
}