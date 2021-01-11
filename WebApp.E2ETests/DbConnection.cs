using System;
using MySql.Data.MySqlClient;

namespace WebApp.E2ETests
{
    public class DbConnection
    {
        private MySqlConnection _connection; 

        public DbConnection()
        {
            _connection = new MySqlConnection(CreateConnectionStringFromEnvironment());
        }
        
        private string CreateConnectionStringFromEnvironment()
        {
            string server = Environment.GetEnvironmentVariable("DB_PSW_SERVER");
            string port = Environment.GetEnvironmentVariable("DB_PSW_PORT");
            string database = Environment.GetEnvironmentVariable("DB_PSW_FEEDBACK_DATABASE");
            string user = Environment.GetEnvironmentVariable("DB_PSW_USER");
            string password = Environment.GetEnvironmentVariable("DB_PSW_PASSWORD");
            if (server == null
                || port == null
                || database == null
                || user == null
                || password == null)
                return null;

            return $"server={server};port={port};database={database};user={user};password={password};";
        }
        public void EnsureFeedbackNotPublished()
        {
            _connection.Open();
            var command = new MySqlCommand("update feedback.userfeedbacks set FeedbackVisibility_IsPublished=false where id=1;", _connection);
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void EnsureFeedbackIsDeletedAfterTest()
        {
            _connection.Open();
            var command = new MySqlCommand("delete from feedback.UserFeedbacks where UserComment='1z7rfxeyqh333kt4sidfsr36y424gqvg'", _connection);
            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}