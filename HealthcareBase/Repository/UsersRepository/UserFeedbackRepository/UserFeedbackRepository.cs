// File:    UserFeedbackRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:51:37
// Purpose: Definition of Interface UserFeedbackRepository

using HealthcareBase.Model.Users.UserFeedback;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.UserFeedbackRepository
{
    public interface UserFeedbackRepository : IWrappableRepository<UserFeedback, int>
    {
    }
}