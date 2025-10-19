using System.Net;
using System.Net.Mail;

namespace MRTracking.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var portString = _configuration["EmailSettings:Port"];
                var senderEmail = _configuration["EmailSettings:SenderEmail"];
                var senderName = _configuration["EmailSettings:SenderName"];
                var username = _configuration["EmailSettings:Username"];
                var password = _configuration["EmailSettings:Password"];
                var enableSslString = _configuration["EmailSettings:EnableSSL"];

                // Validate configuration
                if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(portString) || 
                    string.IsNullOrEmpty(senderEmail) || string.IsNullOrEmpty(username) || 
                    string.IsNullOrEmpty(password))
                {
                    _logger.LogError("Email configuration is incomplete. Please check appsettings.json");
                    throw new InvalidOperationException("Email configuration is incomplete");
                }

                int port = int.Parse(portString);
                bool enableSsl = bool.Parse(enableSslString ?? "true");

                using var smtpClient = new SmtpClient(smtpServer, port)
                {
                    Credentials = new NetworkCredential(username, password),
                    EnableSsl = enableSsl
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail, senderName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation($"Email sent successfully to {toEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send email to {toEmail}");
                throw;
            }
        }
    }
}

