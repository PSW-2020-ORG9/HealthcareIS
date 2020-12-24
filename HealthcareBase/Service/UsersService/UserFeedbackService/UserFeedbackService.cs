// File:    UserFeedbackService.cs
// Author:  Lana
// Created: 28 May 2020 17:09:20
// Purpose: Definition of Class UserFeedbackService

using System.Collections.Generic;
using HealthcareBase.Model.Users.UserFeedback;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.UserFeedbackRepository;

namespace HealthcareBase.Service.UsersService.UserFeedbackService
{
    public class UserFeedbackService
    {
        private readonly RepositoryWrapper<IUserFeedbackRepository> _userFeedbackRepository;

        public UserFeedbackService(IUserFeedbackRepository repository)
        {
            _userFeedbackRepository = new RepositoryWrapper<IUserFeedbackRepository>(repository);
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
        public void Publish(int id)
        {
            var userFeedback = _userFeedbackRepository.Repository.GetByID(id);
            userFeedback.FeedbackVisibility = userFeedback.FeedbackVisibility.Publish();
            _userFeedbackRepository.Repository.Update(userFeedback);
        }

        /// <summary>
        ///     Gets a list of all UserFeedbacks where <see cref="UserFeedback.IsPublished"/> is True.
        /// </summary>
        /// <returns>List of Feedbacks</returns>
        public IEnumerable<UserFeedback> GetAllPublished()
            => _userFeedbackRepository.Repository.GetMatching(feedback => feedback.FeedbackVisibility.IsPublished);
    }
}