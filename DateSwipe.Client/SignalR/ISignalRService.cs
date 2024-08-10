namespace DateSwipe.Client.SignalR
{
    public interface ISignalRService
    {
        Task StartAsync();
        Task HandleReconnection();
        Task StopAsync();
        event Func<string, int, Task> OnMatchReceived;

    }
}
