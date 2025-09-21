using QuizStep.Core.Entities;
using QuizStep.Core.Enums;

namespace QuizStep.Application.DTOs.Quiz
{
    public class QuizDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int CreatorId { get; set; }
        public QuizAccess QuizAccess { get; set; }
    }
}
