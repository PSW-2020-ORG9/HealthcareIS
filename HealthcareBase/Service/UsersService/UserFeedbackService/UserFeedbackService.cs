// File:    UserFeedbackService.cs
// Author:  Lana
// Created: 28 May 2020 17:09:20
// Purpose: Definition of Class UserFeedbackService

using System.Collections.Generic;
using Model.Users.UserFeedback;
using Repository.Generics;
using Repository.UsersRepository.UserFeedbackRepository;

namespace Service.UsersService.UserFeedbackService
{
    public class UserFeedbackService
    {
        private readonly RepositoryWrapper<UserFeedbackRepository> userFeedbackRepository;

        public UserFeedbackService(UserFeedbackRepository repository)
        {
            userFeedbackRepository = new RepositoryWrapper<UserFeedbackRepository>(repository);
        }

        public IEnumerable<UserFeedback> GetAll()
        {
            return userFeedbackRepository.Repository.GetAll();
        }

        public UserFeedback Create(UserFeedback userFeedback)
        {
            return userFeedbackRepository.Repository.Create(userFeedback);
        }

        public UserFeedback Update(UserFeedback userFeedback)
        {
            return userFeedbackRepository.Repository.Update(userFeedback);
        }

        /// <summary>
        /// Publishes a UserFeedback with a given id.
        /// </summary>
        /// <param name="id">Id of the <see cref="UserFeedback"/> to be published.</param>
        /// <returns>A <see cref="bool"/> value indicating the operation success.</returns>
        public bool Publish(int id)
        {
            UserFeedback userFeedback = userFeedbackRepository.Repository.GetByID(id);
            if (!userFeedback.isPublic) return false;
            userFeedback.isPublished = true;
            userFeedbackRepository.Repository.Update(userFeedback);
            return true;
        }
    }
}