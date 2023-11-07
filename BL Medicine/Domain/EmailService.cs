using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BL_Medicine.Domain
{
    public class EmailService
    {
        private readonly string smtpServer;
        private readonly int smtpPort;
        private readonly string smtpUsername; // Your Outlook email address
        private readonly string smtpPassword; // Your Outlook email password

        public EmailService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
        {
            this.smtpServer = smtpServer; // For Outlook, use "smtp-mail.outlook.com"
            this.smtpPort = smtpPort; // For Outlook, use 587 for TLS or 25 for non-secure connections
            this.smtpUsername = smtpUsername;
            this.smtpPassword = smtpPassword;
        }

        public async Task SendEmail( string recipient, string subject, string body )
        {
            var fromAddress = new MailAddress ( smtpUsername );
            var toAddress = new MailAddress ( recipient );

            using (var smtpClient = new SmtpClient
            {
                Host = smtpServer,
                Port = smtpPort,
                Credentials = new NetworkCredential ( smtpUsername, smtpPassword ),
                EnableSsl = true, // Use SSL for secure connections
            })
            using (var message = new MailMessage ( fromAddress, toAddress )
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            })
            {
                await smtpClient.SendMailAsync ( message );
            }
        }

        public async Task SendPasswordResetEmail( string email, string resetToken )
        {
                // Generate the email subject and body.
            string subject = "Password Reset";
            string body = $"Dear User,<br><br>" +
                $"You've requested a password reset for your MediApp account.<br>" +
                $"To reset your password, click here: {GetResetPasswordLink ( resetToken )}<br>" +
                $"If you didn't request this, please disregard this email.<br><br>" +
                $"Best regards,<br>" +
                $"MediApp Team";


            // Send the email.
            await SendEmail ( email, subject, body );

        }

        public string GetResetPasswordLink( string resetToken, string baseUrl = "http://localhost:5183/" )
        {

            // Assuming you have a controller action for password reset named "ResetPassword"
            // and it takes a "token" parameter, you can construct the link like this:
            string resetLink = $"{baseUrl}/User/ResetPassword?token={resetToken}";

            return resetLink;
        }
    }
}
