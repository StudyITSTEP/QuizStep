using Microsoft.AspNetCore.Identity;
using QuizStep.Core.Entities;

namespace QuizStep.Infrastructure.Data;

public static class InitData
{
    public static async Task Initialize(ApplicationContext context, UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        var user = new User()
        {
            Email = "admin@gmail.com",
            EmailConfirmed = true,
            UserName = "admin@gmail.com",
            FirstName = "Admin",
            LastName = "Admin",
        };
        
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("User"));
            await roleManager.CreateAsync(new IdentityRole("Moderator"));
        }

        if (!userManager.Users.Any())
        {
            await userManager.CreateAsync(user, "Aa12345!");
            await userManager.AddToRoleAsync(user, "Admin");
        }

        var categories = new List<Category>
        {
            new Category() { Name = ".NET" },
            new Category() { Name = "React" },
            new Category() { Name = "Python" }
        };

        var quizez = new List<Quiz>
        {
            new Quiz()
            {
                CreatorId = user.Id, Name = "Quiz Step .NET", Description = "Quiz Step",
                CategoryId = 1
            },
            new Quiz()
            {
                CreatorId = user.Id, Name = "Quiz Step React", Description = "Quiz Step",
                CategoryId = 2
            },
            new Quiz()
            {
                CreatorId = user.Id, Name = "Quiz Step JS", Description = "Quiz Step",
                CategoryId = 2,
            },
            new Quiz()
            {
                CreatorId = user.Id, Name = "Quiz Step Python",
                Description = "Quiz Step",
                CategoryId = 3,
            },
        };

       
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }

        if (!context.Quizzes.Any())
        {
            context.Quizzes.AddRange(quizez);
            await context.SaveChangesAsync();
        }
    }
}