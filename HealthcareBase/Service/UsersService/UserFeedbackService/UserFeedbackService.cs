// File:    UserFeedbackService.cs
// Author:  Lana
// Created: 28 May 2020 17:09:20
// Purpose: Definition of Class UserFeedbackService

using System.Collections.Generic;
using Model.Users.UserFeedback;
using Repository.UsersRepository.UserFeedbackRepository;

namespace Service.UsersService.UserFeedbackService
{
    public class UserFeedbackService
    {
        private readonly UserFeedbackRepository userFeedbackRepository;

        public UserFeedbackService(UserFeedbackRepository userFeedbackRepository)
        {
            this.userFeedbackRepository = userFeedbackRepository;
        }

        public IEnumerable<UserFeedback> GetAll()
        {
            return userFeedbackRepository.GetAll();
        }

        public UserFeedback Create(UserFeedback userFeedback)
        {
            return userFeedbackRepository.Create(userFeedback);
        }
    }
}