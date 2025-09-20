using Microsoft.AspNetCore.Identity;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Core.Entities;

public class User: IdentityUser, IEntity
{
    [NonSerialized]
    private readonly List<DomainEvent> _events = new List<DomainEvent>();
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public void Raise(DomainEvent e)
    {
        _events.Add(e);
    }

    public IEnumerable<DomainEvent> GetEvents() => _events;
}