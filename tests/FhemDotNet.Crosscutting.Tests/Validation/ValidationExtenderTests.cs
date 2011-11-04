using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FhemDotNet.CrossCutting.Validation;

namespace FhemDotNet.Crosscutting.Tests.Validation
{
    [TestFixture]
    public class ValidationExtenderTests
    {
        #region IsNotNull Tests

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNull_IsNull_ThrowsArgumentNullException()
        {
            object dummyObject = null;
            dummyObject.RequireArgument("dummyObject").IsNotNull();
        }

        [Test]
        public void IsNotNull_IsNotNull_NoExceptionThrown()
        {
            object dummyObject = new object();
            dummyObject.RequireArgument("dummyObject").IsNotNull();
        }

        #endregion

        #region IsNotNullOrEmpty Tests
        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNullOrEmpty_IsEmpty_ThrowsArgumentNullException()
        {
            string dummyString = string.Empty;
            dummyString.RequireArgument("dummyString").IsNotNullOrEmpty();
        }

        [Test]
        public void IsNotNullOrEmpty_IsNotEmpty_NoExceptionThrown()
        {
            string dummyString = "test";
            dummyString.RequireArgument("dummyString").IsNotNullOrEmpty();
        }
        #endregion

        #region IsBetween<DateTime> Tests
        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IsBetweenDateTime_IsLessThanRange_ArgumentOutOfRangeExceptionThrown()
        {
            DateTime dummyDate = new DateTime(2011, 1, 1, 0, 0, 0);
            DateTime minValue = new DateTime(2011, 1, 1, 0, 0, 1);
            dummyDate.RequireArgument("dummyDate").IsBetween(minValue, DateTime.MaxValue);
        }

        [Test]
        public void IsBetweenDateTime_IsEqualToLowerBound_NoExceptionThrown()
        {
            DateTime dummyDate = new DateTime(2011, 1, 1, 0, 0, 0);
            DateTime minValue = new DateTime(2011, 1, 1, 0, 0, 0);
            dummyDate.RequireArgument("dummyDate").IsBetween(minValue, DateTime.MaxValue);
        }

        [Test]
        public void IsBetweenDateTime_IsEqualToUpperBound_NoExceptionThrown()
        {
            DateTime dummyDate = new DateTime(2011, 1, 1, 0, 0, 0);
            DateTime maxValue = new DateTime(2011, 1, 1, 0, 0, 0);
            dummyDate.RequireArgument("dummyDate").IsBetween(DateTime.MinValue, maxValue);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IsBetweenDateTime_IsGreaterThanRange_ArgumentOutOfRangeExceptionThrown()
        {
            DateTime dummyDate = new DateTime(2011, 1, 1, 0, 0, 1);
            DateTime maxValue = new DateTime(2011, 1, 1, 0, 0, 0);
            dummyDate.RequireArgument("dummyDate").IsBetween(DateTime.MinValue, maxValue);
        }


        [Test]
        public void IsBetweenDateTime_IsWithinRange_NoExceptionThrown()
        {
            DateTime dummyDate = new DateTime(2011, 1, 1, 0, 0, 1);
            DateTime minValue = new DateTime(2011, 1, 1, 0, 0, 0);
            dummyDate.RequireArgument("dummyDate").IsBetween(minValue, DateTime.MaxValue);
        }

        #endregion
    }
}
