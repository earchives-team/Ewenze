using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Application.EMailManagement
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendPasswordResetEmailAsync(string toEmail, string resetLink);
        Task SendEmailConfirmationAsync(string toEmail, string confirmationLink);
        Task SendWelcomeEmailAsync(string toEmail, string UserName);
    }
}
