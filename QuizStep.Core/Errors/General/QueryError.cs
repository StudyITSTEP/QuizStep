using QuizStep.Core.Primitives;

namespace QuizStep.Core.Errors.General;

public class QueryError
{
    public static Error EntityNotExist = new Error("EntityNotExist");
}