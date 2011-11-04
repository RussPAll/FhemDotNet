using System;
using FhemDotNet.UI.Spec.SpecFlowExtensions;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using System.Configuration;
using System.Diagnostics;
using FhemDotNet.UI.Specs.PageObjects;

namespace FhemDotNet.UI.Spec.StepDefinitions
{
    [Binding]
    public class ViewSteps
    {
        private Index _index;
        private IWebDriver _driver;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _index = new Index();
            _driver = FeatureContext.Current.WebDriver();
        }

        [Then(@"I should see a list of thermostats")]
        public void ThenIShouldSeeAListOfThermostats()
        {
            var thermostatList = _index.GetThermostatList(_driver);
            Assert.Greater(thermostatList.Count, 0);
        }

        [Then(@"thermostats have a name")]
        public void ThenThermostatsHaveAName()
        {
            var thermostatRow = _index.GetThermostatList(_driver)[0];
            var thermostatName = _index.GetThermostatName(thermostatRow);

            Assert.Greater(thermostatName.Length, 0);
        }

        [Then(@"thermostats have a current temperature")]
        public void ThenThermostatsHaveACurrentTemperature()
        {
            var thermostatRow = _index.GetThermostatList(_driver)[0];
            var thermostatTemperature = _index.GetThermostatCurrentTemp(thermostatRow);

            Assert.Greater(thermostatTemperature.Length, 0);
        }

        [Then(@"thermostats have a mode")]
        public void ThenThermostatsHaveAMode()
        {
            var thermostatRow = _index.GetThermostatList(_driver)[0];
            var thermostatMode = _index.GetThermostatMode(thermostatRow);

            Assert.Greater(thermostatMode.Length, 0);
        }
    }
}
