using Feedback.API.Model.Feedback;
using Feedback.API.Model.Survey;
using Feedback.API.Model.Survey.SurveyEntry;
using Microsoft.EntityFrameworkCore;

namespace Feedback.API.Infrastructure
{
    public class MySqlContext : DbContext
    {
        private readonly string _connectionString;
        public MySqlContext(string connectionString)
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
        public DbSet<SurveyResponse> SurveyResponses { get; set; }
        public DbSet<RatedSurveySection> RatedSurveySections { get; set; }
        public DbSet<RatedSurveyQuestion> RatedSurveyQuestions { get; set; }
        public DbSet<DoctorSurveySection> DoctorSurveySections { get; set; }
    }
}
