using MediatR;

namespace QuizStep.Core.Primitives;

public record DomainEvent: INotification
{
    public Guid Id { get; set; } =  Guid.NewGuid();
}