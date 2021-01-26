using Feedback.API.Feeback.Domain.AggregatesModel.FeedbackAggregate;
using Feedback.API.Model.Survey;
using Feedback.API.Model.Survey.SurveyEntry;
using Microsoft.EntityFrameworkCore;
using System;

namespace Feedback.API.Infrastructure
{
    public class FeedbackSqlContext : DbContext
    {
        private readonly string _connectionString;
        public FeedbackSqlContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString);
        }

        public DbSet<UserFeedback> UserFeedbacks { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveySection> SurveySections { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<RatedSurvey> SurveyResponses { get; set; }
        public DbSet<RatedSurveySection> RatedSurveySections { get; set; }
        public DbSet<RatedSurveyQuestion> RatedSurveyQuestions { get; set; }
        public DbSet<DoctorSurveySection> DoctorSurveySections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFeedback>()
                 .HasKey(uf => uf.Id);
            modelBuilder.Entity<UserFeedback>()
                .OwnsOne(uf => uf.FeedbackVisibility);
            modelBuilder.Entity<UserFeedback>()
                .Ignore(uf => uf.PatientAccount);

            modelBuilder.Entity<Survey>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Survey>()
                .HasMany(s => s.SurveySections);

            modelBuilder.Entity<SurveySection>()
                .HasKey(ss => ss.Id);
            modelBuilder.Entity<SurveySection>()
                .HasMany(ss => ss.SurveyQuestions);

            modelBuilder.Entity<SurveyQuestion>()
                .HasKey(sq => sq.Id);

            modelBuilder.Entity<RatedSurvey>()
                .HasKey(sr => sr.Id);

            modelBuilder.Entity<RatedSurvey>()
                .HasMany(sr => sr.RatedSurveySections);
            modelBuilder.Entity<RatedSurvey>()
                .HasOne(sr => sr.Survey);
            modelBuilder.Entity<RatedSurvey>()
                .HasOne(sr => sr.DoctorSurveySection);
            modelBuilder.Entity<RatedSurvey>()
                .Ignore(sr => sr.PatientAccount);

            modelBuilder.Entity<RatedSurveyQuestion>()
                .HasKey(rq => rq.Id);
            modelBuilder.Entity<RatedSurveyQuestion>()
                .HasOne(rq => rq.SurveyQuestion);

            modelBuilder.Entity<RatedSurveySection>()
                .HasKey(rs => rs.Id);
            modelBuilder.Entity<RatedSurveySection>()
                .HasOne(rs => rs.SurveySection);
            modelBuilder.Entity<RatedSurveySection>()
                .HasMany(rs => rs.RatedSurveyQuestions);

            modelBuilder.Entity<DoctorSurveySection>()
                .HasOne(ds => ds.SurveySection);
            modelBuilder.Entity<DoctorSurveySection>()
                .HasMany(ds => ds.RatedSurveyQuestions);

            SeedData(modelBuilder);
        }

        protected virtual void SeedData(ModelBuilder modelBuilder)
        {
            
        }
    }
}
