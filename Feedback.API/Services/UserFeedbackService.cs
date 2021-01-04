using Feedback.API.Infrastructure.Repositories;
using Feedback.API.Model.Feedback;
using Feedback.API.Model.User;
using General;
using General.Repository;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using System.Linq;

namespace Feedback.API.Services
{
    public class UserFeedbackService : IUserFeedbackService
    {
        private readonly RepositoryWrapper<IUserFeedbackRepository> _userFeedbackRepository;
        private readonly IConnection _patientAccountsConnection;

        public UserFeedbackService(IUserFeedbackRepository repository, IConnection patientAccountsConnection)
        {
            _userFeedbackRepository = new RepositoryWrapper<IUserFeedbackRepository>(repository);
            _patientAccountsConnection = patientAccountsConnection;
        }

        public IEnumerable<UserFeedback> GetAll()
        {
            var feedbacks = _userFeedbackRepository.Repository.GetAll();
            if (!feedbacks.Any())
                return feedbacks;
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
            if (!feedbacks.Any())
                return feedbacks;
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
                feedback.PatientAccount = patientAccounts.Where(pa => pa.Id == feedback.PatientAccountId).FirstOrDefault();
            }
        }

        private List<PatientAccount> FindPatientAccounts(List<int> patientAccountIds) 
            => _patientAccountsConnection.Post<List<PatientAccount>>(patientAccountIds);
    }
}