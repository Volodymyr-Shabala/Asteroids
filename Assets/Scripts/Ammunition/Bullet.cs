public class Bullet : BaseAmmunition
{
    protected override void Update()
    {
        base.Update();
        Move(transform.up);
    }
}