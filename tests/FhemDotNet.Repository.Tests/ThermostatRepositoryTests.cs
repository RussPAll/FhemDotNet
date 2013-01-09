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
    public class ThermostatRepositoryTests
    {
        private Mock<ITelnetConnection> _mockTelnet;

        [SetUp]
        public void SetUp()
        {
            _mockTelnet = new Mock<ITelnetConnection>();
        }

        #region GetThermostatList

        [Test]
        public void GetThermostatList_NoThermostats_ReturnsEmptyList()
        {
            // Arrange
            _mockTelnet.Setup(x => x.Read()).Returns(FhemXmlBuilder.GetEmptyThermostatList);
            
            // Act
            var repository = new ThermostatRepository(_mockTelnet.Object);
            IList<Thermostat> thermostatList = repository.GetThermostatList();

            // Assert
            Assert.AreEqual(0, thermostatList.Count);
        }

        [Test]
        public void GetThermostatList_SingleThermostat_ThermostatHasName()
        {
            // Arrange
            _mockTelnet.Setup(x => x.Read()).Returns(FhemXmlBuilder.GetThermostatList(1));

            // Act
            var repository = new ThermostatRepository(_mockTelnet.Object);
            IList<Thermostat> thermostatList = repository.GetThermostatList();

            // Assert
            Assert.AreEqual("thermostat0", thermostatList[0].Name);
        }

        [Test]
        public void GetThermostatList_SingleThermostat_ThermostatHasCurrentStatus()
        {
            // Arrange
            _mockTelnet.Setup(x => x.Read()).Returns(FhemXmlBuilder.GetThermostatList(1));

            // Act
            var repository = new ThermostatRepository(_mockTelnet.Object);
            var thermostatList = repository.GetThermostatList();

            // Assert
            Assert.AreEqual(11, thermostatList[0].CurrentTemp);
        }

        [Test]
        public void GetThermostatList_SingleThermostat_ThermostatHasDesiredStatus()
        {
            // Arrange
            _mockTelnet.Setup(x => x.Read()).Returns(FhemXmlBuilder.GetThermostatList(1));

            // Act
            var repository = new ThermostatRepository(_mockTelnet.Object);
            var thermostatList = repository.GetThermostatList();

            // Assert
            Assert.AreEqual(12, thermostatList[0].DesiredTemp);
        }

        [Test]
        public void GetThermostatList_SingleThermostat_ReturnsThermostat()
        {
            // Arrange
            _mockTelnet.Setup(x => x.Read()).Returns(FhemXmlBuilder.GetThermostatList(1));

            // Act
            var repository = new ThermostatRepository(_mockTelnet.Object);
            IList<Thermostat> thermostatList = repository.GetThermostatList();

            // Assert
            Assert.AreEqual(1, thermostatList.Count);
        }

        [Test]
        public void GetThermostatList_FourThermostats_ReturnsFourThermostats()
        {
            // Arrange
            _mockTelnet.Setup(x => x.Read()).Returns(FhemXmlBuilder.GetThermostatList(4));

            // Act
            var repository = new ThermostatRepository(_mockTelnet.Object);
            var thermostatList = repository.GetThermostatList();

            // Assert
            Assert.AreEqual(4, thermostatList.Count);
        }

        [Test]
        public void GetThermostatList_EmptyStringReturned_ThrowsFhemEmptyResponseException()
        {
            // Arrange
            _mockTelnet.Setup(x => x.Read()).Returns(string.Empty);

            // Act
            var repository = new ThermostatRepository(_mockTelnet.Object);
            Assert.Throws<FhemEmptyResponseException>(() => repository.GetThermostatList());
        }

        [Test]
        public void GetThermostatList_MalformedStringReturned_ThrowsFhemResponseTimeoutException()
        {
            // Arrange
            _mockTelnet.Setup(x => x.Read()).Returns("<>Garbage Response");

            // Act
            var repository = new ThermostatRepository(_mockTelnet.Object);
            Assert.Throws<FhemResponseTimeoutException>(() => repository.GetThermostatList());
        }

        [Test]
        public void GetThermostatList_XmlWithoutNodeName_ThrowsFhemMalformedResponseException()
        {
            // Arrange
            _mockTelnet.Setup(x => x.Read()).Returns("<FHZINFO><FHT_LIST><FHT></FHT></FHT_LIST></FHZINFO>");

            // Act
            var repository = new ThermostatRepository(_mockTelnet.Object);
            Assert.Throws<FhemMalformedResponseException>(() => repository.GetThermostatList());
        }

        #endregion
    }
}