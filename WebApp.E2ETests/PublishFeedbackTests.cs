using MySql.Data.MySqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WebApp.E2ETests.Pages;
using Xunit;

namespace WebApp.E2ETests
{
    public class PublishFeedbackTests
    {
        private readonly IWebDriver driver;
        private LoginPage loginPage;
        private ObserveFeedbackPage feedbackPage;
        

        public PublishFeedbackTests()
        {
            
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("disable-infobars");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-notifications");

            driver = new ChromeDriver(options);
            feedbackPage = new ObserveFeedbackPage(driver);
            loginPage = new LoginPage(driver);
            EnsureFeedbackNotPublished();

            loginPage.Navigate();
            loginPage.EnsurePageIsDisplayed();
            Assert.True(loginPage.EmailDisplayed());
            Assert.True(loginPage.PasswordDisplayed());
            Assert.True(loginPage.LoginButtonDisplayed());
            loginPage.Login();

            feedbackPage.EnsurePageIsDisplayed();
            Assert.True(feedbackPage.PublishButtonDisplayed());
        }
        [Fact]
        public void Publishes_feedback()
        {
            feedbackPage.Publish();
            feedbackPage.EnsureSuccessToastIsDisplayed();
            Dispose();
        }

        private void EnsureFeedbackNotPublished()
        {
            var connection = new MySqlConnection(CreateConnectionStringFromEnvironment());
            connection.Open();
            MySqlCommand command = new MySqlCommand("update feedback.userfeedbacks set FeedbackVisibility_IsPublished=false where id=1;", connection);
            command.ExecuteNonQuery();
            connection.Close();
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

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
