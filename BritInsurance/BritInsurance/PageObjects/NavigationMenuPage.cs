using BritInsurance.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace BritInsurance.PageObjects
{
    internal class NavigationMenuPage
    {
        private readonly IWebDriver _webDriver;
        public const int DefaultWaitInSeconds = 10;
        private WebDriverWait _wait;

        public NavigationMenuPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
        }

        public IWebElement ContactMenu => _webDriver.FindElement(By.PartialLinkText("contact"));

        public ContactUsPage ClickNavigationMenu(IWebElement element)
        {
            element.Click();
            return new ContactUsPage(this._webDriver);
        }

    }
}

