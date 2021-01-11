using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using Xunit;

namespace WebApp.E2ETests.Pages
{
    class LoginPage
    {
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }
        private readonly IWebDriver _driver;
        public const string URI = "http://localhost:8080/#/login";
        private IWebElement LoginForm => _driver.FindElement(By.Id("msform"));
        private IWebElement EmailField => _driver.FindElement(By.Name("email"));
        private IWebElement PasswordField => _driver.FindElement(By.Name("password"));
        private IWebElement LoginButton => _driver.FindElement(By.Name("login"));
        
        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return PageIsFormed();
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }

        private bool PageIsFormed()
        {
            return LoginForm.Displayed && EmailField.Displayed && PasswordField.Displayed && LoginButton.Displayed;
        }

        public void LoginAsAdmin()
        {
            EmailField.SendKeys("admin@mail.com");
            PasswordField.SendKeys("123456");
            LoginButton.Click();

        }

        public void LoginAsPatient()
        {
            EmailField.SendKeys("nikola@mail.com");
            PasswordField.SendKeys("123456");
            LoginButton.Click();
        }

        public bool EmailDisplayed() => EmailField.Displayed;
        public bool PasswordDisplayed() => PasswordField.Displayed;
        public bool LoginButtonDisplayed() => LoginButton.Displayed;
        public void Navigate() => _driver.Navigate().GoToUrl(URI);
    }
}
