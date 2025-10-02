using System.ComponentModel.DataAnnotations;
using MediatR;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Entities;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.Question;

public class CreateQuestionCommand: IRequest<Result<QuestionDto>>
{
    [Required]
    public string Text { get; set; } = null!;
    [Required]
    public int QuizId { get; set; }
    public List<Core.Entities.Answer> Answers { get; set; } = null!;
}