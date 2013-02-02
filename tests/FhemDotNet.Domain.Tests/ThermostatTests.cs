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
            var thermostat = new Thermostat
                                 {
                                     Name = "TestDevice",
                                     CurrentTemp = 10
                                 };
            string expectedToString = string.Format(Resources.ToString.Thermostat, thermostat.Name, thermostat.CurrentTemp);

            // Act
            string actualToString = thermostat.ToString();

            // Assert
            Assert.AreEqual(expectedToString, actualToString);
        }
    }
}
