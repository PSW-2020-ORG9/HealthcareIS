using HealthcareBase.Model.Database;
using Microsoft.EntityFrameworkCore;
using Model.Users.UserFeedback;
using Repository.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.UsersRepository.UserFeedbackRepository
{
    public class UserFeedbackSqlRepository : GenericSqlRepository<UserFeedback, int>, UserFeedbackRepository
    {
        public UserFeedbackSqlRepository(IContextFactory contextFactory) : base(contextFactory) { }


        protected override IQueryable<UserFeedback> IncludeFields(IQueryable<UserFeedback> query) 
        {
            return query.Include(uf => uf.PatientAccount);
                
            
        }
    }
}
