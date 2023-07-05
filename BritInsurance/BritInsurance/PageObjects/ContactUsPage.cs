using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Xml.Linq;


namespace BritInsurance.PageObjects
{
    internal class ContactUsPage
    {
        private readonly IWebDriver _webDriver;
        public const int DefaultWaitInSeconds = 10;
        private WebDriverWait _wait;

        public ContactUsPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
        }

        public ICollection<IWebElement?> FindComponentContainers()
        {
            return _webDriver.FindElements(By.ClassName("component--container"));

        }
    }
}

