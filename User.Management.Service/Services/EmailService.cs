using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MailKit.Net.Smtp;

using System.Text;
using System.Threading.Tasks;
using User.Management.Service.Models;
using User.Management.Service.Constants;

namespace User.Management.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailService(EmailConfiguration emailConfig) => _emailConfig = emailConfig;
        public string SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
            var recipients = string.Join(", ", message.To);
            return ResponseMessages.GetEmailSuccessMessage(recipients);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            //var emailMessage = new MimeMessage();
            //emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            //emailMessage.To.AddRange(message.To);
            //emailMessage.Subject = message.Subject;
            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };


            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Support Team", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            // Simple HTML content
            string htmlContent = $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; color: #333; }}
                        .container {{ max-width: 600px; margin: auto; padding: 20px; background-color: #f9f9f9; border-radius: 8px; }}
                        .header {{ font-size: 20px; color: #4A90E2; font-weight: bold; }}
                        .content {{ margin-top: 20px; font-size: 16px; }}
                        .footer {{ margin-top: 30px; font-size: 12px; color: #888; text-align: center; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>{message.Subject}</div>
                        <div class='content'>
                            <p>Hello,</p>
                            <p>{message.Content}</p>
                            <p>Thank you,<br>Your Support Team</p>
                        </div>
                        <div class='footer'>
                            &copy; {DateTime.Now.Year} Your Company
                        </div>
                    </div>
                </body>
                </html>";

            // Set the HTML body
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlContent };


            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                client.Send(mailMessage);
            }
            catch
            {
                //log an error message or throw an exception or both.
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }

}
