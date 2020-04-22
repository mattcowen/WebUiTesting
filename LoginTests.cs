using DynamicsTest.PageObjects;
using DynamicsTest.SeleniumHelpers;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;

namespace DynamicsTest
{
    public class LoginTests
    {
        private readonly string _baseUrl = "https://crm422752.crm.dynamics.com/";
        private IWebDriver _driver;
        IConfiguration Configuration { get; set; }

        public LoginTests()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<LoginTests>();

            Configuration = builder.Build();

            try
            {
                _driver = new DriverFactory().Create();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Fact]
        public void LoginWithValidCredentialsShouldSucceed()
        {
            // Arrange
            // Act
            new LoginPage(_driver).LoginAsAdmin(_baseUrl, Configuration["adminPassword"]);

            // Assert
            //new LoginPage(_driver).LoginButton.Displayed.Should().BeTrue();
        }
    }
}
