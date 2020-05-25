using UnityEngine;
using UnityEngine.Assertions;

public class UFOWeaponInput : MonoBehaviour
{
    private IWeaponInputHandler m_BaseWeaponInputHandler;

    public void Start()
    {
        m_BaseWeaponInputHandler = GetComponentInChildren<IWeaponInputHandler>();
        Assert.IsNotNull(m_BaseWeaponInputHandler,
                         $"Couldn't find IWeaponInputHandler in children in {name}");

        m_BaseWeaponInputHandler.ListenToInput(() => new [] {true});
    }
}