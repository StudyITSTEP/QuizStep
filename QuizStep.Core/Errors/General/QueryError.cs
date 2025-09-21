using QuizStep.Core.Primitives;

namespace QuizStep.Core.Errors.General;

public static class QueryError
{
    public static Error EntityNotExist = new Error(nameof(EntityNotExist));
}