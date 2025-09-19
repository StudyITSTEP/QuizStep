using QuizStep.Core.Interfaces;

namespace QuizStep.Core.Primitives;

public abstract class Entity: IEntity
{
    private readonly List<DomainEvent> _events = new List<DomainEvent>();
    
    public ICollection<DomainEvent> Events => _events;

    public void Raise(DomainEvent e)
    {
        _events.Add(e);
    }
}