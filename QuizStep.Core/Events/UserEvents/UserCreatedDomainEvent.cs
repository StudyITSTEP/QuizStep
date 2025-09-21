using QuizStep.Core.Primitives;

namespace QuizStep.Core.Events.UserEvents;

public record UserCreatedDomainEvent(string ConfirmationLink, string Email): DomainEvent
{
    
}