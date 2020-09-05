using KNAB_Assignment.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace KNAB_Assignment.Steps
{
    [Binding]
    public sealed class LoginSteps
    {
        LoginPage loginPage = null;

        [Given(@"the website is opened")]
        public void GivenTheWebsiteIsOpened()
        {
            IWebDriver webdriver = new ChromeDriver();
            webdriver.Navigate().GoToUrl("https://trello.com/login/");
            webdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            loginPage = new LoginPage(webdriver);

        }

        [When(@"I enter the wrong login credentials")]
        public void GivenIEnterMyLoginCredentials()
        {
            loginPage.Login("test@test", "123");
        }

        [Then(@"it will show an error")]
        public void ThenItWillShowAnError()
        {
            Assert.That(loginPage.LoginErrorMail(), Is.True);
        }

    }
}
