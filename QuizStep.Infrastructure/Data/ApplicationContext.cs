using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuizStep.Application.Interfaces;
using QuizStep.Core.Entities;
using QuizStep.Core.Primitives;

namespace QuizStep.Infrastructure.Data;

public class ApplicationContext : IdentityDbContext<User>, IApplicationDbContext
{
    private readonly IPublisher _publisher;

    public ApplicationContext(DbContextOptions<ApplicationContext> options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    public DbSet<Test> Tests => Set<Test>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<User> Users => Set<User>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var events = ChangeTracker.Entries<Entity>()
            .Select(e => e.Entity)
            .Where(e => e.Events.Any())
            .SelectMany(e => e.Events);

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var e in events)
        {
            await _publisher.Publish(e, cancellationToken);
        }

        return result;
    }
}