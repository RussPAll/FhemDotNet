using Moq;
using NUnit.Framework;

namespace FhemDotNet.Domain.Tests
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    public class ThermostatList_Tests
    {
        [Test]
        public void Load_NormalUse_InvokesRepository()
        {
            // Arrange
            var repositoryMock = new Mock<IThermostatRepository>();

            // Act
            ThermostatList.Load(repositoryMock.Object);

            // Assert
            repositoryMock.Verify(x => x.GetThermostatList());
        }
    }
}
