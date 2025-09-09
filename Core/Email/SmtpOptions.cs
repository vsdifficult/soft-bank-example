namespace SoftBank.Core.Email;

public class SmtpOptions
{
    public string Host { get; set; } = string.Empty;

    public int Port { get; set; }

    public bool EnableSSL { get; set; }

    public string User { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    
    public string From { get; set; } = string.Empty;
}