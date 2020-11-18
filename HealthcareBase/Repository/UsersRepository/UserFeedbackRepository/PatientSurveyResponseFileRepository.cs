// File:    PatientSurveyResponseFileRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:51:37
// Purpose: Definition of Class PatientSurveyResponseFileRepository

using System;
using HealthcareBase.Model.Users.UserFeedback;
using HealthcareBase.Model.Users.UserFeedback.Survey;
using Model.CustomExceptions;
using Model.Users.UserFeedback;
using Model.Utilities;
using Repository.Generics;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using Repository.UsersRepository.UserAccountsRepository;

namespace Repository.UsersRepository.UserFeedbackRepository
{
    public class PatientSurveyResponseFileRepository : GenericFileRepository<Survey, int>,
        PatientSurveyResponseRepository
    {
        private readonly DoctorRepository doctorRepository;
        private readonly IntegerKeyGenerator keyGenerator;
        private readonly PatientAccountRepository patientAccountRepository;

        public PatientSurveyResponseFileRepository(DoctorRepository doctorRepository,
            PatientAccountRepository patientAccountRepository, string filePath) : base(filePath)
        {
            this.doctorRepository = doctorRepository;
            this.patientAccountRepository = patientAccountRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        protected override int GenerateKey(Survey entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override Survey ParseEntity(Survey entity)
        {
            //DOTO
            throw new NotImplementedException();
        }
    }
}