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
using TestStack.Seleno.PageObjects.Locators;
using By = TestStack.Seleno.PageObjects.Locators.By;

namespace DynamicsTest.PageObjects
{
    public class LoginPage : Page
    {
        public IWebElement SignInButton { get; set; }

        public IWebElement NextButton { get; set; }

        public IWebElement UserIdField { get; set; }

        public IWebElement PasswordField { get; set; }
        
        ///// <summary>
        ///// JQuery selector example
        ///// </summary>
        //public IWebElement LoginButton => WebDriver.FindElementByJQuery("input[name='btnSubmit']");

        public LoginPage BrittleLoginAsAdmin(string baseUrl, string password)
        {

            // https://crm422752.crm.dynamics.com/
            WebDriver.Navigate().GoToUrl(baseUrl);

            UserIdField.Clear();
            // sending a single quote is not possible with the Chrome Driver, it sends two single quotes!
            UserIdField.SendKeys("admin@CRM422752.onmicrosoft.com");

            NextButton.Click();

            PasswordField.Clear();
            PasswordField.SendKeys(password);

            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(5));
            wait.Until(f => f.FindElements(By.XPath("//div[@class='col-xs-24 no-padding-left-right button-container']/div/input")).FirstOrDefault());

            SignInButton = WebDriver.FindElements(By.Id("idSIButton9")).FirstOrDefault();
            SignInButton.Click();

            var staySignedInDialog = WebDriver.FindElement(By.XPath("//div[@role='heading']"));
            if(staySignedInDialog?.Text == "Stay signed in?")
            {
                wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(5));
                wait.Until(f => f.FindElements(By.XPath("//div[@class='col-xs-24 no-padding-left-right button-container']/div/input")).FirstOrDefault());

                var noButton = WebDriver.FindElements(By.Id("idBtn_Back")).FirstOrDefault();
                noButton.Click();
            }

            return this;
        }

        public LandingPage LoginAsAdmin(string username, string password)
        {

            // https://crm422752.crm.dynamics.com/
            WebDriver.ElementWithWait(x => x.FindElement(By.Id("i0116")), TimeSpan.FromSeconds(10)).Clear();
            WebDriver.ElementWithWait(x => x.FindElement(By.Id("i0116")), TimeSpan.FromSeconds(10)).SendKeys(username);
            WebDriver.ElementWithWait(x => x.FindElement(By.Id("i0116")), TimeSpan.FromSeconds(10)).SendKeys(Keys.Enter);

            // ugly
            Thread.Sleep(1000);

            WebDriver.FindElement(By.Id("i0118")).Clear();
            Thread.Sleep(500);
            WebDriver.ElementWithWait(x => x.FindElement(By.Id("i0118")), TimeSpan.FromSeconds(10))
                .SendKeys(password);

            Thread.Sleep(500);

            WebDriver.ElementWithWait(x => x.FindElement(By.Id("idSIButton9")), TimeSpan.FromSeconds(10))
                .Click();

            Thread.Sleep(3000);

            var staySignedInDialog = WebDriver.ElementWithWait(x => x.FindElement(By.Id("displayName")), TimeSpan.FromSeconds(10));
            if (staySignedInDialog?.Text == "admin@crm422752.onmicrosoft.com")
            {
                return NavigateTo<LandingPage>(By.Id("idBtn_Back"), e => e.Click());
            }

            return null;
        }


        //internal LoginPage EnterUsername(string username)
        //{

        //    WebDriver.ElementWithWait(x => x.FindElement(By.Id("i0116")), TimeSpan.FromSeconds(10)).Clear();
        //    WebDriver.ElementWithWait(x => x.FindElement(By.Id("i0116")), TimeSpan.FromSeconds(10)).SendKeys(username);
        //    WebDriver.ElementWithWait(x => x.FindElement(By.Id("i0116")), TimeSpan.FromSeconds(10)).SendKeys(Keys.Enter);

        //    return this;
        //}


        //internal LoginPage EnterPassword(string password)
        //{
        //    Thread.Sleep(1000);
            
        //    WebDriver.FindElement(By.Id("i0118")).Clear();
        //    Thread.Sleep(500);
        //    WebDriver.ElementWithWait(x => x.FindElement(By.Id("i0118")), TimeSpan.FromSeconds(10))
        //        .SendKeys(password);
            
        //    Thread.Sleep(500);
            
        //    return NavigateTo<LoginPage>(By.Id("idSIButton9"), e => e.Click());
        //}

        //internal LandingPage NotStayingSignedIn()
        //{
        //    Thread.Sleep(1000);

        //    var staySignedInDialog = WebDriver.ElementWithWait(x => x.FindElement(By.Id("displayName")), TimeSpan.FromSeconds(10));
        //    if (staySignedInDialog?.Text == "admin@crm422752.onmicrosoft.com")
        //    {
        //        return NavigateTo<LandingPage>(By.Id("idBtn_Back"), e => e.Click());
        //    }
            
        //    return null;
        //}

        //public void LoginAsNobody(string baseUrl)
        //{
        //    WebDriver.Navigate().GoToUrl(baseUrl);
        //    SignInLink.Click();

        //    UserIdField.Clear();
        //    UserIdField.SendKeys("nobody");

        //    PasswordField.Clear();
        //    PasswordField.SendKeys("blah");

        //    LoginButton.Click();
        //}

        //public LandingPage NavigateToLandingPage()
        //{
        //    return NavigateTo<BasketPage>(By.XPath($"//*[@type=\"hidden\"][@value={productId}]"), e => e.Submit());
        //}
    }
}
