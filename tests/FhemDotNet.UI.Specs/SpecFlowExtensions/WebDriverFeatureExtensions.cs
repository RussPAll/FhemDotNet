using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace FhemDotNet.UI.Spec.SpecFlowExtensions
{
    public static class WebDriverFeatureExtensions
    {
        public const string WebDriverKey = "WebDriver";

        public static IWebDriver WebDriver(this FeatureContext featureContext)
        {
            return featureContext[WebDriverKey] as IWebDriver;
        }

        public static void SetWebDriver(this FeatureContext featureContext, IWebDriver driver)
        {
            featureContext[WebDriverKey] = driver;
        }
    }
}
