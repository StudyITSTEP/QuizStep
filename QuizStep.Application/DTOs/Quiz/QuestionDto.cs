using System.ComponentModel.DataAnnotations;
using QuizStep.Core.Entities;

namespace QuizStep.Application.DTOs.Quiz;

public class QuestionDto
{
    public int? Id { get; set; }
    [Required]
    public string Text { get; set; } = null!;
    public List<AnswerDto> Answers { get; set; } = null!;
    public int CorrectAnswerIndex { get; set; }
}