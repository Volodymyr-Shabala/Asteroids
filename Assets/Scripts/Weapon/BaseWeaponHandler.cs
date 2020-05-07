using UnityEngine;

public class BaseWeaponHandler : MonoBehaviour, IWeaponsHandler
{
    protected IWeapon[] m_Weapons;
    protected int weaponsAmount;

    public void Init(IWeapon[] weapons)
    {
        m_Weapons = weapons;
        weaponsAmount = m_Weapons.Length;
    }

    public virtual void ReceiveInput(int index)
    {
        if (IsWithinRange(index))
        {
            HandleInput(index);
        }
    }

    protected virtual void HandleInput(int index)
    {
        m_Weapons[index].Fire();
    }

    protected bool IsWithinRange(int index)
    {
        return index >= 0 && index < weaponsAmount;
    }
}