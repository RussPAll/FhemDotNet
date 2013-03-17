using System;
using FhemDotNet.CrossCutting;
using NUnit.Framework;

namespace FhemDotNet.Crosscutting.Tests
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    public class ValidationTests
    {
        [Test]
        public void NotNullOrEmpty_EmptyString_ThrowsArgumentNullException()
        {
            var value = string.Empty;
            Assert.Throws<ArgumentNullException>(
                () => Validation.NotNullOrEmpty(() => value));
        }

        [Test]
        public void NotNullOrEmpty_NullString_ThrowsArgumentNullException()
        {
            string value = null;
            Assert.Throws<ArgumentNullException>(
                () => Validation.NotNullOrEmpty(() => value));
        }

        [Test]
        public void NotNullOrEmpty_NonEmptyString_DoesNotThrowArgumentNullException()
        {
            const string value = "blah";
            Assert.DoesNotThrow(
                () => Validation.NotNullOrEmpty(() => value));
        }
    }
}