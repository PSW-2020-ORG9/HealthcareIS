using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;

namespace WebApp.E2ETests.Pages
{
    class ObserveFeedbackPage
    {
        private readonly IWebDriver _driver;
        public const string URI = "http://localhost:8080/#/feedbacks";
        public ObserveFeedbackPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private ReadOnlyCollection<IWebElement> Feedbacks => _driver.FindElements(By.Name("feedback"));
        private IWebElement PublishButton => _driver.FindElement(By.Id("publish1"));
        private IWebElement Toast => _driver.FindElement(By.CssSelector(".toastify-top"));

        public bool PublishButtonDisplayed() => PublishButton.Displayed;
        
        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return Feedbacks.Count > 0;
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
        public void Publish()
        {
            PublishButton.Click();
        }

        public void EnsureSuccessToastIsDisplayed()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return Toast.Text.Contains("Feedback succesfully published!");
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
        public void Navigate() => _driver.Navigate().GoToUrl(URI);
    }
}
