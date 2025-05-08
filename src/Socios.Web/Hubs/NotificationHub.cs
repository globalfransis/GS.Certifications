using Microsoft.AspNetCore.SignalR;

namespace Socios.Web.Hubs;

//TODO: implementar interfaz desde Application
public class NotificationHub : Hub
{
    public Task SendNotificationTo(string target, string message)
    {
        return Clients.Client(target).SendAsync("ReceiveNotification", message);
    }
}