using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuizStep.Application.Interfaces;
using QuizStep.Core.Entities;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Infrastructure.Data;

public class ApplicationContext : IdentityDbContext<User>
{
    private readonly IPublisher _publisher;

    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    public DbSet<Quiz> Quizzes => Set<Quiz>();
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuizResult> QuizResults { get; set; }
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<User> Users => Set<User>();
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        base.OnModelCreating(modelBuilder);
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var events = ChangeTracker.Entries<IEntity>()
            .Select(e => e.Entity).Where(e => e.GetEvents().Any()).ToList();


        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var e in events)
        { 
            _publisher.Publish(e, cancellationToken);
        }

        return result;
    }
}