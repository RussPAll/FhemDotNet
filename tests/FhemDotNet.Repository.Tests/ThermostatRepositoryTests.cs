using System.Collections.Generic;
using NUnit.Framework;
using FhemDotNet.Repository.Exceptions;
using Moq;
using FhemDotNet.Repository.Interfaces;
using FhemDotNet.Domain;
using FhemDotNet.Repository.Tests.Builders;

namespace FhemDotNet.Repository.Tests
{
    [TestFixture]
    public class ThermostatRpositoryTests
    {
        #region GetThermostatList
        private const int _serverTimeoutMilliseconds = 1000;

        [Test]
        public void GetThermostatList_NoThermostats_ReturnsEmptyList()
        {
            // Arrange
            Mock<ITelnetConnection> mockTelnet = new Mock<ITelnetConnection>();
            mockTelnet.Setup(x => x.Read()).Returns(FhemXmlBuilder.GetEmptyThermostatList);
            
            // Act
            ThermostatRepository repository = new ThermostatRepository(mockTelnet.Object, _serverTimeoutMilliseconds);
            IList<Thermostat> thermostatList = repository.GetThermostatList();

            // Assert
            Assert.AreEqual(0, thermostatList.Count);
        }

        [Test]
        public void GetThermostatList_SingleThermostat_ThermostatHasName()
        {
            // Arrange
            Mock<ITelnetConnection> mockTelnet = new Mock<ITelnetConnection>();
            mockTelnet.Setup(x => x.Read()).Returns(FhemXmlBuilder.GetThermostatList(1));

            // Act
            ThermostatRepository repository = new ThermostatRepository(mockTelnet.Object, _serverTimeoutMilliseconds);
            IList<Thermostat> thermostatList = repository.GetThermostatList();

            // Assert
            Assert.AreEqual("thermostat0", thermostatList[0].Name);
        }

        [Test]
        public void GetThermostatList_SingleThermostat_ThermostatHasCurrentStatus()
        {
            // Arrange
            Mock<ITelnetConnection> mockTelnet = new Mock<ITelnetConnection>();
            mockTelnet.Setup(x => x.Read()).Returns(FhemXmlBuilder.GetThermostatList(1));

            // Act
            ThermostatRepository repository = new ThermostatRepository(mockTelnet.Object, _serverTimeoutMilliseconds);
            IList<Thermostat> thermostatList = repository.GetThermostatList();

            // Assert
            Assert.AreEqual(11, thermostatList[0].CurrentTemp);
        }

        [Test]
        public void GetThermostatList_SingleThermostat_ThermostatHasDesiredStatus()
        {
            // Arrange
            Mock<ITelnetConnection> mockTelnet = new Mock<ITelnetConnection>();
            mockTelnet.Setup(x => x.Read()).Returns(FhemXmlBuilder.GetThermostatList(1));

            // Act
            ThermostatRepository repository = new ThermostatRepository(mockTelnet.Object, _serverTimeoutMilliseconds);
            IList<Thermostat> thermostatList = repository.GetThermostatList();

            // Assert
            Assert.AreEqual(12, thermostatList[0].DesiredTemp);
        }

        [Test]
        public void GetThermostatList_SingleThermostat_ReturnsThermostat()
        {
            // Arrange
            Mock<ITelnetConnection> mockTelnet = new Mock<ITelnetConnection>();
            mockTelnet.Setup(x => x.Read()).Returns(FhemXmlBuilder.GetThermostatList(1));

            // Act
            ThermostatRepository repository = new ThermostatRepository(mockTelnet.Object, _serverTimeoutMilliseconds);
            IList<Thermostat> thermostatList = repository.GetThermostatList();

            // Assert
            Assert.AreEqual(1, thermostatList.Count);
        }

        [Test]
        public void GetThermostatList_FourThermostats_ReturnsFourThermostats()
        {
            // Arrange
            Mock<ITelnetConnection> mockTelnet = new Mock<ITelnetConnection>();
            mockTelnet.Setup(x => x.Read()).Returns(FhemXmlBuilder.GetThermostatList(4));

            // Act
            ThermostatRepository repository = new ThermostatRepository(mockTelnet.Object, _serverTimeoutMilliseconds);
            IList<Thermostat> thermostatList = repository.GetThermostatList();

            // Assert
            Assert.AreEqual(4, thermostatList.Count);
        }

        [Test]
        public void GetThermostatList_EmptyStringReturned_ThrowsFhemEmptyResponseException()
        {
            // Arrange
            Mock<ITelnetConnection> mockTelnet = new Mock<ITelnetConnection>();

            // Act
            ThermostatRepository repository = new ThermostatRepository(mockTelnet.Object, _serverTimeoutMilliseconds);
            Assert.Throws<FhemEmptyResponseException>(() => repository.GetThermostatList());
        }

        [Test]
        public void GetThermostatList_MalformedStringReturned_ThrowsFhemServerException()
        {
            // Arrange
            Mock<ITelnetConnection> mockTelnet = new Mock<ITelnetConnection>();
            mockTelnet.Setup(x => x.Read()).Returns("<>Garbage Response");

            // Act
            ThermostatRepository repository = new ThermostatRepository(mockTelnet.Object, _serverTimeoutMilliseconds);
            Assert.Throws<FhemResponseTimeoutException>(() => repository.GetThermostatList());
        }

        #endregion
    }
}