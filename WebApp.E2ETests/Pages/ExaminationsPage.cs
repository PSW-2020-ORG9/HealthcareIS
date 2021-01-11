using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebApp.E2ETests.Pages
{
    public class ExaminationsPage
    {
        public ExaminationsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private readonly IWebDriver _driver;
        private const string URI = "http://localhost:8080/#/examinations";
        public const string examinationId = "6";
        private const string CancelButtonId = "cancelButton-" + examinationId;
        private IWebElement CancelButton => _driver.FindElement(By.Id(CancelButtonId));

        public void Navigate() => _driver.Navigate().GoToUrl(URI);
        
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
            return CancelButton.Displayed;
        }

        public void CancelExamination()
        {
            CancelButton.Click();
        }
    }
}