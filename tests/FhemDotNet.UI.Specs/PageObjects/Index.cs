using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;

namespace FhemDotNet.UI.Specs.PageObjects
{
    internal class Index
    {
        private static class XPath
        {
            internal static By ThermostatRows()
            { return By.XPath(@"//table[@id='thermostatTable']//tr"); }
            internal static By ThermostatNameTd()
            { return By.XPath(@"//td[@class='thermostatName']"); }
            internal static By ThermostatCurrentTempTd()
            { return By.XPath(@"//td[@class='currentTemp']"); }
            internal static By ThermostatDesiredTempInput()
            { return By.XPath(@"//td[@class='desiredTemp']//input"); }
            internal static By ThermostatDesiredPendingTempInput()
            { return By.XPath(@"//span[@class='pendingDesiredTemp']"); }
            internal static By ThermostatModeSelect()
            { return By.XPath(@"//td[@class='mode']//select"); }
            internal static By SubmitButton()
            { return By.XPath(@"//form[@id='thermostats']//input[@type='submit']"); }
        }
        
        internal ReadOnlyCollection<IWebElement> GetThermostatList(IWebDriver driver)
        {
            var xPath = XPath.ThermostatRows();
            var rows = driver.FindElements(xPath);
            return rows;
        }

        internal string GetThermostatName(IWebElement thermostatRow)
        {
            var xPath = XPath.ThermostatNameTd();
            var cell = thermostatRow.FindElement(xPath);
            return cell.Text;
        }

        internal string GetThermostatCurrentTemp(IWebElement thermostatRow)
        {
            var xPath = XPath.ThermostatCurrentTempTd();
            var cell = thermostatRow.FindElement(xPath);
            return cell.Text;
        }

        internal string GetThermostatDesiredTemp(IWebElement thermostatRow)
        {
            var xPath = XPath.ThermostatDesiredTempInput();
            var input = thermostatRow.FindElement(xPath);
            return input.Value;
        }

        internal object GetThermostatPendingDesiredTemp(IWebElement thermostatRow)
        {
            var xPath = XPath.ThermostatDesiredPendingTempInput();
            var span = thermostatRow.FindElement(xPath);
            return span.Text;
        }

        internal string GetThermostatMode(IWebElement thermostatRow)
        {
            var xPath = XPath.ThermostatModeSelect();
            var mode = thermostatRow.FindElement(xPath);
            return mode.Value;
        }

        internal void Submit(IWebDriver driver)
        {
            var xPath = XPath.SubmitButton();
            driver.FindElement(xPath).Click();
        }

        internal void SetThermostatMode(IWebElement iWebElement, string mode)
        {
            var xPath = XPath.ThermostatModeSelect();
            var selectItem = new SelectElement(iWebElement.FindElement(xPath));
            selectItem.SelectByText(mode);
        }

        internal void SetDesiredTemp(IWebElement thermostatRow, double temp)
        {
            var xPath = XPath.ThermostatDesiredTempInput();
            var inputField = thermostatRow.FindElement(xPath);
            inputField.Clear();
            inputField.SendKeys(temp.ToString());
        }

        internal IWebElement GetThermostatRowByName(ReadOnlyCollection<IWebElement> thermostatList, string thermostatName)
        {
            foreach (var element in thermostatList)
            {
                string currentRowName = GetThermostatName(element);
                if (currentRowName.ToUpper() == thermostatName.ToUpper()) return element;
            }
            throw new NoSuchElementException("Unable to find thermostat " + thermostatName);
        }
    }
}
