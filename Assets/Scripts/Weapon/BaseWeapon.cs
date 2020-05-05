using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    protected Transform[] m_ShootPositions;
    [SerializeField] protected BaseAmmunition m_BaseAmmunition;
    [SerializeField] protected float m_TimeBetweenShots = 0.2f;
    protected float m_Timer;

    public void Init(Transform[] shootPositions)
    {
        m_ShootPositions = shootPositions;
    }
    
    protected virtual void Update()
    {
        if (m_Timer > 0)
        {
            m_Timer -= Time.deltaTime;
        }
    }

    public virtual void Fire()
    {
        if (!CanFire())
        {
            return;
        }
        
        int length = m_ShootPositions.Length;
        for (int i = 0; i < length; i++)
        {
            Instantiate(m_BaseAmmunition, m_ShootPositions[i].position, m_ShootPositions[i].rotation);
        }

        m_Timer = m_TimeBetweenShots;
    }

    protected virtual bool CanFire()
    {
        return m_Timer <= 0;
    }
}