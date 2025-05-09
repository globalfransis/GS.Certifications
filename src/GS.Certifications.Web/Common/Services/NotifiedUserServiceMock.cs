using GSF.Application.Common.Interfaces;
using GSF.Domain.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace GS.Certifications.Web.Common.Services;

public class NotifiedUserServiceMock : INotificationSuscribedUsersService
{
    public IList<Subscription> GetSubscriptionsByNotificationType(string notificationType)
    {
        return new List<Subscription>()
        {
            new Subscription()
            {
                Id = 1,
                UserId = 1,
                Type = "BookingDownEventHandler",
                Criteria = "Id"
            },
            new Subscription()
            {
                Id = 2,
                UserId = 1,
                Type = "BookingDownEventHandler",
                Criteria = "DateRange"
            },
            new Subscription()
            {
                Id = 3,
                UserId = 2,
                Type = "BookingDownEventHandler",
                Criteria = "Id"
            },
            new Subscription()
            {
                Id = 4,
                UserId = 2,
                Type = "BookingDownEventHandler",
                Criteria = "DateRange"
            },
        };


    }

    public IList<SubscriptionKeyValue> GetValuesForSuscription(long IdNotificationSuscription)
    {
        var keyValues = new List<SubscriptionKeyValue>()
        {
            new SubscriptionKeyValue()
            {
                Id = 1,
                SubscriptionId = 1,
                SubscriptionKey = "IdBooking",
                SubscriptionValue = "18"
            },
            new SubscriptionKeyValue()
            {
                Id = 2,
                SubscriptionId = 2,
                SubscriptionKey = "DateFrom",
                SubscriptionValue = "2020-03-01"
            },
            new SubscriptionKeyValue()
            {
                Id = 2,
                SubscriptionId = 2,
                SubscriptionKey = "DateTo",
                SubscriptionValue = "2020-03-10"
            },
        };

        return keyValues.Where(x => x.SubscriptionId == IdNotificationSuscription).ToList();
    }
}
