using API.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendResetPasswordEmail(string email, string resetUrl)
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var fromEmail = _configuration["SendGrid:FromEmail"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, "Automatización TCU-629");
            var subject = "Recuperación de Contraseña";
            var to = new EmailAddress(email);
            var plainTextContent = $"Haga click aquí para reestablecer su contraseña: {resetUrl}";
            var htmlContent = $"<strong>Haga click aquí para reestablecer su contraseña:</strong> <a href='{resetUrl}'>Reestablecer Contraseña</a>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            // Log response details
            var statusCode = response.StatusCode;
            var body = await response.Body.ReadAsStringAsync();
            var headers = response.Headers;

            // Log the information or print it to the console
            Console.WriteLine($"Status Code: {statusCode}");
            Console.WriteLine($"Body: {body}");
            foreach (var header in headers)
            {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }
    }

}
