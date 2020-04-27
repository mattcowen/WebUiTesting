using System;
using System.Configuration;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using WebDriverManager;
using System.IO;
using Microsoft.Edge.SeleniumTools;

namespace DynamicsTest.SeleniumHelpers
{
    public enum DriverToUse
    {
        InternetExplorer,
        Chrome,
        Firefox,
        EdgeDev
    }

    public class DriverFactory
    {
        private static FirefoxOptions FirefoxOptions
        {
            get
            {
                var firefoxProfile = new FirefoxOptions();
                firefoxProfile.SetPreference("network.automatic-ntlm-auth.trusted-uris", "http://localhost");
                return firefoxProfile;
            }
        }

        public IWebDriver Create()
        {
            IWebDriver driver;
            var driverToUse = DriverToUse.EdgeDev; //ConfigurationHelper.Get<DriverToUse>("DriverToUse");

            switch (driverToUse)
            {
                case DriverToUse.EdgeDev:
                    //new DriverManager().SetUpDriver(
                    //    "https://msedgedriver.azureedge.net/84.0.488.1/edgedriver_win64.zip",
                    //    Path.Combine(Directory.GetCurrentDirectory(), "msedgedriver.exe"),
                    //    "msedgedriver.exe"
                    //);
                    
                    var options = new EdgeOptions();
                    options.UseChromium = true;
                    options.BinaryLocation = @"C:\Program Files (x86)\Microsoft\Edge Dev\Application\msedge.exe";

                    driver = new EdgeDriver(options);
                    
                    break;
                case DriverToUse.InternetExplorer:
                    driver = new InternetExplorerDriver(AppDomain.CurrentDomain.BaseDirectory, new InternetExplorerOptions(), TimeSpan.FromMinutes(5));
                    break;
                case DriverToUse.Firefox:
                    var firefoxProfile = FirefoxOptions;
                    driver = new FirefoxDriver(firefoxProfile);
                    driver.Manage().Window.Maximize();
                    break;
                case DriverToUse.Chrome:
                    driver = new ChromeDriver();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            driver.Manage().Window.Maximize();
            var timeouts = driver.Manage().Timeouts();

            timeouts.ImplicitWait = TimeSpan.FromSeconds(30);
            timeouts.PageLoad = TimeSpan.FromSeconds(60);

            // Suppress the onbeforeunload event first. This prevents the application hanging on a dialog box that does not close.
            ((IJavaScriptExecutor)driver).ExecuteScript("window.onbeforeunload = function(e){};");
            return driver;
        }
    }
}