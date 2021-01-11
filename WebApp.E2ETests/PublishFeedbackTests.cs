using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebApp.E2ETests.Pages;
using Xunit;

namespace WebApp.E2ETests
{
    public class PublishFeedbackTests
    {
        private readonly IWebDriver driver;
        private LoginPage loginPage;

        public PublishFeedbackTests()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("disable-infobars");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-notifications");

            driver = new ChromeDriver(options);
            loginPage = new LoginPage(driver);
            loginPage.Navigate();
            loginPage.EnsurePageIsDisplayed();
            Assert.True(loginPage.EmailDisplayed());
            Assert.True(loginPage.PasswordDisplayed());
            Assert.True(loginPage.LoginButtonDisplayed());
        }
        [Fact]
        public void Publishes_feedback()
        {
            loginPage.LoginAsAdmin();
        }
        
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
