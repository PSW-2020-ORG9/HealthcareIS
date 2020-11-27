using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.CompilerServices;
using HtmlAgilityPack;

namespace HealthcareBase.Service.UsersService.RegistrationService
{
    public class RegistrationNotifier:IRegistrationNotifier
    {
        private readonly SmtpClient SmtpClient;
        private static MailMessage MailMessage;
        private const string ActivationEndpoint = "http://localhost:5000/patient/activate/";

        public RegistrationNotifier()
        {
            SmtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("psw.healthcareis.info@gmail.com", "healthcareis"),
                EnableSsl = true,
            };
            MailMessage = new MailMessage
            {
                From = new MailAddress("psw.healthcareis.info@gmail.com"),
                Subject = "Welcome to HealthCare Web!",
                IsBodyHtml = true
            };
        }
        
        public void SendActivationEmail(Guid patientGuid, string patientEmail,string emailTemplatePath)
        {
            ConfigureEmailTemplate(patientGuid,emailTemplatePath,patientEmail);
            SendEmail();
        }

        private void SendEmail()
        {
            SmtpClient.Send(MailMessage);
        }

        private static void ConfigureEmailTemplate(Guid patientGuid, string emailTemplatePath,string patientEmail)
        {
            var verificationHtml = new HtmlDocument();
            verificationHtml.Load(emailTemplatePath);
            verificationHtml.GetElementbyId("activationPath")
                .SetAttributeValue("href", ActivationEndpoint + patientGuid);
            MailMessage.Body = verificationHtml.DocumentNode.OuterHtml;
            MailMessage.To.Add(patientEmail);

        }
    }
}