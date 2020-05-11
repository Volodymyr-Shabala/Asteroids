using UnityEngine;
using UnityEngine.Assertions;

public class PlayerWeaponInput : MonoBehaviour
{
    private BaseWeaponHandler m_BaseWeaponHandler;

    public void Start()
    {
        m_BaseWeaponHandler = GetComponentInChildren<BaseWeaponHandler>();
        Assert.IsNotNull(m_BaseWeaponHandler, $"Couldn't find BaseWeaponHandler in children in {name}");
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            m_BaseWeaponHandler.ReceiveInput(0);
        }

        if (Input.GetKey(KeyCode.L))
        {
            m_BaseWeaponHandler.ReceiveInput(1);
        }
    }
}