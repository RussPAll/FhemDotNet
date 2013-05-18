using System.Collections.Generic;
using System.Linq;
//using FhemDotNet.Host;
//using FhemDotNet.Host.Models;
using NUnit.Framework;
using Moq;
using FhemDotNet.Domain;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Testing;
using Nancy.ViewEngines;
using FhemDotNet.Domain.Tests.Builders;

namespace FhemDotNet.UI.Tests
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    public class HomeController_Tests : NancyModuleTestBase
    {
        private Mock<IThermostatRepository> _repositoryMock;
        private ConfigurableBootstrapper _bootstrapperFake;
        private Browser _browserFake;
        
        [SetUp]
        public new void SetUp()
        {
            _browserFake = new Browser(new DefaultNancyBootstrapper());

            _repositoryMock = new Mock<IThermostatRepository>();
            //_bootstrapperFake = new ConfigurableBootstrapper(with =>
            //{
            //    with.ViewFactory(ViewFactoryMock.Object);
            //    with.Module<HomeModule>();
            //    with.Dependency<IThermostatRepository>(_repositoryMock.Object);
            //});
            //_browserFake = new Browser(_bootstrapperFake);
        }

        [Test]
        public void Index_WhenCreated_CallsThermostatRepository()
        {
            // Arrange
            _repositoryMock.Setup(x => x.GetThermostatList()).Returns(new ThermostatList());

            // Act
            _browserFake.Get("/");

            // Assert
            _repositoryMock.Verify(x => x.GetThermostatList());
        }

        [Test]
        public void Index_SingleThermostatInRepository_ThermostatIsPopulated()
        {
            // Arrange
            var mockThermostat = ThermostatBuilder.Build();

            _repositoryMock.Setup(x => x.GetThermostatList())
                .Returns(new ThermostatList {mockThermostat});

            // Act
            _browserFake.Get("/");

            // Assert
            //ViewFactoryMock.Verify(x => x.RenderView(
            //    It.IsAny<string>(),
            //    It.Is<IEnumerable<ThermostatViewModel>>(therm => therm.First().Name == mockThermostat.Name),
            //    It.IsAny<ViewLocationContext>()));
        }

        [Test]
        public void Index_SingleThermostatInRepository_ViewReceivesViewModelType()
        {
            // Arrange
            _repositoryMock.Setup(x => x.GetThermostatList())
                .Returns(new ThermostatList());

            // Act
            _browserFake.Get("/");

            // Assert
            //ViewFactoryMock.Verify(x => x.RenderView(
            //    It.IsAny<string>(),
            //    It.IsAny<IEnumerable<ThermostatViewModel>>(),
            //    It.IsAny<ViewLocationContext>()));
        }
    }
}
