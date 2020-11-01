// File:    UserFeedbackRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:51:37
// Purpose: Definition of Interface UserFeedbackRepository

using Model.Users.UserFeedback;
using Repository.Generics;

namespace Repository.UsersRepository.UserFeedbackRepository
{
    public interface UserFeedbackRepository : IWrappableRepository<UserFeedback, int>
    {
    }
}