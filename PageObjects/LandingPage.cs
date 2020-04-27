using DynamicsTest.SeleniumHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using TestStack.Seleno.PageObjects.Locators;
using TestStack.Seleno.PageObjects.Actions;
using AngleSharp.Dom;

namespace DynamicsTest.PageObjects
{
    public class LandingPage : Page
    {
        internal LandingPage HomePageLink()
        {
            return NavigateTo<LandingPage>(By.XPath("//*[@id='id - 15']/div[2]/button[1]"), e => e.Click());
        }

        internal ServicePage NavigateToCustomerService()
        {

            //WebDriver.LoadjQuery(version:"latest");
            
            WebDriver = WebDriver.SwitchTo().Frame(WebDriver.WaitForElement(By.jQuery("#AppLandingPage")));

            //IExecutor exec = new Executor(WebDriver.GetJavaScriptExecutor(), new ElementFinder(this.WebDriver));
            //Wait js = new Wait(exec);

            //js.AjaxCallsToComplete();

            var apps = WebDriver.WaitForElement(By.Id("AppDetailsSec_1_Item_4"), x => x.TagName == "div");

            return NavigateTo<ServicePage>(By.Id("AppModuleTileSec_1_Item_4"), e => e.Click());
        }
    }
}
