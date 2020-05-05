using UnityEngine;

public interface IObject : IDestroyable{
    Rigidbody2D rigidbody{ get; set; }
}