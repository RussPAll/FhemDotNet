using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FhemDotNet.CrossCutting.Validation;

namespace FhemDotNet.Crosscutting
{
    [TestFixture]
    public class ExtensionsTests
{
        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void RequireArgument_ArgumentIsNull_ThrowsNullArgumentException()
        {
            object dummyObject = null;
            dummyObject.RequireArgument("dummyObject").IsNotNull();
        }

        [Test]
        public void RequireArgument_ArgumentIsNotNull_DoesNotThrowException()
        {
            object dummyObject = new object();
            dummyObject.RequireArgument("dummyObject").IsNotNull();
        }
    }
}
