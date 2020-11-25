// File:    PatientAccountService.cs
// Author:  Win 10
// Created: 27 May 2020 19:14:10
// Purpose: Definition of Class PatientAccountService

using System.Linq;
using Model.CustomExceptions;
using Model.Users.Employee;
using Model.Users.Patient;
using Model.Users.UserAccounts;
using Model.Users.UserFeedback;
using Repository.Generics;
using Repository.UsersRepository.UserAccountsRepository;
using Repository.UsersRepository.UserFeedbackRepository;

namespace Service.UsersService.PatientService
{
    public class PatientAccountService
    {
        private readonly RepositoryWrapper<PatientAccountRepository> patientAccountRepository;
        private readonly RepositoryWrapper<PatientSurveyResponseRepository> patientSurveyResponseRepository;

        public PatientAccountService(
            PatientAccountRepository patientAccountRepository,
            PatientSurveyResponseRepository patientSurveyResponseRepository)
        {
            this.patientAccountRepository = new RepositoryWrapper<PatientAccountRepository>(patientAccountRepository);
            this.patientSurveyResponseRepository =
                new RepositoryWrapper<PatientSurveyResponseRepository>(patientSurveyResponseRepository);
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

        public void RecordSurveyResponse(PatientSurveyResponse surveyResponse)
        {
            patientSurveyResponseRepository.Repository.Create(surveyResponse);
        }
    }
}