using Microsoft.AspNetCore.Identity.UI.Services;

namespace QuizStep.Infrastructure.Repositories;

public class FakeEmailService: IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        Console.WriteLine(htmlMessage);
        return Task.CompletedTask;
    }
}