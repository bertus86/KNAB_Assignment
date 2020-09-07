using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace KNAB_Assignment.Pages
{
    class LoginPage
    {
        public IWebDriver WebDriver { get; }
        //Method so it knows which browser to use in this page.
        public LoginPage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        // These are the Webelements that are used on for the Login Page
        public IWebElement BtnLogin => WebDriver.FindElement(By.Id("login"));
        public IWebElement BtnLoginAtlassian => WebDriver.FindElement(By.XPath("//input[@value='Log in with Atlassian']"));
        public IWebElement BtnLoginSubmit => WebDriver.FindElement(By.Id("login-submit"));
        public IWebElement BtnUserMenu => WebDriver.FindElement(By.XPath("//button[@aria-label='Open ledenmenu']"));
        public IWebElement FldUser => WebDriver.FindElement(By.Name("user"));
        public IWebElement FldPassword => WebDriver.FindElement(By.Name("password"));
        public IWebElement FldPasswordAtlassian => WebDriver.FindElement(By.Id("password"));
        public IWebElement MsgErrorMail => WebDriver.FindElement(By.XPath("//p[contains(text(),'an account for this email')]"));

        // Wrong login at the main login page.
        public void WrongLogin(string username, string password)
        {
            FldUser.SendKeys(username);
            FldPassword.SendKeys(password);
            // Sleep is introduced because the website takes some time to make the error visible. 
            // TODO Find out why this sleep is needed. Should be removed. Fluent wait?
            Thread.Sleep(500);
            BtnLogin.Click();
        }

        // Normal logging using Atlassian login
        public void Login(string username, string password)
        {
            FldUser.SendKeys(username);
            BtnLoginAtlassian.Click();
            FldPasswordAtlassian.SendKeys(password);
            BtnLoginSubmit.Click();
        }

        // Check if the error message is shown (There isn't an account for this email)
        public bool LoginErrorMail() => MsgErrorMail.Displayed;

        // Check if the user menu is visible (Check if logged in).
        public bool UserLoggedIn() => BtnUserMenu.Displayed;

    }
}
