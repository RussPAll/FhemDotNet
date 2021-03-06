﻿using System;
using System.Threading;
using FhemDotNet.Repository.Interfaces;
using NUnit.Framework;
using FhemDotNet.CrossCutting;

namespace FhemDotNet.Repository.Tests
{
    [TestFixture, Category("Integration tests")]
    // ReSharper disable InconsistentNaming
    public class TelnetConnectionTests
    {
        #region Constructor tests
        [Test]
        public void TelnetRepository_ValidParameters_NoException()
        {
            var repository = GetTelnetConnection();
            Assert.IsNotNull(repository);
        }

        [Test]
        public void TelnetRepository_MissingHostname_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new TelnetConnection("", 0));
        }
        #endregion

        #region Read tests
        [Test]
        public void Read_ValidConnection_BlankResult()
        {
            var repository = GetTelnetConnection();
            string output = repository.Read();
            Assert.AreEqual(0, output.Length);
        }

        [Test]
        public void Read_AfterHelpCommand_ValidResult()
        {
            // Arrange
            ITelnetConnection repository = GetTelnetConnection();

            // Act
            repository.WriteLine("help");
            Thread.Sleep(500);
            string output = repository.Read();
            Assert.Greater(output.Length, 0);
        }
        #endregion

        #region Utility methods
        private static ITelnetConnection GetTelnetConnection()
        {
            return new TelnetConnection(ConfigHelper.FhemServerName, ConfigHelper.FhemServerPort);
        }
        #endregion
    }
}
