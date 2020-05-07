using UnityEngine;

public interface IWeapon
{
    void Init(Transform[] shootPositions);
    void Fire();
}