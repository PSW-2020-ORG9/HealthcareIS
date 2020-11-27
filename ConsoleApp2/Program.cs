using System;
using HealthcareBase.Service.UsersService.RegistrationService;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            RegistrationNotifier notif = new RegistrationNotifier();
            notif.SendActivationEmail(new Guid(),"kaitwin41@gmail.com","/home/vakslen/faks/psw/projekat/HealthcareIS/HospitalWebApp/Resources/verification-mail.html" );
        }
    }
}