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


        public LoginPage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        // These are the Webelements that are used on for the Login Page
        public IWebElement FldUser => WebDriver.FindElement(By.Name("user"));
        public IWebElement FldPassword => WebDriver.FindElement(By.Name("password"));
        public IWebElement BtnLogin => WebDriver.FindElement(By.Id("login"));
        public IWebElement MsgErrorMail => WebDriver.FindElement(By.XPath("//p[contains(text(),'an account for this email')]"));

        public void Login(string username, string password)
        {
            FldUser.SendKeys(username);
            FldPassword.SendKeys(password);
            Thread.Sleep(500);
            BtnLogin.Click();
        }

        // Check if the error message is shown (There isn't an account for this email)
        public bool LoginErrorMail() => MsgErrorMail.Displayed;

    }
}
