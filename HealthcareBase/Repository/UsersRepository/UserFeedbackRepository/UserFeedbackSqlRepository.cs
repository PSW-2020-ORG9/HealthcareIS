using HealthcareBase.Model.Database;
using Model.Users.UserFeedback;
using Repository.Generics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.UsersRepository.UserFeedbackRepository
{
    public class UserFeedbackSqlRepository : GenericSqlRepository<UserFeedback, int>, UserFeedbackRepository
    {
        public UserFeedbackSqlRepository(IContextFactory contextFactory) : base(contextFactory) { }
    }
}
