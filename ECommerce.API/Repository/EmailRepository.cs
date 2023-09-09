using System.Net;
using System.Net.Mail;
using ECommerce.API.Interface;

namespace ECommerce.API.Repository;

public class EmailRepository : IEmailRepository
{
    public Task SendEmailAsync(string toEmail, string subject, string message, CancellationToken cancellationToken,
        bool isMessageHtml = false)
    {
        using (var client = new SmtpClient())
        {
            var credentials = new NetworkCredential
            {
                UserName = "info", // without @gmail.com
                Password = "5kDuerZUT2MlpI"
            };

            client.Credentials = credentials;
            client.Host = "webmail.boloorico.com";
            client.Port = 587;
            client.EnableSsl = false;

            using var emailMessage = new MailMessage
            {
                To = {new MailAddress(toEmail)},
                From = new MailAddress("info@boloorico.com"), // with @gmail.com
                Subject = subject,
                Body = message,
                IsBodyHtml = isMessageHtml
            };

            client.Send(emailMessage);
        }

        return Task.CompletedTask;
    }
}