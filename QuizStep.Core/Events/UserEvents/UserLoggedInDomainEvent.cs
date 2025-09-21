using System.Net;
using QuizStep.Core.Primitives;

namespace QuizStep.Core.Events.UserEvents;

public record UserLoggedInDomainEvent(IPAddress address) : DomainEvent;
