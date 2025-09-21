namespace QuizStep.Core.Primitives;

public class Result
{
    public bool Succeeded { get; }
    public bool Failed => !Succeeded;
    public Error? Error { get;  } = null;

    private Result(bool succeeded, Error? error)
    {
        if (succeeded && error != null
            || !succeeded && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }
        Succeeded = succeeded;
        Error = error;
    }
    
    public static Result Success() => new Result(true, null);
    public static Result Failure(Error error) => new Result(false, error);
    public static implicit operator bool(Result result) => result.Succeeded;
}