using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizStep.Core.Entities;

namespace QuizStep.Infrastructure.Data;

public class ApplicationContext: IdentityDbContext<User>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }
}