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
        private DbConnection dbConnection;


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
            dbConnection = new DbConnection();
            
            dbConnection.EnsureFeedbackNotPublished();

            loginPage.Navigate();
            loginPage.EnsurePageIsDisplayed();
            Assert.True(loginPage.EmailDisplayed());
            Assert.True(loginPage.PasswordDisplayed());
            Assert.True(loginPage.LoginButtonDisplayed());
            loginPage.LoginAsAdmin();

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
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
