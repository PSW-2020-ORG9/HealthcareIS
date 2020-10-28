// File:    PatientAccountService.cs
// Author:  Win 10
// Created: 27 May 2020 19:14:10
// Purpose: Definition of Class PatientAccountService

using Model.CustomExceptions;
using Model.Users.Employee;
using Model.Users.Patient;
using Model.Users.UserAccounts;
using Model.Users.UserFeedback;
using Repository.UsersRepository.UserAccountsRepository;
using Repository.UsersRepository.UserFeedbackRepository;
using System;
using System.Linq;

namespace Service.UsersService.PatientService
{
    public class PatientAccountService
    {
        private PatientAccountRepository patientAccountRepository;
        private PatientSurveyResponseRepository patientSurveyResponseRepository;

        public PatientAccountService(PatientAccountRepository patientAccountRepository, 
            PatientSurveyResponseRepository patientSurveyResponseRepository)
        {
            this.patientAccountRepository = patientAccountRepository;
            this.patientSurveyResponseRepository = patientSurveyResponseRepository;
        }

        public void DeleteAccount(PatientAccount patientAccount)
        {
            patientAccountRepository.Delete(patientAccount);
        }

        public PatientAccount GetAccount(Patient patient)
        {
            return patientAccountRepository.GetByPatient(patient);
        }

        public PatientAccount ChangePassword(PatientAccount account, String newPassword)
        {
            if (newPassword == null)
                throw new BadRequestException();
            if (newPassword.Trim().Equals(""))
                throw new BadRequestException();

            var acc = patientAccountRepository.GetByID(account.Id);
            acc.Password = newPassword;

            return patientAccountRepository.Update(acc);
        }

        public PatientAccount AddFavouriteDoctor(Doctor doctor, PatientAccount account)
        {
            var acc = patientAccountRepository.GetByID(account.Id);
            acc.AddFavouriteDoctor(doctor);

            return patientAccountRepository.Update(acc);
        }

        public PatientAccount RemoveFavoriteDoctor(Doctor doctor, PatientAccount account)
        {
            var acc = patientAccountRepository.GetByID(account.Id);
            acc.RemoveFavouriteDoctor(doctor);

            return patientAccountRepository.Update(acc);
        }

        public void RecordSurveyResponse(PatientSurveyResponse surveyResponse)
        {
            patientSurveyResponseRepository.Create(surveyResponse);
        }

    }
}