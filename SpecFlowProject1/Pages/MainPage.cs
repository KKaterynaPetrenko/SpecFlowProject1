using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Xml.Linq;


namespace SpecFlowProject1.Pages
{
    public class MainPage
    {
        IWebDriver webDriver;
        IJavaScriptExecutor js;
        private IWebElement elementsCategory 
            => webDriver.FindElement(By.XPath("//h5[text()='Elements']/parent::div"));
        public MainPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }
        public void NavigateToElementsCategory()
        {
            webDriver.Navigate().GoToUrl("https://demoqa.com/");
            webDriver.Manage().Window.Maximize();

            js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript("window.scrollBy(0,1000)");

            elementsCategory.Click();
        }
    }
}
