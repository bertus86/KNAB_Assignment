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
            webdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            loginPage = new LoginPage(webdriver);
            boardsPage = new BoardsPage(webdriver);
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

        [Then(@"it will show an error")]
        public void ThenItWillShowAnError()
        {
            Assert.That(loginPage.LoginErrorMail(), Is.True);
        }

        [Then(@"the user is logged in")]
        public void TheUserIsLoggedIn()
        {
            Assert.That(loginPage.UserLoggedIn(), Is.True);
        }

        [AfterScenario("Web")]
        // After For the tag @Web so that the browser currently used is closed.
        public void CloseBrowser() => webdriver.Close();
    }
}
