using QuizStep.Core.Primitives;

namespace QuizStep.Core.Interfaces;

public interface IEntity
{
    protected void Raise(DomainEvent e);
    public IEnumerable<DomainEvent> GetEvents();
}