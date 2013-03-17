using System.Configuration;
using NUnit.Framework;
using FhemDotNet.CrossCutting;

namespace FhemDotNet.Crosscutting.Tests
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    public class ConfigHelperTests
    {
        [Test]
        public void FhemServerName_NoConfigAvailable_ThrowsException()
        {
            // ReSharper disable UnusedVariable
            Assert.Throws<ConfigurationErrorsException>(
                () => { string result = ConfigHelper.FhemServerName; });
            // ReSharper restore UnusedVariable
        }

        [Test]
        public void FhemServerPort_NoConfigAvailable_ReturnsPort7072()
        {
            // Act
            int result = ConfigHelper.FhemServerPort;

            // Assert
            Assert.AreEqual(7072, result);
        }

        #region GetAppSetting
        [Test, ExpectedException(typeof(ConfigurationErrorsException))]
        public void GetAppSetting_NotInConfigAndNoDefault_ThrowsException()
        {
            new ConfigHelper().GetAppSetting("NonExistentField", "");
        }

        [Test]
        public void GetAppSetting_NotInConfigAndDefault_ReturnsDefault()
        {
            // Arrange
            const string expectedValue = "Default";

            // Act
            string result = new ConfigHelper().GetAppSetting("NonExistentField", expectedValue);

            // Assert
            Assert.AreEqual(expectedValue, result);
        }
        
        #endregion

        #region GetIntAppSetting
        [Test]
        public void GetIntAppSetting_ConfigIsValidInt_ReturnsInt()
        {
            // Arrange
            const int expectedResult = 1000;

            // Act
            int actualResult = new ConfigHelper().GetIntAppSetting("NonExistentField", expectedResult);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test, ExpectedException(typeof(ConfigurationErrorsException))]
        public void GetIntAppSetting_ConfigIsInvalidInt_ThrowsException()
        {
            // Arrange
            const int expectedResult = 1000;

            // Act
            int actualResult = new BogusNumberConfigHelper().GetIntAppSetting("NonExistentField", expectedResult);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        #endregion
    }

    class BogusNumberConfigHelper : ConfigHelper
    {
        public override string GetAppSetting(string fieldName, string defaultValue)
        {
            return "bogusNumber";
        }
    }
}
