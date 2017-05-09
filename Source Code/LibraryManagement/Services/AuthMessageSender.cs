using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class AuthMessageSender : IEmailSender
    {
        public AuthMessageSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }
        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager
        public Task SendEmail(string email, string subject, string htmlmessage, string txtmessage)
        {
            // Plug in your email service here to send an email.
            Execute(Options.SendGridKey, subject, htmlmessage, txtmessage, email).Wait();
            return Task.FromResult(0);
        }
        public async Task Execute(string apiKey, string subject, string htmlmessage, string txtmessage, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("support.books.holycode.io@gmail.com", "Anh B."),
                Subject = subject,
                PlainTextContent = txtmessage,
                HtmlContent = htmlmessage
            };
            msg.AddTo(new EmailAddress(email));
            var response = await client.SendEmailAsync(msg);
        }
    }

}
