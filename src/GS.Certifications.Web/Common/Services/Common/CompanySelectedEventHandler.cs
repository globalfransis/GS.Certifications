using GSF.Application.Common.Models;
using GSF.Domain.Entities.Companies.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Common.Services.Common;

public class CompanySelectedEventHandler : INotificationHandler<DomainEventNotification<CompanySelectedEvent>>
{

    public async Task Handle(DomainEventNotification<CompanySelectedEvent> notification, CancellationToken cancellationToken)
    {
        //TODO: Revisar esto
        //var companyId = notification.DomainEvent.CompanyId;
        await Task.CompletedTask;
    }
}
