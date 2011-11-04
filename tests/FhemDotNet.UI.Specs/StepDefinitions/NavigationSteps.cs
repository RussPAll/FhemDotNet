using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using System.Configuration;
using TechTalk.SpecFlow;
using FhemDotNet.UI.Spec.SpecFlowExtensions;
using NUnit.Framework;
using FhemDotNet.UI.Specs.PageObjects;

namespace FhemDotNet.UI.Spec.StepDefinitions
{
    [Binding]
    class NavigationSteps
    {
        [When(@"I navigate to ""(.*)""")]
        public void WhenINavigateTo(string url)
        {
            IWebDriver driver = FeatureContext.Current.WebDriver();
            Uri testUri = new Uri(ConfigurationManager.AppSettings["TestSiteUrl"]);
            driver.Navigate().GoToUrl(testUri + url);
        }

        #region Homepage navigation

        [Given(@"I am on the ""thermostat list"" page")]
        public void GivenIAmOnTheHomepage()
        {
            WhenINavigateTo("/Home");
        }

        [Then(@"I should be on the ""thermostat list"" page")]
        public void ThenIShouldBeOnThePage()
        {
            string actualTitle = FeatureContext.Current.WebDriver().Title;
            Assert.AreEqual("FhemDotNet - Homepage", actualTitle);
        }

        #endregion

        [When(@"I have submitted the page")]
        [Then(@"I can submit the page")]
        public void WhenIHaveSubmittedThePage()
        {
            IWebDriver driver = FeatureContext.Current.WebDriver();
            new Index().Submit(driver);
        }

        //public void ThenICanSubmitThePage()
        //{
        //    WhenIHaveSubmittedThePage();
        //}
    }
}
