using DynamicsTest.PageObjects;
using DynamicsTest.SeleniumHelpers;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System;
using System.Threading;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;

namespace DynamicsTest.Tests
{
    public class LoginTests : Specs
    {
        public LoginTests() : base(BrowserHost.Chrome) { }

        [Fact]
        public void LoginWithValidCredentialsShouldSucceed()
        {
            // Arrange
            // Act
            var landingPage = Browser.NavigateToInitial<LoginPage>(BaseUrl)
                .LoginAsAdmin("admin@CRM422752.onmicrosoft.com", Configuration["adminPassword"]);
                

            // Assert
            var actualPage = Assert.IsType<LandingPage>(landingPage);
            //Assert.EndsWith("/main.aspx?forceUCI=1&pagetype=apps", actualPage.Url);
            //Assert.Equal("My Apps", actualPage.Title);
        }

        [Fact]
        public void OpenCustomerServiceTest()
        {
            // Arrange
            // Act
            var landingPage = Browser.NavigateToInitial<LoginPage>(BaseUrl)
                .LoginAsAdmin("admin@CRM422752.onmicrosoft.com", Configuration["adminPassword"])
                .NavigateToCustomerService();

            // Assert
            var actualPage = Assert.IsType<LandingPage>(landingPage);
            //Assert.EndsWith("/main.aspx?forceUCI=1&pagetype=apps", actualPage.Url);
            //Assert.Equal("My Apps", actualPage.Title);
        }
    }
}
