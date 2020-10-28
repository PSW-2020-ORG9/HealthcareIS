// File:    PatientSurveyResponseFileRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:51:37
// Purpose: Definition of Class PatientSurveyResponseFileRepository

using Model.CustomExceptions;
using Model.Users.UserFeedback;
using Model.Utilities;
using Repository.Generics;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using Repository.UsersRepository.UserAccountsRepository;
using System;

namespace Repository.UsersRepository.UserFeedbackRepository
{
    public class PatientSurveyResponseFileRepository : GenericFileRepository<PatientSurveyResponse, int>, PatientSurveyResponseRepository
    {
        private DoctorRepository doctorRepository;
        private PatientAccountRepository patientAccountRepository;
        private IntegerKeyGenerator keyGenerator;

        public PatientSurveyResponseFileRepository(DoctorRepository doctorRepository, 
            PatientAccountRepository patientAccountRepository, String filePath) : base(filePath)
         {
            this.doctorRepository = doctorRepository;
            this.patientAccountRepository = patientAccountRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        protected override int GenerateKey(PatientSurveyResponse entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override PatientSurveyResponse ParseEntity(PatientSurveyResponse entity)
        {
            try
            {
                if (entity.BestDoctor != null)
                    entity.BestDoctor = doctorRepository.GetByID(entity.BestDoctor.GetKey());
                if (entity.Patient != null)
                    entity.Patient = patientAccountRepository.GetByID(entity.Patient.GetKey());
            }
            catch (BadRequestException)
            {
                throw new ValidationException();
            }

            return entity;
        }
    }
}