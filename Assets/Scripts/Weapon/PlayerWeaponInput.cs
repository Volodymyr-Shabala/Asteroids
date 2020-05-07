using UnityEngine;
using UnityEngine.Assertions;

public class PlayerWeaponInput : MonoBehaviour
{
    private BaseWeaponHandler m_BaseWeaponHandler;
    private bool m_Initialized;

    public void Start()
    {
        Assert.IsFalse(m_Initialized, $"Trying to initialize already initialized class in {name}");
        m_BaseWeaponHandler = GetComponentInChildren<BaseWeaponHandler>();
        Assert.IsNotNull(m_BaseWeaponHandler, $"Couldn't find BaseWeaponHandler in children in {name}");
        m_Initialized = true;
    }
    private void Update()
    {
        if (!m_Initialized)
        {
            return;
        }
        
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