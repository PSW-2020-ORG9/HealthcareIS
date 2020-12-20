using System;
using System.Net;
using System.Net.Mail;
using HtmlAgilityPack;

namespace HealthcareBase.Service.UsersService.RegistrationService
{
    public class RegistrationNotifier:IRegistrationNotifier
    {
        private readonly SmtpClient SmtpClient;
        private static MailMessage MailMessage = new MailMessage
        {
            From = new MailAddress(Environment.GetEnvironmentVariable("PSW_EMAIL_USERNAME")),
            Subject = "Welcome to HealthCare Web!",
            IsBodyHtml = true
        };
        private readonly string _activationEndpoint;

        public RegistrationNotifier() : this(Environment.GetEnvironmentVariable("PSW_ACTIVATION_ENDPOINT")) {   }

        //TODO: Inject dynamicaly constructed activation endpoint string from web server
        public RegistrationNotifier(string activationEndpoint)
        {
            _activationEndpoint = activationEndpoint;
            SmtpClient = new SmtpClient(Environment.GetEnvironmentVariable("PSW_SMTP_HOST_SERVER"))
            {
                Port = Convert.ToInt32(Environment.GetEnvironmentVariable("PSW_SMTP_SSL_PORT")),
                Credentials = new NetworkCredential(Environment.GetEnvironmentVariable("PSW_EMAIL_USERNAME"), Environment.GetEnvironmentVariable("PSW_EMAIL_PASSWORD")),
                EnableSsl = true,
            };
        }
        
        public void SendActivationEmail(Guid guid, string patientEmail,string emailTemplatePath)
        {
            ConfigureEmailTemplate(guid, emailTemplatePath, patientEmail);
            SendEmail();
        }

        private void SendEmail()
        {
            SmtpClient.Send(MailMessage);
        }

        private void ConfigureEmailTemplate(Guid guid, string emailTemplatePath,string patientEmail)
        {
            var verificationHtml = new HtmlDocument();
            verificationHtml.Load(emailTemplatePath);
            verificationHtml.GetElementbyId("activationPath")
                .SetAttributeValue("href", _activationEndpoint + guid);
            MailMessage.Body = verificationHtml.DocumentNode.OuterHtml;
            MailMessage.To.Add(patientEmail);

        }
    }
}