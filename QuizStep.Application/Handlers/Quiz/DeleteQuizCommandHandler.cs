using MediatR;
using QuizStep.Application.Commands___Queries.Quiz;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Errors.General;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.Quiz
{
    public class DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand, Result>
    {
        private readonly IQuizProvider _quizProviderRepo;

        public DeleteQuizCommandHandler(IQuizProvider quizProviderRepo)
        {
            _quizProviderRepo = quizProviderRepo;
        }

        public async Task<Result> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
        {
            var test = await _quizProviderRepo.GetByIdAsync(request.Id, cancellationToken);

            if (test == null)
                return QueryError.EntityNotExist;

            await _quizProviderRepo.DeleteAsync(test, cancellationToken);
            
            return Result.Success();
        }
    }

}
