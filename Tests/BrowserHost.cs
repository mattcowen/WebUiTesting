using DynamicsTest.PageObjects;
using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DynamicsTest.Tests
{
    public class BrowserHost : IDisposable
    {
        private IWebDriver _webdriver;

        private BrowserHost(IWebDriver webDriver)
        {
            _webdriver = webDriver;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomainDomainUnload;
            _webdriver.Manage().Window.Maximize();
        }

        private void CurrentDomainDomainUnload(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.DomainUnload -= CurrentDomainDomainUnload;
            Dispose();
        }


        public TPage NavigateToInitial<TPage>(string url)
          where TPage : Page, new()
        {
            return Page.NavigateToInitial<TPage>(_webdriver, url);
        }

        public static BrowserHost Chrome()
        {
            var options = new ChromeOptions();
            options.AddArgument("test-type");
            
            var directory = Directory.GetCurrentDirectory();
            
            var driver = new ChromeDriver(directory, options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //var directory = Environment.GetEnvironmentVariable("ChromeWebDriver");

            return new BrowserHost(driver);
        }

        public static BrowserHost EdgeChromium()
        {
            var options = new EdgeOptions();
            options.UseChromium = true;
            options.BinaryLocation = @"C:\Program Files (x86)\Microsoft\Edge Dev\Application\msedge.exe";

            //var directory = Environment.GetEnvironmentVariable("ChromeWebDriver");
            var directory = Directory.GetCurrentDirectory();

            return new BrowserHost(new EdgeDriver(directory, options));
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_webdriver != null)
                    {
                        _webdriver.Quit();
                        _webdriver.Dispose();
                        _webdriver = null;
                    }
                }
                disposedValue = true;
            }
        }


        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

    }
}
