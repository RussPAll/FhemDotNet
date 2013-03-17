using System;
using System.Linq;
using System.Xml;
using FhemDotNet.Repository.Exceptions;
using FhemDotNet.Repository.Mappers;
using FhemDotNet.Repository.Tests.Builders;
using NUnit.Framework;

namespace FhemDotNet.Repository.Tests.Mappers
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    public class FhemThermostatMapper_Tests
    {
        [Test]
        public void GetThermostatFromFhemEntry_ValidXmlNode_ReturnsNonNullThermostat()
        {
            var xmlString = FhemXmlBuilder.GetThermostat("testDevice");
            var xmlNode = GetXmlNode(xmlString);

            // Act
            var thermostat = FhemThermostatMapper.GetThermostatFromFhemEntry(xmlNode);

            // Assert
            Assert.That(thermostat, Is.Not.Null);
        }

        [Test]
        public void GetThermostat_XmlWithoutNodeName_ThrowsFhemMalformedResponseException()
        {
            // Arrange
            const string xmlString = "<FHZINFO><FHT_LIST><FHT></FHT></FHT_LIST></FHZINFO>";
            var xmlNode = GetXmlNode(xmlString);

            // Act
            Assert.Throws<FhemMalformedResponseException>(() => FhemThermostatMapper.GetThermostatFromFhemEntry(xmlNode));
        }

        [Test]
        public void GetThermostatList_SingleThermostat_ThermostatHasName()
        {
            // Arrange
            const string deviceName = "testDevice";
            var xmlString = FhemXmlBuilder.GetThermostat(deviceName);
            var xmlNode = GetXmlNode(xmlString);

            // Act
            var thermostat = FhemThermostatMapper.GetThermostatFromFhemEntry(xmlNode);

            // Assert
            Assert.AreEqual(deviceName, thermostat.Name);
        }

        [Test]
        public void GetThermostatList_SingleThermostat_ThermostatHasCorrectFlatProperties()
        {
            // Arrange
            var xmlString = FhemXmlBuilder.GetThermostat("testDevice");
            var xmlNode = GetXmlNode(xmlString);

            // Act
            var thermostat = FhemThermostatMapper.GetThermostatFromFhemEntry(xmlNode);

            // Assert
            Assert.AreEqual(11, thermostat.CurrentTemp.Value);
            Assert.AreEqual(DateTime.Parse("2010-10-11 02:20:22"), thermostat.CurrentTemp.Timestamp);
            Assert.AreEqual(12, thermostat.DesiredTemp.Value);
            Assert.AreEqual(DateTime.Parse("2010-10-11 03:30:33"), thermostat.DesiredTemp.Timestamp);
            Assert.AreEqual("10", thermostat.Actuator.Value);
            Assert.AreEqual(DateTime.Parse("2010-10-11 01:10:11"), thermostat.Actuator.Timestamp);
        }

        [Test]
        public void GetThermostatList_ThermostatWithoutTimingData_ReturnsEmptyTimingDataInstance()
        {
            // Arrange
            var xmlString = FhemXmlBuilder.GetThermostat("testDevice");
            var xmlNode = GetXmlNode(xmlString);

            // Act
            var thermostat = FhemThermostatMapper.GetThermostatFromFhemEntry(xmlNode);

            // Assert
            var dayPeriods = thermostat.Schedule[(int) DayOfWeek.Saturday].Periods;
            Assert.That(dayPeriods, Is.Empty);
        }
        [Test]
        // ReSharper disable PossibleInvalidOperationException
        public void GetThermostatList_ThermostatWithTimingData_ReturnsPopulatedTimingData()
        {
            // Arrange
            var xmlString = FhemXmlBuilder.GetThermostat("testDevice", true);
            var xmlNode = GetXmlNode(xmlString);

            // Act
            var thermostat = FhemThermostatMapper.GetThermostatFromFhemEntry(xmlNode);

            // Assert
            var dayPeriods = thermostat.Schedule[(int)DayOfWeek.Saturday].Periods.ToArray();
            Assert.AreEqual(8, dayPeriods[0].FromTime.Hour);
            Assert.AreEqual(10, dayPeriods[0].ToTime.Hour);
            Assert.AreEqual(16, dayPeriods[1].FromTime.Hour);
            Assert.AreEqual(18, dayPeriods[1].ToTime.Hour);
        }
        // ReSharper restore PossibleInvalidOperationException

        private static XmlNode GetXmlNode(string xmlString)
        {
            var document = new XmlDocument();
            document.LoadXml(xmlString);
            XmlNode xmlData = document.DocumentElement;
            return xmlData;
        }
    }
}
