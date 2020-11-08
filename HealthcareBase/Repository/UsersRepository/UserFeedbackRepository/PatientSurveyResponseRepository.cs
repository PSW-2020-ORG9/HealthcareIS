// File:    PatientSurveyResponseRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:51:37
// Purpose: Definition of Interface PatientSurveyResponseRepository

using Model.Users.UserFeedback;
using Repository.Generics;

namespace Repository.UsersRepository.UserFeedbackRepository
{
    public interface PatientSurveyResponseRepository : IWrappableRepository<PatientSurveyResponse, int>
    {
    }
}