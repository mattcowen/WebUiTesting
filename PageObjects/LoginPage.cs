using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Support.PageObjects;
using DynamicsTest.SeleniumHelpers;
using WebDriverManager.DriverConfigs.Impl;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace DynamicsTest.PageObjects
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {

            _driver = driver;
        }

        public IWebElement SignInButton { get; set; }

        public IWebElement NextButton 
        {
            get
            {
                return _driver.FindElements(By.Id("idSIButton9")).FirstOrDefault();
            }
        }

        public IWebElement UserIdField 
        { 
            get
            { 
                return _driver.FindElements(By.Id("i0116")).FirstOrDefault(); 
            }
        }

        public IWebElement PasswordField { get; set; }


        ///// <summary>
        ///// JQuery selector example
        ///// </summary>
        //public IWebElement LoginButton => _driver.FindElementByJQuery("input[name='btnSubmit']");

        public void LoginAsAdmin(string baseUrl, string password)
        {

            // https://crm422752.crm.dynamics.com/
            _driver.Navigate().GoToUrl(baseUrl);

            UserIdField.Clear();
            // sending a single quote is not possible with the Chrome Driver, it sends two single quotes!
            UserIdField.SendKeys("admin@CRM422752.onmicrosoft.com");

            NextButton.Click();

            PasswordField = _driver.FindElements(By.Id("i0118")).FirstOrDefault();
            PasswordField.Clear();
            PasswordField.SendKeys(password);

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(f => f.FindElements(By.XPath("//div[@class='col-xs-24 no-padding-left-right button-container']/div/input")).FirstOrDefault());

            SignInButton = _driver.FindElements(By.Id("idSIButton9")).FirstOrDefault();
            SignInButton.Click();

            var staySignedInDialog = _driver.FindElement(By.XPath("//div[@role='heading']"));
            if(staySignedInDialog?.Text == "Stay signed in?")
            {
                wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                wait.Until(f => f.FindElements(By.XPath("//div[@class='col-xs-24 no-padding-left-right button-container']/div/input")).FirstOrDefault());

                var noButton = _driver.FindElements(By.Id("idBtn_Back")).FirstOrDefault();
                noButton.Click();
            }

        }

        //public void LoginAsNobody(string baseUrl)
        //{
        //    _driver.Navigate().GoToUrl(baseUrl);
        //    SignInLink.Click();

        //    UserIdField.Clear();
        //    UserIdField.SendKeys("nobody");

        //    PasswordField.Clear();
        //    PasswordField.SendKeys("blah");

        //    LoginButton.Click();
        //}
    }
}
