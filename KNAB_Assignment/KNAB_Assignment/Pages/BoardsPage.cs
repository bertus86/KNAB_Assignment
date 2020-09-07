using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

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


    }
}
