public class Rocket : BaseAmmunition
{
    protected override void Update()
    {
        base.Update();
        Move(transform.up);
    }
}