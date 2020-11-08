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
        private readonly RepositoryWrapper<UserFeedbackRepository> _userFeedbackRepository;

        public UserFeedbackService(UserFeedbackRepository repository)
        {
            _userFeedbackRepository = new RepositoryWrapper<UserFeedbackRepository>(repository);
        }

        public IEnumerable<UserFeedback> GetAll()
        {
            return _userFeedbackRepository.Repository.GetAll();
        }

        public UserFeedback Create(UserFeedback userFeedback)
        {
            return _userFeedbackRepository.Repository.Create(userFeedback);
        }

        public UserFeedback Update(UserFeedback userFeedback)
        {
            return _userFeedbackRepository.Repository.Update(userFeedback);
        }

        /// <summary>
        ///     Publishes a <see cref="UserFeedback"/> with a given id.
        /// </summary>
        /// <param name="id">Id of the <see cref="UserFeedback"/> to be published.</param>
        /// <returns>
        ///     True if publishing succeeds. False if a UserFeedback with the given ID cannot be found,
        ///     if it is already published, or if Feedback is not set to public.
        /// </returns>
        public bool Publish(int id)
        {
            UserFeedback userFeedback = _userFeedbackRepository.Repository.GetByID(id);
            if (userFeedback.isPublished) return false;    // If already published
            if (!userFeedback.isPublic) return false;    // If not public
            userFeedback.isPublished = true;
            _userFeedbackRepository.Repository.Update(userFeedback);
            return true;
        }

        /// <summary>
        ///     Gets a list of all UserFeedbacks where <see cref="UserFeedback.isPublished"/> is True.
        /// </summary>
        /// <returns>List of Feedbacks</returns>
        public IEnumerable<UserFeedback> GetAllPublished()
            => _userFeedbackRepository.Repository.GetMatching(feedback => feedback.isPublished);
    }
}