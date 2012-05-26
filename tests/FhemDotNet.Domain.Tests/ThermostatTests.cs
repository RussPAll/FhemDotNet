using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FhemDotNet.Domain.Tests
{
    [TestFixture]
    public class ThermostatTests
    {
        [Test]
        public void ToString_NormalUse_ReturnsExpectedString()
        {
            // Arrange
            Thermostat thermostat = new Thermostat();
            thermostat.Name = "TestDevice";
            thermostat.CurrentTemp = 10;
            string expectedToString = string.Format(Resources.ToString.Thermostat, thermostat.Name, thermostat.CurrentTemp);

            // Act
            string actualToString = thermostat.ToString();

            // Assert
            Assert.AreEqual(expectedToString, actualToString);
        }
    }
}
