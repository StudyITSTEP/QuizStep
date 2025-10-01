using MediatR;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.Quiz
{
    public class CreateQuizCommand : IRequest<Result<QuizDto>>
    {
        public QuizDto Quiz { get; set; } = null!;
    }
}
