// File:    UserFeedbackFileRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:51:37
// Purpose: Definition of Class UserFeedbackFileRepository

using HealthcareBase.Model.Users.UserFeedback;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.UserFeedbackRepository
{
    public class UserFeedbackFileRepository : GenericFileRepository<UserFeedback, int>, UserFeedbackRepository
    {
        private readonly IntegerKeyGenerator keyGenerator;

        public UserFeedbackFileRepository(string filePath) : base(filePath)
        {
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        protected override int GenerateKey(UserFeedback entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override UserFeedback ParseEntity(UserFeedback entity)
        {
            return entity;
        }
    }
}