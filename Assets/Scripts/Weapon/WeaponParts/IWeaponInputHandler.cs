using System;

public interface IWeaponInputHandler
{
    void Init(IWeapon[] weapons);
    void ListenToInput(int inputValue);
    void ListenToInput(Func<bool[]> inputValues);
}