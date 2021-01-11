using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebApp.E2ETests.Pages;
using Xunit;

namespace WebApp.E2ETests
{
    public class CreateFeedbackTests
    {
        private readonly IWebDriver _driver;
        private LoginPage _loginPage;
        private FeedbackPage _feedbackPage;
        private PatientsHomePage _patientsHomePage;
        private DbConnection _dbConnection;

        public CreateFeedbackTests()
        {
            var options = new ChromeOptions();
            ConfigureChromeOptions(options);
            _driver = new ChromeDriver(options);
            _dbConnection = new DbConnection();
            SetupPages();
        }

        private void SetupPages()
        {
            _loginPage = new LoginPage(_driver);
            _feedbackPage = new FeedbackPage(_driver);
            _patientsHomePage = new PatientsHomePage(_driver);
        }

        private static void ConfigureChromeOptions(ChromeOptions options)
        {
            options.AddArgument("start-maximized");
            options.AddArgument("disable-infobars");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-notifications");
        }

        [Fact]
        public void Creates_feedback()
        {
            _loginPage.Navigate();
            _loginPage.EnsurePageIsDisplayed();
            _loginPage.LoginAsPatient();
            
            _patientsHomePage.EnsurePageIsDisplayed();
            
            _feedbackPage.Navigate();
            _feedbackPage.EnsurePageIsDisplayed();
            _feedbackPage.FillForm();
            _feedbackPage.SubmitFeedback();
            _feedbackPage.NotificationSuccess();
            _dbConnection.EnsureFeedbackIsDeletedAfterTest();
            
            Dispose();
        }

        [Fact]
        public void Checks_if_submission_is_disabled()
        {
            _loginPage.Navigate();
            _loginPage.EnsurePageIsDisplayed();
            _loginPage.LoginAsPatient();
            
            _patientsHomePage.EnsurePageIsDisplayed();
            
            _feedbackPage.Navigate();
            _feedbackPage.EnsurePageIsDisplayed();
            Assert.False(_feedbackPage.IsSubmitEnabled);
            
            Dispose();
        }
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}