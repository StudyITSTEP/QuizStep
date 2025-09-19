using Microsoft.EntityFrameworkCore;
using QuizStep.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Test> Tests { get; }
        DbSet<Category> Categories { get; }
        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
