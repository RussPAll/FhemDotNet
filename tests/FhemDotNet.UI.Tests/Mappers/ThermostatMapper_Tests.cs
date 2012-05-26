using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FhemDotNet.Domain;
using FhemDotNet.UI.Mappers;

namespace FhemDotNet.UI.Tests.Mappers
{
    [TestFixture]
    public class ThermostatMapper_Tests
    {
        [Test]
        public void DomainToViewModel_NormalUse_GeneratesCorrectViewModel()
        {
            var input = new Thermostat
            {
                Name = "DeviceName",
                CurrentTemp = 10,
                DesiredTemp = 10,
                Mode = ThermostatMode.Manu
            };

            var result = ThermostatMapper.DomainToViewModel(input);
            Assert.That(result.Name, Is.EqualTo(input.Name));
            Assert.That(result.CurrentTemp, Is.EqualTo(input.CurrentTemp.ToString()));
            Assert.That(result.DesiredTemp, Is.EqualTo(input.DesiredTemp.ToString()));
            Assert.That(result.Mode, Is.EqualTo("Manu"));
        }
    }
}
