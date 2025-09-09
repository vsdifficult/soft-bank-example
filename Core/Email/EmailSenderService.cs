using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace SoftBank.Core.Email;

public class EmailSenderService
{
    private readonly SmtpOptions _options;
    public EmailSenderService(IOptions<SmtpOptions> options)
    {
        _options = options.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using var client = new SmtpClient(_options.Host, _options.Port)
        {
            EnableSsl = _options.EnableSSL,
            Credentials = new NetworkCredential(_options.User, _options.Password)
        };

        var mail = new MailMessage(_options.From, to, subject, body);

        await client.SendMailAsync(mail);
    }
}
