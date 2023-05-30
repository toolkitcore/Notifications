namespace Notifications.Application.Common.Interfaces;

public interface IMassTransitService
{
    void SendMessage<T>(T message);
}