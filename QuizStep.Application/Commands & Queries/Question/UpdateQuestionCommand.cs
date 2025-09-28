using System.ComponentModel.DataAnnotations;
using MediatR;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Entities;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.Question;

public class UpdateQuestionCommand: IRequest<Result<QuestionDto>>
{
    public int? Id { get; set; }
    [Required]
    public string Text { get; set; } = null!;
    [Required]
    public List<Core.Entities.Answer> Answers { get; set; } = null!;
}