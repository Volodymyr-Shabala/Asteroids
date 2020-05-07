public interface IWeaponsHandler
{
    void Init(IWeapon[] weapons);
    void ReceiveInput(int index);
}