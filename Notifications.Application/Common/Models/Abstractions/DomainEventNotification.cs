using MediatR;
using Notifications.Domain.Common.Events;

namespace Notifications.Application.Common.Models.Abstractions;

public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEvent
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="domainEvent"></param>
    public DomainEventNotification(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
}
