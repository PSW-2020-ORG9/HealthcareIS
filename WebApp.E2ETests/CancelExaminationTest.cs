using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebApp.E2ETests.Pages;
using Xunit;

namespace WebApp.E2ETests
{
    public class CancelExaminationTest
    {
        private readonly IWebDriver _driver;
        private LoginPage _loginPage;
        private ExaminationsPage _examinationsPage;
        private PatientsHomePage _patientsHomePage;
        private DbConnection _dbConnection;

        public CancelExaminationTest()
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
            _examinationsPage = new ExaminationsPage(_driver);
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
        public void Cancel_Examination()
        {
            _loginPage.Navigate();
            _loginPage.EnsurePageIsDisplayed();
            _loginPage.LoginAsPatient();
            
            SeedData();
            _patientsHomePage.EnsurePageIsDisplayed();
            
            _examinationsPage.Navigate();
            _examinationsPage.EnsurePageIsDisplayed();
            _examinationsPage.CancelExamination();

            RemoveSeededData();
            Dispose();
        }

        private void SeedData()
        {
            _dbConnection.EnsureExaminationNotCancelled();
        }

        private void RemoveSeededData()
        {
            _dbConnection.RestoreTestChangesForExaminations();
        }

        private void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}