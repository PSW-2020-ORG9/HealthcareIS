// File:    UserFeedbackService.cs
// Author:  Lana
// Created: 28 May 2020 17:09:20
// Purpose: Definition of Class UserFeedbackService

using Model.Users.UserFeedback;
using Repository.UsersRepository.UserFeedbackRepository;
using System;
using System.Collections.Generic;

namespace Service.UsersService.UserFeedbackService
{
    public class UserFeedbackService
    {
        private UserFeedbackRepository userFeedbackRepository;

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