using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using QuizStep.Core.Events.UserEvents;

namespace QuizStep.Application.DomainEventHandlers.UserDomainEventHandlers;

public class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}