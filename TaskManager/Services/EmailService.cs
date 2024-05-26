using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using TaskManager.Abstractions.Services;
using TaskManager.Options;

namespace TaskManager.Services
{
    public class EmailService(IOptionsMonitor<EmailOptions> emailOptions): IEmailService
    {
        private readonly EmailOptions _emailOptions = emailOptions.CurrentValue;

        public async Task SendEmailAsync(string emailTo, string message)
        {
            using var smtp = new SmtpClient();
            var mailMessage = CreateMailMessage(emailTo, message);

            try
            {
                await smtp.ConnectAsync(_emailOptions.Host, _emailOptions.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_emailOptions.MailFrom, _emailOptions.Password);

                await smtp.SendAsync(mailMessage);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private MimeMessage CreateMailMessage(string emailTo, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.Sender = MailboxAddress.Parse(_emailOptions.MailFrom);
            emailMessage.To.Add(MailboxAddress.Parse(emailTo));

            emailMessage.Subject = "Identity study project";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };

            return emailMessage;
        }
    }
}
