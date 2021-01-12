using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebApp.E2ETests.Pages
{
    public class PatientsHomePage
    {
        private readonly IWebDriver _driver;
        public const string URI = "http://localhost:8080/#/feedbacks";
        private IWebElement NavBar => _driver.FindElement(By.ClassName("navbar"));
        private IWebElement NavBarDropDown => _driver.FindElement(By.Id("navbarDropdown"));
        public PatientsHomePage(IWebDriver driver) => _driver = driver;
        public bool NavBarContainsUsername() => NavBarDropDown.Text.Contains("Nikola");
        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return NavBar.Displayed && NavBarContainsUsername();
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
    }
}