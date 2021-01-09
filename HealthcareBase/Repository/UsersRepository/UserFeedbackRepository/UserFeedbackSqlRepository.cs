﻿using System.Linq;
using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.UserFeedback;
using HealthcareBase.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Repository.UsersRepository.UserFeedbackRepository
{
    public class UserFeedbackSqlRepository : GenericSqlRepository<UserFeedback, int>, IUserFeedbackRepository
    {
        public UserFeedbackSqlRepository(IContextFactory contextFactory) : base(contextFactory) { }


        protected override IQueryable<UserFeedback> IncludeFields(IQueryable<UserFeedback> query) 
        {
            return query.Include(uf => uf.PatientAccount);
                
            
        }
    }
}