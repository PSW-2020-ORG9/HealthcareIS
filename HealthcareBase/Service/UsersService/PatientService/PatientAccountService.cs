// File:    PatientAccountService.cs
// Author:  Win 10
// Created: 27 May 2020 19:14:10
// Purpose: Definition of Class PatientAccountService

using System.Linq;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Model.Users.UserFeedback;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.UserAccountsRepository;
using HealthcareBase.Repository.UsersRepository.UserFeedbackRepository;

namespace HealthcareBase.Service.UsersService.PatientService
{
    public class PatientAccountService
    {
        private readonly RepositoryWrapper<IPatientAccountRepository> patientAccountRepository;

        public PatientAccountService(
            IPatientAccountRepository patientAccountRepository)
        {
            this.patientAccountRepository = new RepositoryWrapper<IPatientAccountRepository>(patientAccountRepository);
        }

        public void DeleteAccount(PatientAccount patientAccount)
        {
            patientAccountRepository.Repository.Delete(patientAccount);
        }

        public PatientAccount GetAccount(Patient patient)
        {
            return patientAccountRepository.Repository.GetByPatient(patient);
        }

        public PatientAccount ChangePassword(PatientAccount account, string newPassword)
        {
            if (newPassword == null)
                throw new BadRequestException();
            if (newPassword.Trim().Equals(""))
                throw new BadRequestException();

            var acc = patientAccountRepository.Repository.GetByID(account.Id);
            acc.Password = newPassword;

            return patientAccountRepository.Repository.Update(acc);
        }

        // TODO When fetching FavoriteDoctors, do no
        public PatientAccount AddFavouriteDoctor(Doctor doctor, PatientAccount account)
        {
            var acc = patientAccountRepository.Repository.GetByID(account.Id);
            acc.FavouriteDoctors.ToList().Add(new FavoriteDoctor()
            {
                Doctor = doctor,
            });
            return patientAccountRepository.Repository.Update(acc);
        }

        public PatientAccount RemoveFavoriteDoctor(Doctor doctor, PatientAccount account)
        {
            var acc = patientAccountRepository.Repository.GetByID(account.Id);
            var favDoctorList = acc.FavouriteDoctors.ToList();
            foreach (var favDoc in favDoctorList)
            {
                if (favDoc.Doctor.Id == doctor.Id)
                {
                    favDoctorList.Remove(favDoc);
                    break;
                }
            }
            return patientAccountRepository.Repository.Update(acc);
        }
        
    }
}