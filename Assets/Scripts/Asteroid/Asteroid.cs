using UnityEngine.Assertions;

public class Asteroid : SpaceObject, IAsteroid
{
    /*
     * Random scale
     * Bigger scale => more damage => more hp => slower => bigger mass
     * Move with linear and angular speed
     * If big enough on destroy spawn small asteroids. Small asteroids move in opposite directions
     */

    private float m_Health;
    private float m_ScaleToPerformDestroyLogic = 3f;
    private bool m_Initialized;
    
    public void Init(float health, float scaleToPerformDestroyLogic)
    {
        Assert.IsFalse(m_Initialized, $"Trying to initialize already initialized class {name}.");
        
        m_Health = health;
        m_ScaleToPerformDestroyLogic = scaleToPerformDestroyLogic;
        
        m_Initialized = true;
    }

    public override void TakeDamage(float amount)
    {
        m_Health -= amount;
        if (m_Health <= 0)
        {
            if (transform.localScale.x >= m_ScaleToPerformDestroyLogic)
            {
                GetComponent<IAsteroidDestroyManager>()?.Handle();
            }

            Destroy(gameObject);
        }
    }
}