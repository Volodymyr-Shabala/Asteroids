using UnityEngine;
using UnityEngine.Assertions;

public class PlayerWeaponInput : MonoBehaviour
{
    private IWeaponInputHandler m_WeaponInputHandler;
    [SerializeField] private KeyCode[] m_WeaponsInput = new KeyCode[0];

    public void Start()
    {
        m_WeaponInputHandler = GetComponentInChildren<IWeaponInputHandler>();
        Assert.IsNotNull(m_WeaponInputHandler,
                         $"Couldn't find IWeaponInputHandler in children in {name}");

        m_WeaponInputHandler.ListenToInput(GetInputsDelegate);
    }

    private bool[] GetInputsDelegate()
    {
        int length = m_WeaponsInput.Length;
        bool[] returnValues = new bool[length];
        for (int i = 0; i < length; i++)
        {
            returnValues[i] = Input.GetKey(m_WeaponsInput[i]);
        }

        return returnValues;
    }
}