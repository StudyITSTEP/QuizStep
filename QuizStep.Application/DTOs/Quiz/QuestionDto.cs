using System.ComponentModel.DataAnnotations;
using QuizStep.Core.Entities;

namespace QuizStep.Application.DTOs.Quiz;

public class QuestionDto
{
    public int? Id { get; set; }
    [Required]
    public string Text { get; set; } = null!;
    [Required]
    public int QuizId { get; set; }
    public List<Answer> Answers { get; set; } = null!;
}