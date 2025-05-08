using GSF.Application.Interfaces;
using GSF.Domain.Notifications;
using Microsoft.AspNetCore.SignalR;
using Socios.Web.Hubs;

namespace Socios.Web.Common.Services;

public class NotificationServiceMock : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationServiceMock(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public bool SendNotificationToUsers(IList<Notification> notificationUsers)
    {
        Console.WriteLine("Sending notifications...");

        foreach (Notification notificationUser in notificationUsers)
        {
            _hubContext.Clients.User(notificationUser.User.Login).SendAsync("ReceiveNotification", notificationUser.Body);
        }

        return true;
    }
}
