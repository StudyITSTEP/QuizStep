using MediatR;
using QuizStep.Core.Events.UserEvents;

namespace QuizStep.Application.DomainEventHandlers;

public class UserDomainEventsHandlers : INotificationHandler<CreateUserDomainEvent>
{
    public Task Handle(CreateUserDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}