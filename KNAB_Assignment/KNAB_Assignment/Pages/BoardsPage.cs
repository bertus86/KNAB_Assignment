using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace KNAB_Assignment.Pages
{
    class BoardsPage
    {
        
        public IWebDriver WebDriver { get; }
        //Method so it knows which browser to use in this page.
        public BoardsPage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public IWebElement BtnHome => WebDriver.FindElement(By.XPath("//a[@aria-label='Terug naar de startpagina']"));
        public IWebElement VerifyHome => WebDriver.FindElement(By.XPath("//h3[@class='boards-page-board-section-header-name']"));
        public IWebElement BtnCreateBoard => WebDriver.FindElement(By.XPath("//p/span[contains(text(),'Nieuw bord maken')]"));
        public IWebElement FldCreateBoardTitle => WebDriver.FindElement(By.XPath("//div/input[@placeholder='Bordtitel toevoegen']"));
        public IWebElement BtnCreateBoardFinal => WebDriver.FindElement(By.XPath("//button/span[contains(text(),'Maak Bord Aan')]"));
        public IWebElement BtnBoardMenu => WebDriver.FindElement(By.CssSelector("a[class='board-header-btn mod-show-menu js-show-sidebar']"));
        public IWebElement BtnBoardMenuMore => WebDriver.FindElement(By.CssSelector("a[class='board-menu-navigation-item-link js-open-more']"));
        public IWebElement BtnBoardMenuCloseBoard => WebDriver.FindElement(By.CssSelector("a[class='board-menu-navigation-item-link js-close-board']"));
        public IWebElement BtnBoardMenuCloseBoardSubmit => WebDriver.FindElement(By.XPath("//input[@class='js-confirm full negate'][@value='Sluiten']"));
        public IWebElement BtnBoardRemove => WebDriver.FindElement(By.XPath("//p[@class='delete-container']/a[contains(text(),'Bord permanent verwijderen...')]"));
        public IWebElement MsgBoardClosed(string title) => WebDriver.FindElement(By.XPath("//div[@class='big-message quiet closed-board']/h1[contains(text(),'" + title + " is gesloten.')]"));
        public IWebElement BtnBoardRemoveSubmit => WebDriver.FindElement(By.XPath("//input[@class='js-confirm full negate'][@value='Verwijder']"));
        public IWebElement FldBoardTitle(string title) => WebDriver.FindElement(By.XPath("//h1[@class='js-board-editing-target board-header-btn-text'][contains(text(),'" + title + "')]"));

        public IWebElement BtnBoard(string title) => WebDriver.FindElement(By.XPath("//div[@class='board-tile-details is-badged']/div[@title='" + title + "']"));

        //Terug naar de home pagina van Boards
        public void HomePageBoard()
        {
            BtnHome.Click();
            Assert.That(VerifyHome.Displayed, Is.True);
        }
        //create a new board
        public void CreateBoard(string title)
        {
            BtnCreateBoard.Click();
            FldCreateBoardTitle.SendKeys(title);
            BtnCreateBoardFinal.Click();
            Thread.Sleep(5000);
            Assert.That(BoardOpened(title), Is.True);
        }

        // Check if the board was created
        public bool BoardOpened(string title) => FldBoardTitle(title).Displayed;

        // Check if the board was closed
        public bool BoardClosed(string title) => MsgBoardClosed(title).Displayed;

        public void CloseAndRemoveBoard(string title)
        {
            Assert.That(BoardOpened(title), Is.True);
            BtnBoardMenuMore.Click();
            BtnBoardMenuCloseBoard.Click();
            BtnBoardMenuCloseBoardSubmit.Click();
            Assert.That(BoardClosed(title), Is.True);
            BtnBoardRemove.Click();
            BtnBoardRemoveSubmit.Click();
        }

        //Check if the board is not there anymore on the main board page
        public void BoardNotVisible(string boardtitle)
        {
            //TODO. method ExpectedConditions is depricated. Look for newer solution
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='board-tile-details is-badged']/div[@title='" + boardtitle + "']")));
        }
    }
}
