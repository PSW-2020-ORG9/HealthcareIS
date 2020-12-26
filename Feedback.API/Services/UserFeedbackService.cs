// File:    UserFeedbackService.cs
// Author:  Lana
// Created: 28 May 2020 17:09:20
// Purpose: Definition of Class UserFeedbackService

using Feedback.API.Infrastructure.Repositories;
using Feedback.API.Model.Feedback;
using Feedback.API.Model.Survey;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using System.Linq;

namespace Feedback.API.Services
{
    public class UserFeedbackService : IUserFeedbackService
    {
        private readonly RepositoryWrapper<IUserFeedbackRepository> _userFeedbackRepository;

        public UserFeedbackService(IUserFeedbackRepository repository)
        {
            _userFeedbackRepository = new RepositoryWrapper<IUserFeedbackRepository>(repository);
        }

        public IEnumerable<UserFeedback> GetAll()
        {
            var feedbacks = _userFeedbackRepository.Repository.GetAll();
            AttachPatientAccounts(feedbacks);
            return feedbacks;
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
        {
            var feedbacks = _userFeedbackRepository.Repository.GetMatching(feedback => feedback.FeedbackVisibility.IsPublished);
            AttachPatientAccounts(feedbacks);
            return feedbacks;
        }

        private void AttachPatientAccounts(IEnumerable<UserFeedback> feedbacks)
        {
            List<int> patientAccountIds = new List<int>();
            foreach (var feedback in feedbacks)
            {
                patientAccountIds.Add(feedback.PatientAccountId);
            }
            List<PatientAccount> patientAccounts = FindPatientAccounts(patientAccountIds);
            foreach (var feedback in feedbacks)
            {
                feedback.PatientAccount = patientAccounts.Where(pa => pa.Id == feedback.PatientAccountId).First();
            }
        }

        private List<PatientAccount> FindPatientAccounts(List<int> patientAccountIds)
        {
            //TODO: Shouldn't be hardcoded
            var client = new RestClient("http://localhost:5003/");
            var request = new RestRequest("patient/accounts", DataFormat.Json);
            request.AddJsonBody(patientAccountIds);
            var response = client.Post(request);
            JsonDeserializer deserializer = new JsonDeserializer();
            return deserializer.Deserialize<List<PatientAccount>>(response);
        }
    }
}