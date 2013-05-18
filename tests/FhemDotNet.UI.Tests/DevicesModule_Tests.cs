using System.Collections.Generic;
using System.Linq;
using FhemDotNet.Domain;
using FhemDotNet.Domain.Tests.Builders;
using FhemDotNet.Host;
using FhemDotNet.Host.Models;
using Moq;
using NUnit.Framework;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Testing;
using Nancy.ViewEngines;

namespace FhemDotNet.UI.Tests
{
    public class TestableNancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override IRootPathProvider RootPathProvider
        {
            get
            {
                return new Nancy.Testing.Fakes.FakeRootPathProvider();
            }
        }   
    }

    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class DevicesModule_Tests : NancyModuleTestBase
    {
        private Mock<IDevicesInteractor> _interactorMock;
        private ConfigurableBootstrapper _bootstrapperFake;
        private Browser _browserFake;

        [SetUp]
        public new void SetUp()
        {
            //var types = AppDomainAssemblyTypeScanner.TypesOf<IRootPathProvider>(ScanMode.ExcludeNancy);
            base.SetUp();
            //_browserFake = new Browser(new TestableNancyBootstrapper());

            _interactorMock = new Mock<IDevicesInteractor>();
            _interactorMock.Setup(x => x.GetDeviceList()).Returns(new ThermostatList());
            _bootstrapperFake = new ConfigurableBootstrapper(with =>
            {
                with.ViewFactory(ViewFactoryMock.Object);
                with.Module<DevicesModule>();
                with.Dependency(_interactorMock.Object);
            });
            _browserFake = new Browser(_bootstrapperFake);
        }

        [Test]
        public void GetDevices_InvokesDevicesInteractor()
        {
            // Arrange

            // Act
            _browserFake.Get("/devices");

            // Assert
            _interactorMock.Verify(x => x.GetDeviceList());
        }

        [Test]
        public void Index_SingleThermostatInRepository_ThermostatIsPopulated()
        {
            // Arrange
            var mockThermostat = ThermostatBuilder.Build();

            _interactorMock.Setup(x => x.GetDeviceList())
                .Returns(new ThermostatList { mockThermostat });

            // Act
            _browserFake.Get("/devices");

            // Assert
            ViewFactoryMock.Verify(x => x.RenderView(
                It.IsAny<string>(),
                It.Is<IEnumerable<ThermostatViewModel>>(therm => therm.First().Name == mockThermostat.Name),
                It.IsAny<ViewLocationContext>()));
        }
    }
    // ReSharper restore InconsistentNaming
}
