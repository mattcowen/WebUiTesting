using DynamicsTest.SeleniumHelpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicsTest.PageObjects
{
    public abstract class Page
    {
        protected IWebDriver WebDriver { get; set; }

        public string Title => WebDriver.TitleWithWait();

        public string Url => WebDriver.Url;

        protected TPage NavigateTo<TPage>(By byLocator, Action<IWebElement> performAction = null)
            where TPage : Page, new()
        {
            var action = performAction ?? (e => e.Click());
            int attempts = 0;

            //while (attempts < 5)
            //{
            //    try
            //    {
                    var element = WebDriver.ElementWithWait(x => x.FindElement(byLocator), TimeSpan.FromSeconds(10));
                    action(element);
                    
            //    }
            //    catch { }
            //    attempts++;
            //}

            return new TPage { WebDriver = WebDriver };
        }

        internal static TPage NavigateToInitial<TPage>(IWebDriver webDriver, string startUpUrl)
             where TPage : Page, new()
        {
            if (webDriver == null) throw new ApplicationException("Please provide with an instance of web driver to proceed");
            if (string.IsNullOrWhiteSpace(startUpUrl)) throw new ApplicationException("Please provide with a start up url");

            webDriver.Navigate().GoToUrl(startUpUrl);

            return new TPage { WebDriver = webDriver };
        }


        protected TReturn ExecuteScriptAndReturn<TReturn>(string scriptToExecute)
        {
            var javascriptExecutor = (IJavaScriptExecutor)WebDriver;

            var untypedValue = javascriptExecutor.ExecuteScript($"return {scriptToExecute}");

            return (TReturn)Convert.ChangeType(untypedValue, typeof(TReturn));
        }

        
    }
}
