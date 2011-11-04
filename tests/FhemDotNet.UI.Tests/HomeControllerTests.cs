using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Web.Mvc;
using FhemDotNet.UI.Controllers;
using System.Collections;
using Moq;
using FhemDotNet.Domain;
using FhemDotNet.Repository.Interfaces;

namespace FhemDotNet.UI.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public void Index_WhenCreated_ContainsThermostatList()
        {
            // Arrange
            Mock<IThermostatRepository> repositoryMock = new Mock<IThermostatRepository>();
            repositoryMock.Setup(x => x.GetThermostatList()).Returns(new List<Thermostat>());
            // Act
            var viewResult = (ViewResult)new HomeController(repositoryMock.Object).Index();
            // Assert
            Assert.IsNotNull((ICollection)viewResult.ViewData.Model);
        }

        [Test]
        public void Index_SingleThermostatInRepository_ThermostatIsPopulated()
        {
            // Arrange
            Thermostat mockThermostat = new Thermostat
            {
                Name = "TestThermostat",
                CurrentTemp = "20 deg c",
                DesiredTemp = "19 deg c"
            };

            Mock<IThermostatRepository> thermostatRepository = new Mock<IThermostatRepository>();
            thermostatRepository.Setup(x => x.GetThermostatList())
                .Returns(new List<Thermostat> {mockThermostat});

            // Act
            var viewResult = (ViewResult)new HomeController(thermostatRepository.Object).Index();
            List<Thermostat> model = (List<Thermostat>)viewResult.ViewData.Model;

            // Assert
            Assert.AreEqual(mockThermostat, model.First());
        }


    }
}
