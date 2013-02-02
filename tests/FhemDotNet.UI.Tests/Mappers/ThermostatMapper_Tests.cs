using System;
using FhemDotNet.Host.Mappers;
using NUnit.Framework;
using FhemDotNet.Domain;

namespace FhemDotNet.UI.Tests.Mappers
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
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

        [Test]
        public void DomainToViewModel_DaySchedules_GeneratesCorrectViewModel()
        {
            // Arrange
            var timePeriod1 = new TimePeriod(new DateTime(2000, 1, 1, 8, 0, 0),
                                      new DateTime(2000, 1, 1, 12, 0, 0));

            var input = new Thermostat
                            {
                                Name = "DeviceName"
                            };
            input.Schedule[(int)DayOfWeek.Sunday].AddPeriod(timePeriod1);


            // Act
            var result = ThermostatMapper.DomainToViewModel(input);

            // Assert
            Assert.That(result.DaySchedules[6].Name, Is.EqualTo("Sunday"));
            Assert.That(result.DaySchedules[6].FromAM, Is.EqualTo("08:00"));
            Assert.That(result.DaySchedules[6].ToAM, Is.EqualTo("12:00"));
            Assert.That(result.DaySchedules[6].FromPM, Is.Empty);
            Assert.That(result.DaySchedules[6].ToPM, Is.Empty);
        }
    }
}
