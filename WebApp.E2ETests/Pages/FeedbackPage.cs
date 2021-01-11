using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebApp.E2ETests.Pages
{
    public class FeedbackPage
    {
        private readonly IWebDriver _driver;
        public const string URI = "http://localhost:8080/#/feedbacks";
        private IWebElement FeedbackForm => _driver.FindElement(By.Id("feedback-form"));
        private IWebElement FeedbackTextArea => _driver.FindElement(By.Id("feedback-textarea"));
        private IWebElement PublicCheckBox => _driver.FindElement(By.Id("public-feedback"));
        private IWebElement AnonymousCheckBox => _driver.FindElement(By.Id("anon-feedback"));
        private IWebElement SubmitButton => _driver.FindElement(By.Id("submit-btn"));
        private IWebElement ToastifyNotification => _driver.FindElement(By.CssSelector(".toastify-top"));

        public FeedbackPage(IWebDriver driver) => _driver = driver;
        public bool IsSubmitEnabled => SubmitButton.Enabled;
        public void Navigate() => _driver.Navigate().GoToUrl(URI);
        public void SubmitFeedback() => SubmitButton.Click();
        
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
        public void NotificationSuccess()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0,0,20));
            wait.Until(condition =>
            {
                try
                {
                    return ToastifyNotification.Text.Contains("Feedback succesfully added!");
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
            return FeedbackForm.Displayed && FeedbackTextArea.Displayed && PublicCheckBox.Displayed 
                   && AnonymousCheckBox.Displayed && SubmitButton.Displayed;
        }

        public void FillForm()
        {
            FeedbackTextArea.SendKeys("1z7rfxeyqh333kt4sidfsr36y424gqvg");
            PublicCheckBox.Click();
            AnonymousCheckBox.Click();
        }
    }
}