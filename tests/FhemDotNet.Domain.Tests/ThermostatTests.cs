using System;
using NUnit.Framework;

namespace FhemDotNet.Domain.Tests
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    public class ThermostatTests
    {
        [Test]
        public void ToString_NormalUse_ReturnsExpectedString()
        {
            // Arrange
            var thermostat = new Thermostat
                                 {
                                     Name = "TestDevice",
                                     CurrentTemp = new Measurement<float?>(10, new DateTime(2013, 01, 01, 01, 02, 03))
                                 };
            const string expectedToString = "Thermostat TestDevice, CurrentTemp = 10 (2013-01-01 01:02:03)";

            // Act
            string actualToString = thermostat.ToString();

            // Assert
            Assert.AreEqual(expectedToString, actualToString);
        }
    }
}
