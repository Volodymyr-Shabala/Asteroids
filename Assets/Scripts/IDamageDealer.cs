public interface IDamageDealer
{
    float Damage { get; set; }
    void ApplyDamage(IObject target);
}