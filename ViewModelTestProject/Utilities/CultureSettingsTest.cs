//-----------------------------------------------------------------------
// <copyright file="CultureSettingsTest.cs" company="Owlet Care Inc">
//   Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewModelTestProject.Utilities
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using SystemInterface.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Ninject;
    using Ninject.MockingKernel.Moq;
    using ViewModel.Utilities;

    /// <summary>
    /// Unit tests for CultureSettingsTest.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable",
        Justification = "This is a test class, and won't be used by others.")]
    public class CultureSettingsTest
    {
        #region Fields

        private readonly MoqMockingKernel kernel = new MoqMockingKernel();
        private Mock<IDirectory> directoryMock;
        private CultureSettings cultureSettings;

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
        [TestInitialize]
        public void MyTestInitialize()
        {
            this.kernel.Reset();
            this.kernel.Bind<CultureSettings>().ToSelf().InTransientScope();

            this.directoryMock = this.kernel.GetMock<IDirectory>();
            this.cultureSettings = this.kernel.Get<CultureSettings>();
        }

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
            this.cultureSettings.SelectedSupportedCulture = new CultureInfo("ru");
            this.cultureSettings = this.kernel.Get<CultureSettings>();
            Assert.AreEqual(new CultureInfo("ru"), this.cultureSettings.SelectedSupportedCulture);
            this.cultureSettings.SelectedSupportedCulture = new CultureInfo("en-US");
            this.cultureSettings = this.kernel.Get<CultureSettings>();
            Assert.AreEqual(new CultureInfo("en-US"), this.cultureSettings.SelectedSupportedCulture);
        }

        #endregion

        #region Method Tests

        /// <summary>
        /// Tests the LoadSupportedCultures method.
        /// </summary>
        [TestMethod]
        public void LoadSupportedCulturesTest()
        {
            this.directoryMock.Setup(d => d.GetDirectories(It.IsAny<string>()))
                .Returns(new string[] { "????", ".", string.Empty, " ", "..", "en-GB", "ru" });
            this.cultureSettings.LoadSupportedCultures();
            Assert.AreEqual(3, this.cultureSettings.SupportedCultures.Count);
            Assert.AreEqual(new CultureInfo("en-US"), this.cultureSettings.SupportedCultures[0]);
            Assert.AreEqual(new CultureInfo("en-GB"), this.cultureSettings.SupportedCultures[1]);
            Assert.AreEqual(new CultureInfo("ru"), this.cultureSettings.SupportedCultures[2]);
        }

        #endregion

        #endregion
    }
}
