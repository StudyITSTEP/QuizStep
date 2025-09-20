using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuizStep.Core.Entities;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Infrastructure.Data;

public class ApplicationContext : IdentityDbContext<User>
{
    private readonly IPublisher _publisher;

    public ApplicationContext(DbContextOptions<ApplicationContext> options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var events = ChangeTracker.Entries<IEntity>()
            .Select(e => e.Entity).Where(e => e.GetEvents().Any()).ToList();
            

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var e in events)
        {
            await _publisher.Publish(e, cancellationToken);
        }

        return result;
    }
}