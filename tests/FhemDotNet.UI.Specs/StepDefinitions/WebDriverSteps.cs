using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using FhemDotNet.UI.Spec.SpecFlowExtensions;
using OpenQA.Selenium.Chrome;

namespace FhemDotNet.UI.Spec.StepDefinitions
{
    [Binding]
    class WebDriverSteps
    {
        [BeforeFeature("WebDriver")]
        public static void BeforeScenario()
        {
            Trace.WriteLine("Launching web browser");
            IWebDriver driver = new FirefoxDriver();
            Trace.WriteLine("Finished launching web browser");
            FeatureContext.Current.SetWebDriver(driver);
        }

        [AfterFeature("WebDriver")]
        public static void AfterScenario()
        {
            FeatureContext.Current.WebDriver().Quit();
        }
    }
}
