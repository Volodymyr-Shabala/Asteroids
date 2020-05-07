public interface IEngineHandler
{
    void Init(IEngine engine);
    void ReceiveInput(float horizontal, float vertical);
}