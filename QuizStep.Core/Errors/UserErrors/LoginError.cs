using QuizStep.Core.Primitives;

namespace QuizStep.Core.Errors.UserErrors;

public static class LoginError
{
    public static Error UserOrPassword = new Error(nameof(UserOrPassword), "User or password is incorrect");
    public static Error RefreshTokenFailure = new Error(nameof(RefreshTokenFailure));
    public static Error  EmailNotConfirmed = new Error(nameof(EmailNotConfirmed));

}