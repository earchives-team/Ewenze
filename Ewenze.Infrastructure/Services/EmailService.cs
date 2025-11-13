
using Ewenze.Application.EMailManagement;
using Ewenze.Infrastructure.Persistence.Email;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Ewenze.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
        {
            try
            {
                using var message = new MailMessage();
                message.From = new MailAddress(_settings.SenderEmail, _settings.SenderName);
                message.To.Add(new MailAddress(toEmail));
                message.Subject = subject;
                message.Body = htmlBody;
                message.IsBodyHtml = true;

                using var client = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort);
                client.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
                client.EnableSsl = _settings.EnableSsl;

                await client.SendMailAsync(message);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task SendEmailConfirmationAsync(string toEmail, string confirmationLink)
        {
           throw new NotImplementedException();
        }

        public async Task SendPasswordResetEmailAsync(string toEmail, string resetLink)
        {
            var subject = "Réinitialisation de votre mot de passe";
            var body = $@"
                <h2>Réinitialisation de mot de passe</h2>
                <p>Vous avez demandé la réinitialisation de votre mot de passe.</p>
                <p>Cliquez sur le lien ci-dessous pour réinitialiser votre mot de passe :</p>
                <a href='{resetLink}'>Réinitialiser mon mot de passe</a>
                <p>Ce lien expire dans 15 Minute.</p>
                <p>Si vous n'avez pas demandé cette réinitialisation, ignorez cet email.</p>
            ";

            await SendEmailAsync(toEmail, subject, body);
        }

        public Task SendWelcomeEmailAsync(string toEmail, string UserName)
        {
            throw new NotImplementedException();
        }
    }
}
