using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebApp.E2ETests.Pages;
using Xunit;

namespace WebApp.E2ETests
{
    public class CreateFeedbackTests
    {
        private readonly IWebDriver driver;
        private LoginPage _loginPage;
        private FeedbackPage _feedbackPage;
        private PatientsHomePage _patientsHomePage;

        public CreateFeedbackTests()
        {
            var options = new ChromeOptions();
            ConfigureChromeOptions(options);
            driver = new ChromeDriver(options);
            SetupPages();
        }

        private void SetupPages()
        {
            _loginPage = new LoginPage(driver);
            _feedbackPage = new FeedbackPage(driver);
            _patientsHomePage = new PatientsHomePage(driver);
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
            driver.Quit();
            driver.Dispose();
        }
    }
}