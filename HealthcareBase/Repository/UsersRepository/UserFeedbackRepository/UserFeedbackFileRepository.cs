// File:    UserFeedbackFileRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:51:37
// Purpose: Definition of Class UserFeedbackFileRepository

using Model.Users.UserFeedback;
using Model.Utilities;
using Repository.Generics;
using System;

namespace Repository.UsersRepository.UserFeedbackRepository
{
    public class UserFeedbackFileRepository : GenericFileRepository<UserFeedback, int>, UserFeedbackRepository
    {
        private IntegerKeyGenerator keyGenerator;

        public UserFeedbackFileRepository(String filePath) : base(filePath)
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