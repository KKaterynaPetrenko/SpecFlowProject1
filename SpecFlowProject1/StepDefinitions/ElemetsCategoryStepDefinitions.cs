using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SpecFlowProject1.Pages;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public class ElemetsCategoryStepDefinitions
    {
        IWebDriver webDriver;
        IJavaScriptExecutor js;
        MainPage mainPage;
        ElementsPage elementsPage;

        [BeforeScenario]
        public void SetUp()
        {
            webDriver = new ChromeDriver();
            mainPage = new MainPage(webDriver);
            elementsPage = new ElementsPage(webDriver);

        }
        [AfterScenario]
        public void CleanUp()
        {
            webDriver.Quit();
        }

        [Given(@"user navigate to the Elements category")]
        public void GivenUserNavigateToTheElementsCategory()
        {
            mainPage.NavigateToElementsCategory();
        }

        [Given(@"user is on the Text Box Section")]
        public void GivenUserIsOnTheTextBoxSection()
        {
            elementsPage.NavigateToTextBoxSection();
        }

        [When(@"user enter '([^']*)' , '([^']*)' , '([^']*)' and '([^']*)'")]
        public void WhenUserEnterAnd(string name, string email, string ñurrentAddress, string permanentAddress)
        {
            elementsPage.FillAllFieldsInForm(name, email, ñurrentAddress, permanentAddress);
        }

        [When(@"click on the submit button")]
        public void WhenClickOnTheSubmitButton()
        {
            elementsPage.SubmitTheForm();
        }

        [Then(@"just entered info '([^']*)' , '([^']*)' , '([^']*)' and '([^']*)'")]
        public void ThenJustEnteredInfoAnd(string name, string email, string currentAddress, string permanentAddress)
        {
            Assert.Multiple(() =>
            {
                Assert.That(elementsPage.DisplayedName(), Is.EqualTo(name));
                Assert.That(elementsPage.DisplayedEmail(), Is.EqualTo(email));
                Assert.That(elementsPage.DisplayedCurrentAddress(), Is.EqualTo(currentAddress));
                Assert.That(elementsPage.DisplayedPermanentAddress(), Is.EqualTo(permanentAddress));

            });
        }

        [Given(@"user is on Check Box Section")]
        public void GivenUserIsOnCheckBoxSection()
        {
            elementsPage.NavigateToCheckBoxSection();

        }

        [When(@"user unfolds the '([^']*)' folder")]
        public void WhenUserUnfoldsTheFolder(string home)
        {
            elementsPage.ToggleTheList(home);
        }

        [When(@"user select '([^']*)' folder without unfolding it")]
        public void WhenUserSelectFolderWithoutUnfoldingIt(string desktop)
        {
            elementsPage.SelectTheFolder(desktop);
        }

        [When(@"user unfolds '([^']*)' than '([^']*)' folder and select '([^']*)' and '([^']*)' items")]
        public void WhenUserUnfoldsThanFolderAndSelectAndItems(string documents, string workspace, string angular, string veu)
        {
            elementsPage.ToggleTheList(documents);
            elementsPage.ToggleTheList(workspace);
            elementsPage.SelectTheFolder(angular);
            elementsPage.SelectTheFolder(veu);
        }

        [When(@"user unfolds '([^']*)' folder and select one by one '([^']*)' , '([^']*)' , '([^']*)' and '([^']*)'")]
        public void WhenUserUnfoldsFolderAndSelectOneByOneAnd(string office, string publicFile, string privateFile, string classified, string general)
        {
            elementsPage.ToggleTheList(office);
            js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript("window.scrollBy(0,200)");
            elementsPage.SelectTheFolder(publicFile);
            elementsPage.SelectTheFolder(privateFile);
            elementsPage.SelectTheFolder(classified);
            elementsPage.SelectTheFolder(general);
        }

        [When(@"user unfolds '([^']*)' folder and select whole folder")]
        public void WhenUserUnfoldsFolderAndSelectWholeFolder(string downloads)
        {
            elementsPage.ToggleTheList(downloads);
            elementsPage.SelectTheFolder(downloads);
        }

        [Then(@"result of selecting folders equals to '([^']*)'")]
        public void ThenResultOfSelectingFoldersEqualsTo(string expectedResult)
        {
            Assert.That(elementsPage.GetStringOfResult(), Is.EqualTo(expectedResult));
        }

        [Given(@"user is on Web Tables Section")]
        public void GivenUserIsOnWebTablesSection()
        {
            elementsPage.NavigateToWebTableSection();
        }

        [When(@"click on '([^']*)' column")]
        public void WhenClickOnColumn(string salary)
        {
            elementsPage.ascendingFilterByColunm(salary);
        }

        [Then(@"values are sorted by increasing in '([^']*)' column")]
        public void ThenValuesAreSortedByIncreasingInColumn(string salary)
        {
            var actualResult = (elementsPage.getColumnByName(salary)).Select(x => Int32.Parse(x)).ToList();
            Assert.That(actualResult, Is.Ordered);
        }


        [When(@"click on Trash Can Icon in Action column in '([^']*)' row")]
        public void WhenClickOnTrashCanIconInActionColumnInRow(string orderNum)
        {
            elementsPage.deleteRow(orderNum);
        }


        [Then(@"there are (.*) records in the table")]
        public void ThenThereAreRecordsInTheTable(int quantity)
        {
            var actualResult = elementsPage.quantityOfRecordsInTheTable();
            Assert.That(actualResult, Is.EqualTo(quantity));
        }


        [Then(@"the '([^']*)' column does not have the '([^']*)' value")]
        public void ThenTheColumnDoesNotHaveTheValue(string department, string compliance)
        {
            Assert.That(elementsPage.getColumnByName(department), Has.No.Member(compliance));
        }


        [Given(@"user is on Button Section")]
        public void GivenUserIsOnButtonSection()
        {
            elementsPage.NavigateToButtonSection();
        }

        [When(@"click on '([^']*)'")]
        public void WhenClickOn(string buttonName)
        {
            elementsPage.clickByDifferentWay(buttonName);
        }

        [Then(@"message:'([^']*)' is displayed")]
        public void ThenMessageIsDisplayed(string message)
        {
            Assert.That(elementsPage.getMessageText(), Is.EqualTo(message));
        }
    }
}
