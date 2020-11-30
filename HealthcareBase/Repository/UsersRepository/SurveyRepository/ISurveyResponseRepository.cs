// File:    PatientSurveyResponseRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:51:37
// Purpose: Definition of Interface PatientSurveyResponseRepository

using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.UsersRepository.SurveyRepository
{
    public interface ISurveyResponseRepository : IWrappableRepository<SurveyResponse, int>
    {
    }
}