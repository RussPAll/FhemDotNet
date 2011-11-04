using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using NUnit.Framework;
using FhemDotNet.UI.Spec.SpecFlowExtensions;
using OpenQA.Selenium.Support.UI;
using FhemDotNet.UI.Specs.PageObjects;

namespace FhemDotNet.UI.Spec.StepDefinitions
{
    [Binding]
    class ConfigureSteps
    {
        private Index _index;
        private IWebDriver _driver;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _index = new Index();
            _driver = FeatureContext.Current.WebDriver();
        }

        [Then(@"I can input a desired temperature ""(.*)""")]
        public void ThenICanInputADesiredTemp(double temp)
        {
            // Arrange
            var rows = _index.GetThermostatList(_driver);
            _index.SetDesiredTemp(rows[0], temp);
        }

        [Then(@"I can set the thermostat mode to ""(.*)""")]
        public void ThenICanSetTheThermostatModeTo(string mode)
        {
            var rows = _index.GetThermostatList(_driver);
            _index.SetThermostatMode(rows[0], mode);
        }

        //[When(@"When I have modified the thermostat ""(.*)""")]
        //public void WhenIHaveModifiedTheThermostat(string thermostatName)
        //{
        //    var thermostatList = _index.GetThermostatList(_driver);
        //    var thermostatRpw = _index.GetThermostatRowByName(thermostatList, thermostatName);
        //    _index.SetThermostatMode(thermostatRpw, "manu");
        //}

        [When(@"I have set the thermostat ""(.*)"" to temperature ""(.*)""")]
        public void WhenIHaveSetTheThermostatToTemp(string thermostatName, double temperature)
        {
            var thermostatList = _index.GetThermostatList(_driver);
            var thermostatRow = _index.GetThermostatRowByName(thermostatList, thermostatName);
            _index.SetDesiredTemp(thermostatRow, temperature);
        }

        [Then(@"the ""(.*)"" thermostat should show a pending desired temperature ""(.*)""")]
        public void ThenTheThermostatShouldShowPendingDesiredTemp(string thermostatName, double temperature)
        {
            var thermostatList = _index.GetThermostatList(_driver);
            var thermostatRow = _index.GetThermostatRowByName(thermostatList, thermostatName);
            Assert.AreEqual(temperature.ToString(), _index.GetThermostatPendingDesiredTemp(thermostatRow));
            _index.SetDesiredTemp(thermostatRow, temperature);
        }
    }
}
