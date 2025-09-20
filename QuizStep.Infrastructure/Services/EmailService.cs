using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using MimeKit;
using QuizStep.Infrastructure.Config;

namespace QuizStep.Infrastructure.Repositories;

public class EmailService : IEmailSender
{
    private readonly EmailConfig _config;
    private readonly ILogger<EmailService> _logger;

    public EmailService(EmailConfig config,  ILogger<EmailService> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("QuizStep", _config.From));
        message.To.Add(new MailboxAddress(name: email, address: email));
        message.Subject = subject;
        message.Body = new TextPart("html")
        {
            Text = htmlMessage,
        };
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_config.SmtpServer, 587, false);
            await client.AuthenticateAsync(_config.UserName, _config.Password);

            try
            {
                await client.SendAsync(message);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }

        return;
    }
}