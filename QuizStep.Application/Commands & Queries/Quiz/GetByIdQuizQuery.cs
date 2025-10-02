using MediatR;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.Quiz
{
    public class GetByIdQuizQuery : IRequest<Result<QuizDto>>
    {
        public int Id { get; set; }
        public string? AccessCode { get; set; }
    }
}