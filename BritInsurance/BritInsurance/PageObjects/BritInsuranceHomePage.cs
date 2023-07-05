using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace BritInsurance.PageObjects
{
    internal class BritInsuranceHomePage
    {
        private const string BritInsuranceHomePageUrl = "https://www.britinsurance.com/";
        private readonly IWebDriver _webDriver;
        public const int DefaultWaitInSeconds = 10;
        private WebDriverWait _wait;
        private const string Searchdivlocator = "component--header__search";
        private const string Searchbuttonlocator = "button[type='button']";
        private const string Searcheditboxlocator = "header--search";


        public BritInsuranceHomePage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
        }

        public void LoadSite()
        {
            _webDriver.Navigate().GoToUrl(BritInsuranceHomePageUrl);
            _webDriver.Manage().Window.Maximize();
        }

        public IWebElement SearchButton
        {
            get
            {

                IWebElement searchDiv = _webDriver.FindElement(By.ClassName(Searchdivlocator)) ;
                var searchbutton = searchDiv.FindElement(By.CssSelector(Searchbuttonlocator));
                _wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(Searchbuttonlocator)));
                return searchbutton;
            }

        }

        public IWebElement HeaderToggle
        {
            get
            {
                IWebElement headerToggeleDiv = _wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("component--header__burger")));
                return headerToggeleDiv.FindElement(By.ClassName("header--toggle"));
            }

        }

        public IWebElement SearchBox
        {
            get
            {
                IWebElement searchDiv = _wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(Searcheditboxlocator)));
                var searchEditBox = searchDiv.FindElement(By.TagName("input"));
                return searchEditBox;
            }

        }

        public SearchResultsPage Search(string searchTerm)
        {
            SearchButton.Click();
            SearchBox.SendKeys(searchTerm);
            SearchButton.Submit();
            return new SearchResultsPage(_webDriver);
        }

        public NavigationMenuPage ClickHeaderToggle()
        {
           this.HeaderToggle.Click();
           return new NavigationMenuPage(_webDriver);
        }

        public void AcceptCookies()
        {
            _webDriver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll")).Click();
            _wait.Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("CybotCookiebotDialogBodyButton")));
        }
    }
}
