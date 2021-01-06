using Feedback.API.Infrastructure;
using Feedback.API.Model.Feedback;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feedback.API.IntegrationTests.Context
{
    internal class FeedbackSqlTestContext : FeedbackSqlContext
    {
        public FeedbackSqlTestContext(string connectionString) : base(connectionString) { }

        protected override void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFeedback>(
                uf =>
                {
                    uf.HasData(new UserFeedback
                    {
                        Id = 1,
                        Date = DateTime.Now,
                        PatientAccountId = 1,
                        UserComment = "Excellent service."
                    });
                    uf.OwnsOne(x => x.FeedbackVisibility).HasData(new
                    {
                        UserFeedbackId = 1,
                        IsPublic = true,
                        IsPublished = true,
                        IsAnonymous = false
                    });
                });
        }
    }
}
