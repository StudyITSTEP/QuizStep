namespace QuizStep.Core.Primitives;

public class Result
{
    public bool Succeeded { get; }
    public bool Failed => !Succeeded;
    public Error Error { get;  } = Error.None;

    private Result(bool succeeded, Error error)
    {
        if (succeeded && error != Error.None
            || !succeeded && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }
        Succeeded = succeeded;
        Error = error;
    }
    
    public static Result Success() => new Result(true, Error.None);
    public static Result Failure(Error error) => new Result(false, error);
}