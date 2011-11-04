using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Castle.Core;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.Windsor;

using FhemDotNet.UI.Installers;
using System.Web.Mvc;
using FhemDotNet.UI.Controllers;

namespace FhemDotNet.UI.Tests.InstallersTests
{
    [TestFixture]
    public class ControllersInstallersTests
    {
        private IWindsorContainer containerWithControllers;

        [SetUp]
        public void SetUp()
        {
            containerWithControllers = new WindsorContainer().Install(new ControllersInstaller());            
        }

        [Test]
        public void GetAssignableHandlers_AllControllers_EqualsAllIControllerDerivedControllers()
        {
            var allHandlers = GetAllHandlers(containerWithControllers);
            var controllerHandlers = GetHandlersFor(typeof(IController), containerWithControllers);

            Assert.IsNotEmpty(allHandlers);
            Assert.AreEqual(allHandlers, controllerHandlers);
        }

        [Test]
        public void GetAssignableHandlers_AllControllers_AllAvailableTypesAreAssigned()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Is<IController>());
            var registeredControllers = GetImplementationTypesFor(typeof(IController), containerWithControllers);

            Assert.AreEqual(allControllers, registeredControllers);
        }

        [Test]
        public void GetAssignableHandlers_AllControllers_AllControllersHaveControllersSuffix()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Name.EndsWith("Controller"));
            var registeredControllers = GetImplementationTypesFor(typeof(IController), containerWithControllers);
            Assert.AreEqual(allControllers, registeredControllers);
        }

        [Test]
        public void GetAssignableHandlers_AllControllers_AllControllersLiveInControllersNamespace()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Namespace.Contains("Controllers"));
            var registeredControllers = GetImplementationTypesFor(typeof(IController), containerWithControllers);
            Assert.AreEqual(allControllers, registeredControllers);
        }

        #region Helper methods

        private IHandler[] GetAllHandlers(IWindsorContainer container)
        {
            return GetHandlersFor(typeof(object), container);
        }

        private IHandler[] GetHandlersFor(Type type, IWindsorContainer container)
        {
            return container.Kernel.GetAssignableHandlers(type);
        }

        private Type[] GetImplementationTypesFor(Type type, IWindsorContainer container)
        {
            return GetHandlersFor(type, container)
                .Select(h => h.ComponentModel.Implementation)
                .OrderBy(t => t.Name)
                .ToArray();
        }

        private Type[] GetPublicClassesFromApplicationAssembly(Predicate<Type> where)
        {
            return typeof(HomeController).Assembly.GetExportedTypes()
                .Where(t => t.IsClass)
                .Where(t => t.IsAbstract == false)
                .Where(where.Invoke)
                .OrderBy(t => t.Name)
                .ToArray();
        }
        
        #endregion
    }
}
