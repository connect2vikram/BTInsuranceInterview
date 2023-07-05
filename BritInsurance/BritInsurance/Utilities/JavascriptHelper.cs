using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace BritInsurance.Utilities
{
    internal class JavascriptHelper
    {
        private readonly IWebDriver _webDriver;
        private WebDriverWait _wait;
        private IJavaScriptExecutor _javaScriptExecutor;

        public JavascriptHelper(IWebDriver webDriver, int intTimeOutFromSeconds = 60)
        {
            _webDriver = webDriver;
            _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(intTimeOutFromSeconds));
            _javaScriptExecutor = (IJavaScriptExecutor)_webDriver;
        }

        public void WaitForPageLoadToComplete()
        {
            _wait.Until
            (
                wd =>
                    _javaScriptExecutor.ExecuteScript("return document.readyState").ToString() == "complete"
            );

        }

        public void ScrollDownToMenu(IWebElement element)
        {
            _javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", element);

        }

        public void ScrollPageByPosition(int x, int y)
        {
            _javaScriptExecutor.ExecuteScript("window.scrollBy(" + x + "," + y + ");");
        }

        public void WaitForAjax()
        {
            while (true) // Handle timeout somewhere
            {
                try
                {
                    var ajaxIsComplete = (bool)_javaScriptExecutor.ExecuteScript("return window.jQuery.active == 0");
                    if (ajaxIsComplete)
                        break;
                    Thread.Sleep(100);
                }
                catch (JavaScriptException js)
                {
                }
            }

        }
    }
}
