using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Linq;

namespace SpecFlowProject1.Pages
{
    public class ElementsPage
    {
        IWebDriver webDriver;
        IJavaScriptExecutor js;
        Actions actions;

        //textbox section
        private IWebElement textBoxSection => webDriver.FindElement(By.Id("item-0"));
        private IWebElement userNameField => webDriver.FindElement(By.Id("userName"));
        private IWebElement userEmailField => webDriver.FindElement(By.Id("userEmail"));
        private IWebElement currectAddressField => webDriver.FindElement(By.Id("currentAddress"));
        private IWebElement permanentAddressField => webDriver.FindElement(By.Id("permanentAddress"));
        private IWebElement submitButton => webDriver.FindElement(By.Id("submit"));
        private IWebElement displayedUserName => webDriver.FindElement(By.Id("name"));
        private IWebElement displayedEmail => webDriver.FindElement(By.Id("email"));
        private IWebElement displayedCurrentAddress => webDriver.FindElement(By.XPath("//p[@id ='currentAddress']"));
        private IWebElement displayedPermanentaddress => webDriver.FindElement(By.XPath("//p[@id ='permanentAddress']"));

        // check box section
        private IWebElement checkBoxSection => webDriver.FindElement(By.Id("item-1"));
        private IWebElement toggle(string itemName) => webDriver.FindElement(By.XPath($"//label[@for='tree-node-{itemName}']/preceding-sibling::button"));
        private IWebElement checkbox(string itemName) => webDriver.FindElement(By.XPath($"//input[@id='tree-node-{itemName}']/following-sibling::span[@class='rct-checkbox']"));
        private IList<IWebElement> results => webDriver.FindElements(By.XPath("//div[@id='result']/span"));

        // web table section
        private IWebElement webTableSection => webDriver.FindElement(By.Id("item-3"));
        private IWebElement column (string name) => webDriver.FindElement(By.XPath($"//div[@class='rt-resizable-header-content'][text() = '{name}']"));
        private IList<IWebElement> rows => webDriver.FindElements(By.XPath("//div[@class='rt-tbody']//div[@role='row']"));
        private IWebElement deleteAction(string orderNumber) => webDriver.FindElement(By.XPath($"//span[@id='delete-record-{orderNumber}']"));
        private Dictionary<string, int> columnNamesByIndex => (webDriver.FindElements(By.XPath("//div[@class='rt-resizable-header-content']")))
            .Select((x, i) => new {x.Text, i}).ToDictionary(x => x.Text, x => x.i);

        // Buttons section
        private IWebElement buttonsSection => webDriver.FindElement(By.Id("item-4"));
        private IWebElement button(string name) => webDriver.FindElement(By.XPath($"//button[text()='{name}']"));
        private IWebElement message => webDriver.FindElement(By.XPath("//div[@class='col-12 mt-4 col-md-6']//p"));

        public ElementsPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public string DisplayedName()
        {
            string text = displayedUserName.Text;
            return text.Substring(text.IndexOf(":") + 1);
        }
        public string DisplayedEmail()
        {
            string text = displayedEmail.Text;
            return text.Substring(text.IndexOf(":") + 1);
        }

        public string DisplayedCurrentAddress()
        {
            string text = displayedCurrentAddress.Text;
            return text.Substring(text.IndexOf(":") + 1);
        }

        public string DisplayedPermanentAddress()
        {
            string text = displayedPermanentaddress.Text;
            return text.Substring(text.IndexOf(":") + 1);
        }
        public ElementsPage FillAllFieldsInForm(string name, string email, string currentAddress, string permanentAddress)
        {
            userNameField.SendKeys(name);
            userEmailField.SendKeys(email);
            currectAddressField.SendKeys(currentAddress);
            permanentAddressField.SendKeys(permanentAddress);
            return this;
        }

        public ElementsPage SubmitTheForm()
        {
            js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript("window.scrollBy(0,200)");
            Thread.Sleep(10);
            submitButton.Click();
            return this;
        }
        public ElementsPage NavigateToTextBoxSection()
        {
            textBoxSection.Click();
            return this;
        }
        public ElementsPage NavigateToCheckBoxSection()
        {
            checkBoxSection.Click();
            return this;
        }
        public ElementsPage ToggleTheList(string folderName)
        {
            toggle(folderName).Click();
            return this;
        }
        public ElementsPage SelectTheFolder(string folderName)
        {
            checkbox(folderName).Click();
            return this;
        }

        public string GetStringOfResult()
        {
            string stringResult = "";
            foreach (IWebElement item in results)
            {
                stringResult += (" " + item.Text);
            }
            return stringResult;
        }
        public ElementsPage NavigateToWebTableSection()
        {
            webTableSection.Click();
            return this;
        }
        public ElementsPage ascendingFilterByColunm(string name)
        {
            column(name).Click();
            return this;
        }

        public List<string> getColumnByName(string name)
        {
            var columnValues = new List<string>();
            foreach (var item in rows)
            {
                var cells = item.FindElements(By.XPath(".//div"));
                var values = cells.Select(x => x.Text).ToList();
                if (!string.IsNullOrWhiteSpace(values[columnNamesByIndex[name]]))
                    columnValues.Add(values[columnNamesByIndex[name]]);
            }
            return columnValues;
        }

        public ElementsPage deleteRow(string orderNumber)
        {
            deleteAction(orderNumber).Click();
            return this;
        }

        public int quantityOfRecordsInTheTable()
        {
            return getColumnByName("First Name").Count();
        }

        public ElementsPage NavigateToButtonSection()
        {
            buttonsSection.Click();
            return this;
        }

        public ElementsPage clickByDifferentWay(string buttonName)
        {
            switch (buttonName)
            {
                case "Double Click Me":
                    actions = new Actions(webDriver);
                    actions.DoubleClick(button(buttonName)).Perform();
                    return this;
                case "Right Click Me":
                    actions = new Actions(webDriver);
                    actions.ContextClick(button(buttonName)).Perform();
                    return this;
                case "Click Me":
                    button(buttonName).Click();
                    return this;
            }
            return this;
        }
        public string getMessageText()
        {
            return message.Text;
        }
    }

}
