public interface IDestroyManager
{
    void Init(IDamageable healthContainer, int spawnAmount, float pushForce);
    void Handle();
}