using BritInsurance.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using BritInsurance.Utilities;


namespace BritInsurance
{
    public class Tests
    {
        private IWebDriver _driver;
        private BritInsuranceHomePage _homePage;
        private JavascriptHelper _javascriptHelper;


        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _homePage = new BritInsuranceHomePage(_driver);
            _javascriptHelper = new JavascriptHelper(_driver);
            _homePage.LoadSite();
            Thread.Sleep(2000);
            _javascriptHelper.WaitForPageLoadToComplete();
            _javascriptHelper.WaitForAjax();
            _homePage.AcceptCookies();
            
        }

        [Test]
        [TestCase("IFRS 17",3, "Interim results for the six months ended 30 June 2022", "Gavin Wilkinson", "John King")]
        public void SearchShouldReturnExpectedCountAndResults(string searchTerm, int countOfResultsReturned, string result1, string result2, string result3)
        {
            var searchResultsPage = _homePage.Search(searchTerm);
            searchResultsPage.HasSearchPageLoaded();
            var totalResults = searchResultsPage.Results;

            Assert.That(totalResults.Count,Is.EqualTo(countOfResultsReturned), searchTerm + " should have returned " + countOfResultsReturned + " results");
            Assert.That(totalResults[0].Text,Is.EqualTo(result1));
            Assert.That(totalResults[1].Text, Is.EqualTo(result2));
            Assert.That(totalResults[2].Text, Is.EqualTo(result3));
        }

        [Test]
        [TestCase("bermuda office")]
        public void ClickOnContactShouldReturnBermudaOfficeAddress(string officeAddressToBeLocated)
        {
            var navigationMenuPage = _homePage.ClickHeaderToggle();
            //_javascriptHelper.WaitForPageLoadToComplete();
            //_javascriptHelper.WaitForAjax();
            Thread.Sleep(3000);
            _javascriptHelper.ScrollDownToMenu(navigationMenuPage.ContactMenu);
            var contactUsPage = navigationMenuPage.ClickNavigationMenu(navigationMenuPage.ContactMenu);
            var containers = contactUsPage.FindComponentContainers().ToList();
            var bermudacontainer = ScrollUntilAddressIsLocated(containers, officeAddressToBeLocated, contactUsPage);
            Assert.That(bermudacontainer.FindElement(By.TagName("address")).Text.Replace("\r\n"," "), Is.EqualTo("Ground Floor, Chesney House The Waterfront, 96 Pitts Bay Road, Pembroke, Hamilton HM 08, Bermuda"));
        }

        private IWebElement? ScrollUntilAddressIsLocated(List<IWebElement?> containers, string locationtofind,ContactUsPage contactUsPage, int xpos = 0, int ypos = 400)
        {
            foreach (var element in containers)
            {
                var h2 = element.FindElements(By.TagName("h2")).FirstOrDefault();
                if (h2 != null && h2.Text.ToLower().Contains(locationtofind))
                {
                    return element;
                }
                _javascriptHelper.ScrollPageByPosition(xpos, ypos);
                ypos += 400;
            }

            return null;
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
        }
    }
}
