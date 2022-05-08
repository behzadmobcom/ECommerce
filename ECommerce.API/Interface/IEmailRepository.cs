using System.Threading;
using System.Threading.Tasks;

namespace API.Interface
{
    public interface IEmailRepository
    {
        public Task SendEmailAsync(string toEmail, string subject, string message, CancellationToken cancellationToken, bool isMessageHtml = false);
    }
}
