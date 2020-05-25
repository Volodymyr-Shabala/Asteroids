using UnityEngine;

class MissileWeapon : BaseWeapon
{
    private int m_MissilesFired;
    
    public override void Fire()
    {
        if (!CanFire() || !m_Initialized)
        {
            return;
        }
        
        int length = m_ShootPositions.Length;
        for (int i = 0; i < length; i++)
        {
            SpaceObject ammunition = ObjectPool.Get(m_BaseAmmunition);
            Transform ammunitionTransform = ammunition.transform;
            ammunitionTransform.position = m_ShootPositions[i].position;
            ammunitionTransform.rotation = m_ShootPositions[i].rotation;

            m_MissilesFired++;
            ammunition.OnReset += OnMissileHit;
        }
    }

    private void OnMissileHit(SpaceObject spaceObject)
    {
        m_MissilesFired--;
        spaceObject.OnReset -= OnMissileHit;
    }

    protected override bool CanFire()
    {
        return m_MissilesFired <= 0;
    }
}