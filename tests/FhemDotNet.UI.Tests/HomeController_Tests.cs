//using FhemDotNet.Host;
//using FhemDotNet.Host.Models;

using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace FhemDotNet.UI.Tests
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    public class HomeController_Tests : NancyModuleTestBase
    {
        private ConfigurableBootstrapper _bootstrapperFake;
        private Browser _browserFake;
        
        [SetUp]
        public new void SetUp()
        {
            _browserFake = new Browser(new DefaultNancyBootstrapper());
            _bootstrapperFake = new ConfigurableBootstrapper(with => { });
            _browserFake = new Browser(_bootstrapperFake);
        }

    }
}
