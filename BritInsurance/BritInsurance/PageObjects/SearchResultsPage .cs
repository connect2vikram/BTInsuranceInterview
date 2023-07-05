using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace BritInsurance.PageObjects
{
    internal class SearchResultsPage
    {
        private readonly IWebDriver _webDriver;
        public const int DefaultWaitInSeconds = 10;
        private WebDriverWait _wait;
        private const string SearchPage = "hero-static__title";
        private const string SearchResultContainer = "results-container";
        private const string SearchResult = "s-results__tag";

        public SearchResultsPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
        }

        public void HasSearchPageLoaded()
        {
            _wait.Until(ExpectedConditions.ElementExists(By.ClassName(SearchPage)));
        }

        public IWebElement ResultsContainer => _webDriver.FindElement(By.ClassName(SearchResultContainer));
        public List<IWebElement> Results => this.ResultsContainer.FindElements(By.ClassName(SearchResult)).ToList();
    }
}

