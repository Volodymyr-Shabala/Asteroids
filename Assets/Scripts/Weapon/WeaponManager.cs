using UnityEngine;
using UnityEngine.Assertions;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private ShipWeapon[] m_ShipWeapons = new ShipWeapon[0];
    [SerializeField] private BaseWeaponInputHandler m_WeaponInputHandler = null;

    private void Awake()
    {
        Assert.IsNotNull(m_WeaponInputHandler, $"BaseWeaponHandler needs to be assigned in {name}");
        Assert.AreNotEqual(m_ShipWeapons.Length, 0, $"ShipWeapon needs to be assigned in {name}");

        Transform myTransform = transform;
        Vector3 position = myTransform.position;

        BaseWeaponInputHandler weaponInputHandler =
            Instantiate(m_WeaponInputHandler, position, Quaternion.identity, myTransform);

        int length = m_ShipWeapons.Length;
        IWeapon[] weapons = new IWeapon[length];

        for (int i = 0; i < length; i++)
        {
            weapons[i] = Instantiate(m_ShipWeapons[i].prefab, position, Quaternion.identity,
                                     weaponInputHandler.transform);
            weapons[i].Init(m_ShipWeapons[i].shootPositions);
        }

        weaponInputHandler.Init(weapons);
    }
}

[System.Serializable]
public struct ShipWeapon
{
    public BaseWeapon prefab;
    public Transform[] shootPositions;
}