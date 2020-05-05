using UnityEngine;

public class WeaponsHandler : MonoBehaviour, IWeaponsHandler
{
    [SerializeField] private ShipWeapon[] m_ShipWeapons = new ShipWeapon[0];
    private BaseWeapon[] m_Weapons;

    private void Awake()
    {
        int length = m_ShipWeapons.Length;
        m_Weapons = new BaseWeapon[length];

        Transform myTransform = transform;
        Vector3 position = myTransform.position;

        for (int i = 0; i < length; i++)
        {
            m_Weapons[i] = Instantiate(m_ShipWeapons[i].prefab, position, Quaternion.identity, myTransform);
            m_Weapons[i].Init(m_ShipWeapons[i].shootPositions);
        }
    }

    public void ReceiveInput(int index)
    {
        if (index >= 0 && index < m_Weapons.Length)
        {
            m_Weapons[index].Fire();
        }
    }
}

[System.Serializable]
public struct ShipWeapon
{
    public BaseWeapon prefab;
    public Transform[] shootPositions;
}