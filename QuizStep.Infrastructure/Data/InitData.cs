using QuizStep.Core.Entities;

namespace QuizStep.Infrastructure.Data;

public static class InitData
{
    public static async Task Initialize(ApplicationContext context)
    {
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
                CreatorId = "76efc217-c4b6-48d0-871a-6571bebb1048", Name = "Quiz Step .NET", Description = "Quiz Step",
                CategoryId = 1
            },
            new Quiz()
            {
                CreatorId = "76efc217-c4b6-48d0-871a-6571bebb1048", Name = "Quiz Step React", Description = "Quiz Step",
                CategoryId = 2
            },
            new Quiz()
            {
                CreatorId = "76efc217-c4b6-48d0-871a-6571bebb1048", Name = "Quiz Step JS", Description = "Quiz Step",
                CategoryId = 2,
            },
            new Quiz()
            {
                CreatorId = "76efc217-c4b6-48d0-871a-6571bebb1048", Name = "Quiz Step Python", Description = "Quiz Step",
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