//using System;
//using FhemDotNet.Domain.Tests.Builders;
//using FhemDotNet.Host.Mappers;
//using NUnit.Framework;
//using FhemDotNet.Domain;

//namespace FhemDotNet.UI.Tests.Mappers
//{
//    [TestFixture]
//    // ReSharper disable InconsistentNaming
//    public class ThermostatMapper_Tests
//    {
//        [Test]
//        public void DomainToViewModel_NormalUse_GeneratesCorrectViewModel()
//        {
//            var input = new Thermostat
//            {
//                Name = "DeviceName",
//                CurrentTemp = new Measurement<float?>(10, DateTime.Now),
//                DesiredTemp = new Measurement<float?>(10, DateTime.Now),
//                Mode = new Measurement<ThermostatMode>(ThermostatMode.Manu, DateTime.Now),
//                Actuator = new Measurement<string>("75", DateTime.Now)
//            };

//            var result = ThermostatMapper.DomainToViewModel(input);
//            Assert.That(result.Name, Is.EqualTo(input.Name));
//            Assert.That(result.CurrentTemp.Value, Is.EqualTo(input.CurrentTemp.Value.ToString()));
//            Assert.That(result.DesiredTemp.Value, Is.EqualTo(input.DesiredTemp.Value));
//            Assert.That(result.Mode.Value, Is.EqualTo("Manu"));
//        }

//        [Test]
//        public void DomainToViewModel_DaySchedules_GeneratesCorrectViewModel()
//        {
//            // Arrange
//            var timePeriod1 = new TimePeriod(new DateTime(2000, 1, 1, 8, 0, 0),
//                                      new DateTime(2000, 1, 1, 12, 0, 0));

//            var input = ThermostatBuilder.Build()
//                                         .WithTimePeriod(DayOfWeek.Sunday, timePeriod1);

//            // Act
//            var result = ThermostatMapper.DomainToViewModel(input);

//            // Assert
//            Assert.That(result.DaySchedules[6].Name, Is.EqualTo("Sunday"));
//            Assert.That(result.DaySchedules[6].FromAM, Is.EqualTo("08:00"));
//            Assert.That(result.DaySchedules[6].ToAM, Is.EqualTo("12:00"));
//            Assert.That(result.DaySchedules[6].FromPM, Is.Empty);
//            Assert.That(result.DaySchedules[6].ToPM, Is.Empty);
//        }
//    }
//}
