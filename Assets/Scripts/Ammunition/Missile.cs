using UnityEngine;

public class Missile : BaseAmmunition
{
    [SerializeField] private float m_SeekingRadius = 15;
    [SerializeField] private LayerMask m_LayerToAttack = 0;

    protected override void Start()
    {
        base.Start();
        FindObjectOfType<ScreenWrappingManager>().AddToWrap(transform);
    }

    protected override void Update()
    {
        Move(transform.up);
        Transform target = GetClosestTarget();
        float angle = GetRotationToTarget(target);
        Rotate(angle);
    }

    private float GetRotationToTarget(Transform target)
    {
        Transform myTransform = transform;
        Vector2 dir = (target.position - myTransform.position).normalized;
        return -Vector3.Cross(dir, myTransform.up).z;
    }

    private Transform GetClosestTarget()
    {
        Transform myTransform = transform;
        Transform returnValue = myTransform;
        Vector3 myPosition = myTransform.position;
        Collider2D[] colliders = new Collider2D[5];
        Physics2D.OverlapCircleNonAlloc(myPosition, m_SeekingRadius, colliders, m_LayerToAttack);

        float dist = float.MaxValue;
        foreach (Collider2D col in colliders)
        {
            if (col != null)
            {
                Transform collidedTransform = col.transform;
                float distance = Vector2.Distance(myPosition, collidedTransform.position);
                if (dist > distance)
                {
                    dist = distance;
                    returnValue = collidedTransform;
                }
            }
        }

        return returnValue;
    }
}