
using Ewenze.Application.EMailManagement;
using Ewenze.Infrastructure.Persistence.Email;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

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
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            email.Body = new TextPart(TextFormat.Html)
            {
                Text = htmlBody
            };

            using var smtp = new SmtpClient();

            try
            {
                await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, SecureSocketOptions.StartTls);

                await smtp.AuthenticateAsync(_settings.Username, _settings.Password);

                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de l'envoi de l'email : {ex.Message}", ex);
            }
            finally
            {
                await smtp.DisconnectAsync(true);
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
