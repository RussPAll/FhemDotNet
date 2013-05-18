using Moq;
using NUnit.Framework;

namespace FhemDotNet.Domain.Tests
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class DevicesInteractor_Tests
    {
        [Test]
        public void GetDeviceList_NormalUse_InvokesProductRepository()
        {
            // Arrange
            var repositoryMock = new Mock<IThermostatRepository>();
            var devicesInteractor = new DevicesInteractor(repositoryMock.Object);

            // Act
            devicesInteractor.GetDeviceList();

            // Assert
            repositoryMock.Verify(x => x.GetThermostatList());
        }
    }
}
