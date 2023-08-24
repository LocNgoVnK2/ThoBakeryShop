using Infrastructure.Entities;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
namespace Infrastructure.Service
{
    public interface IEmailService
    {

        void SendEmail(EmailMessage emailMessage);
    }
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailService(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public void SendEmail(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }
        private MimeMessage CreateEmailMessage(EmailMessage emailMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Zalo Clone", _emailConfig.From));
            message.To.AddRange(emailMessage.To);
            message.Subject = emailMessage.Subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailMessage.Content };

            return message;
        }
        private void Send(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.Username, _emailConfig.Password);
                client.Send(message);

            }
        }
    }
}
