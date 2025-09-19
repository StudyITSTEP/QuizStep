using QuizStep.Core.Primitives;

namespace QuizStep.Core.Events.UserEvents;

public record CreateUserDomainEvent(string UserId): DomainEvent
{
    
}