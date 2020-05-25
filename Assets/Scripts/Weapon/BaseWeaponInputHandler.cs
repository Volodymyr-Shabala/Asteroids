using System;
using UnityEngine;

public class BaseWeaponInputHandler : MonoBehaviour, IWeaponInputHandler
{
    protected IWeapon[] m_Weapons;
    protected Func<bool[]> m_WeaponsInputValuesFunc;
    
    protected bool m_Initialized;
    protected bool m_InputAssigned;

    public void Init(IWeapon[] weapons)
    {
        m_Weapons = weapons;
        m_Initialized = true;
    }

    protected void Update()
    {
        if (!m_Initialized || !m_InputAssigned)
        {
            return;
        }
        
        bool[] inputValues = m_WeaponsInputValuesFunc();

        int length = inputValues.Length;
        for (int i = 0; i < length; i++)
        {
            if (inputValues[i])
            {
                HandleInput(i);
            }
        }
    }

    public void ListenToInput(int inputValue)
    {
        if (inputValue < 0)
        {
            return;
        }

        m_WeaponsInputValuesFunc = () =>
                                   {
                                       bool[] returnValue = new bool[inputValue + 1];
                                       returnValue[inputValue] = true;
                                       return returnValue;
                                   };
        m_InputAssigned = true;
    }

    public void ListenToInput(Func<bool[]> inputValues)
    {
        m_WeaponsInputValuesFunc = inputValues;
        m_InputAssigned = true;
    }

    protected virtual void HandleInput(int index)
    {
        if (IsWithinRange(index))
        {
            m_Weapons[index].Fire();
        }
    }

    protected bool IsWithinRange(int index)
    {
        return index >= 0 && index < m_Weapons.Length;
    }
}