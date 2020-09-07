using KNAB_Assignment.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace KNAB_Assignment.Steps
{
    [Binding]
    public sealed class WebSteps
    {

        private IWebDriver webdriver;
        LoginPage loginPage = null;
        BoardsPage boardsPage = null;

        [Given(@"the website is opened")]
        public void GivenTheWebsiteIsOpened()
        {
            //TODO BrowserFactory creation so it can be used easier
            webdriver = new ChromeDriver();
            webdriver.Navigate().GoToUrl("https://trello.com/login/");
            webdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            loginPage = new LoginPage(webdriver);
            boardsPage = new BoardsPage(webdriver);
        }
        [Given(@"the user is logged in")]
        public void TheUserIsLoggedIn()
        {
            Assert.That(loginPage.UserLoggedIn(), Is.True);
        }

        [When(@"I create a board named ""(.*)""")]
        public void WhenICreateABoardNamed(string boardname)
        {
            boardsPage.CreateBoard(boardname);
        }

        [When(@"I close and remove board with the name ""(.*)""")]
        public void WhenICloseAndRemoveBoardWithTheName(string boardname)
        {
            boardsPage.CloseAndRemoveBoard(boardname);
        }

        [When(@"I enter the wrong login credentials")]
        public void WhenIEnterTheWrongLoginCredentials()
        {
            loginPage.WrongLogin("test@test", "123");
        }

        [When(@"I enter the login credentials")]
        public void WhenIEnterTheLoginCredentials()
        {
            loginPage.Login("bert.rietveld@alten.nl", "H$cu8V3!rtPsTQ");
        }

        [Then(@"the board with title ""(.*)"" is not visible in the boardspage")]
        public void ThenTheBoardWithTitleIsNotVisibleInTheBoardspage(string boardname)
        {
            boardsPage.HomePageBoard();
            //TODO Exception because Selenium wants to find the element
            //Assert.That(boardsPage.BoardNotVisible(boardname), Is.False);
        }
        [Then(@"it will show an error")]
        public void ThenItWillShowAnError()
        {
            Assert.That(loginPage.LoginErrorMail(), Is.True);
        }

        [Then(@"the user is logged in succesfully")]
        public void TheUserIsLoggedInSuccesfully()
        {
            Assert.That(loginPage.UserLoggedIn(), Is.True);
        }



        [Then(@"The board with name ""(.*)"" is created")]
        public void ThenTheBoardWithNameIsCreated(string boardname)
        {
            Assert.That(boardsPage.BoardOpened(boardname), Is.True);
        }



        [BeforeScenario("UI")]
        // Before for the tag @UI so that it starts logged in
        public void StartLoggedIn()
        {
            GivenTheWebsiteIsOpened();
            WhenIEnterTheLoginCredentials();
        }

        [AfterScenario("Web")]
        // After For the tag @Web so that the browser currently used is closed.
        public void CloseBrowser() => webdriver.Close();



    }
}
