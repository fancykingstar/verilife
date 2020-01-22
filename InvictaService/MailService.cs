using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
namespace InvictaService
{
    public class MailService
    {
        private IConfiguration _configuration;
        public MailService(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        public void SendEmail(string toEmail, string subject, string message)
        {

            var fromName = _configuration["MAIL:fromName"];
            var fromEmail = _configuration["MAIL:fromEmail"];
            var smtp = _configuration["MAIL:smtp"];
            var smtpLogin = _configuration["MAIL:smtpLogin"];
            var smtpPass = _configuration["MAIL:smtpPass"];

            var emailMessage = new MimeKit.MimeMessage();

            emailMessage.From.Add(new MailboxAddress(fromName, fromEmail));
            emailMessage.To.Add(new MailboxAddress(toEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect(smtp, 587, false);
                client.Authenticate(smtpLogin, smtpPass);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }

        public void LogEmail(string subject, string message)
        {

            var fromName = _configuration["MAIL:fromName"];
            var fromEmail = _configuration["MAIL:fromEmail"];
            var smtp = _configuration["MAIL:smtp"];
            var smtpLogin = _configuration["MAIL:smtpLogin"];
            var smtpPass = _configuration["MAIL:smtpPass"];
            var toEmail = _configuration["MAIL:logMail"];
            var emailMessage = new MimeKit.MimeMessage();

            emailMessage.From.Add(new MailboxAddress(fromName, fromEmail));
            emailMessage.To.Add(new MailboxAddress("Log Mail", toEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect(smtp, 587, false);
                client.Authenticate(smtpLogin, smtpPass);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
