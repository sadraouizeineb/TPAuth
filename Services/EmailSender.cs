using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
namespace tpAuth.Services
{


    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("No Reply", emailSettings["FromEmail"]));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = htmlMessage };

            using var client = new SmtpClient();
            await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), false);
            await client.AuthenticateAsync(emailSettings["SmtpUsername"], emailSettings["SmtpPassword"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }


        // Optionnellement, si tu veux garder une méthode synchrone
        //public void SendEmail(string email, string subject, string htmlMessage)
        //{
        //    var emailSettings = _configuration.GetSection("EmailSettings");

        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress("No Reply", emailSettings["FromEmail"]));
        //    message.To.Add(new MailboxAddress("", email));
        //    message.Subject = subject;
        //    message.Body = new TextPart("html") { Text = htmlMessage };

        //    using var client = new SmtpClient();
        //    client.Connect(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), false);
        //    client.Authenticate(emailSettings["SmtpUsername"], emailSettings["SmtpPassword"]);
        //    client.Send(message);
        //    client.Disconnect(true);
        //}
    }


}
