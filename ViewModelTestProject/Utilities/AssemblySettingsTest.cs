//-----------------------------------------------------------------------
// <copyright file="AssemblySettingsTest.cs" company="Owlet Care Inc">
//   Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewModelTestProject.Utilities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ViewModel.Utilities;

    /// <summary>
    /// Unit tests for AssemblySettingsTest.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AssemblySettingsTest
    {
        #region Fields

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get;
            set;
        }

        #endregion

        #region Test Attributes

        /// <summary>
        /// Use ClassInitialize to run code before running the first test in the class.
        /// </summary>
        ////[ClassInitialize]
        ////public static void MyClassInitialize(TestContext testContext)
        ////{
        ////}

        /// <summary>
        /// Use ClassCleanup to run code after all tests in a class have run.
        /// </summary>
        ////[ClassCleanup]
        ////public static void MyClassCleanup()
        ////{
        ////}

        /// <summary>
        /// Use TestInitialize to run code before running each test.
        /// </summary>
        ////[TestInitialize]
        ////public void MyTestInitialize()
        ////{
        ////}

        /// <summary>
        /// Use TestCleanup to run code after each test has run
        /// </summary>
        ////[TestCleanup]
        ////public void MyTestCleanup()
        ////{
        ////}

        #endregion

        #region Tests

        #region Constructor Tests

        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [TestMethod]
        public void ConstructorTest()
        {
            var assemblySettings = new AssemblySettings();
            Assert.AreEqual("1.0.0.0", assemblySettings.Version);
        }

        #endregion

        #region Property Tests

        /// <summary>
        /// Tests the FullName property.
        /// </summary>
        [TestMethod]
        public void FullNameTest()
        {
            var assemblySettings = new AssemblySettings();
            Assert.AreEqual("ViewModel.dll", assemblySettings.FullName);
        }

        /// <summary>
        /// Tests the Name property.
        /// </summary>
        [TestMethod]
        public void NameTest()
        {
            var assemblySettings = new AssemblySettings();
            Assert.AreEqual("ViewModel", assemblySettings.Name);
        }

        /// <summary>
        /// Tests the Version property.
        /// </summary>
        [TestMethod]
        public void VersionTest()
        {
            var assemblySettings = new AssemblySettings();
            Assert.AreEqual("1.0.0.0", assemblySettings.Version);
        }

        /// <summary>
        /// Tests the BranchDate property.
        /// </summary>
        [TestMethod]
        public void BranchDateTest()
        {
            var assemblySettings = new AssemblySettings();
            Assert.AreEqual(string.Empty, assemblySettings.BranchDate);
        }

        /// <summary>
        /// Tests the Branch property.
        /// </summary>
        [TestMethod]
        public void BranchTest()
        {
            var assemblySettings = new AssemblySettings();
            Assert.AreEqual(string.Empty, assemblySettings.Branch);
        }

        /// <summary>
        /// Tests the CommitDate property.
        /// </summary>
        [TestMethod]
        public void DateTest()
        {
            var assemblySettings = new AssemblySettings();
            Assert.IsTrue(new DateTime().Equals(assemblySettings.CommitDate));
        }

        /// <summary>
        /// Tests the Configuration property.
        /// </summary>
        [TestMethod]
        public void ConfigurationTest()
        {
            var assemblySettings = new AssemblySettings();
            Assert.IsNull(assemblySettings.Configuration);
        }

        /// <summary>
        /// Tests the GitId property.
        /// </summary>
        [TestMethod]
        public void GitIdTest()
        {
            var assemblySettings = new AssemblySettings();
            Assert.AreEqual(string.Empty, assemblySettings.GitId);
        }

        /// <summary>
        /// Tests the LocalModifications property.
        /// </summary>
        [TestMethod]
        public void LocalModificationsTest()
        {
            var assemblySettings = new AssemblySettings();
            Assert.IsFalse(assemblySettings.LocalModifications);
        }

        #endregion

        #endregion
    }
}
