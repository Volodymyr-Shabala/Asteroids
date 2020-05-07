using UnityEngine;
using UnityEngine.Assertions;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    protected Transform[] m_ShootPositions;
    [SerializeField] protected BaseAmmunition m_BaseAmmunition;
    [SerializeField] protected float m_TimeBetweenShots = 0.2f;
    protected float m_TimeHolder;
    private bool m_Initialized;

    public void Init(Transform[] shootPositions)
    {
        Assert.IsFalse(m_Initialized, $"Trying to initialize already initialized class in {name}");
        m_ShootPositions = shootPositions;
        m_Initialized = true;
    }

    public virtual void Fire()
    {
        if (!CanFire() || !m_Initialized)
        {
            return;
        }
        
        int length = m_ShootPositions.Length;
        for (int i = 0; i < length; i++)
        {
            Instantiate(m_BaseAmmunition, m_ShootPositions[i].position, m_ShootPositions[i].rotation, transform);
        }

        m_TimeHolder = m_TimeBetweenShots + Time.time;
    }

    protected bool CanFire()
    {
        return m_TimeHolder <= Time.time;
    }
}