namespace messager.Signal.Shared
{
    public interface IAppHub
    {
        Task ToAll(string Message);
    }
}
