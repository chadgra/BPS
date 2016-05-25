//-----------------------------------------------------------------------
// <copyright file="MainWindowViewModelTest.cs" company="Owlet Care Inc">
//   Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewModelTestProject
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Ninject.MockingKernel.Moq;
    using ViewModel;
    using ViewModel.Utilities;
    using ViewUtilities.Dialogs;

    /// <summary>
    /// Unit tests for MainWindowViewModelTest.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable",
        Justification = "This is a test class, and won't be used by others.")]
    public class MainWindowViewModelTest
    {
        #region Fields

        private readonly MoqMockingKernel kernel = new MoqMockingKernel();
        private Mock<ICultureSettings> cultureSettingsMock;
        private Mock<IDialogServices> dialogServicesMock;

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
            this.cultureSettingsMock = this.kernel.GetMock<ICultureSettings>();
            this.dialogServicesMock = this.kernel.GetMock<IDialogServices>();
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
            var mainWindowViewModel1 = new MainWindowViewModel(null, null, null);

            // Set a value to clear out the previous value.
            mainWindowViewModel1.PortName = "first value";
            mainWindowViewModel1.PortName = "second value";

            var mainWindowViewModel2 = new MainWindowViewModel(null, null, null);
            Assert.AreEqual(mainWindowViewModel1.PortName, mainWindowViewModel2.PortName);
        }

        #endregion

        #region Property Tests

        /// <summary>
        /// Tests the TestData property.
        /// </summary>
        [TestMethod]
        public void TestDataTest()
        {
            const string TestData = "some data";
            var propertyEvents = 0;
            var mainWindowViewModel = new MainWindowViewModel(null, null, null);

            mainWindowViewModel.PropertyChanged += (o, e) =>
                {
                    propertyEvents++;
                };

            mainWindowViewModel.PortName = null;
            Assert.AreEqual(null, mainWindowViewModel.PortName);
            mainWindowViewModel.PortName = string.Empty;
            Assert.AreEqual(null, mainWindowViewModel.PortName);
            mainWindowViewModel.PortName = TestData;
            Assert.AreEqual(TestData, mainWindowViewModel.PortName);
            Assert.AreEqual(3, propertyEvents);
        }

        #endregion

        #region Method Tests

        /// <summary>
        /// Tests the DoSomething method.
        /// </summary>
        [TestMethod]
        public void DoSomethingTest()
        {
            var mainWindowViewModel = new MainWindowViewModel(null, null, this.dialogServicesMock.Object);
            mainWindowViewModel.PortName = "0123456789";
            Assert.AreEqual("0123456789", mainWindowViewModel.PortName);
            this.dialogServicesMock.Verify(
                dsm => dsm.ShowDialog(
                    It.Is<MessageBoxViewModel>(
                        m => m.Text == "9876543210")));
        }

        /// <summary>
        /// Tests the DoSomething method if an exception is thrown..
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DoSomethingExceptionTest()
        {
            var mainWindowViewModel = new MainWindowViewModel(null, null, null);
            mainWindowViewModel.PortName = null;
        }

        /// <summary>
        /// Tests the OnViewLoaded method.
        /// </summary>
        [TestMethod]
        public void OnViewLoadedTest()
        {
            var cultureInfo = new CultureInfo("ru");
            var mainWindowViewModel = new MainWindowViewModelAccessor(null, this.cultureSettingsMock.Object, null);
            this.cultureSettingsMock.SetupGet(csm => csm.SelectedSupportedCulture).Returns(cultureInfo);
            mainWindowViewModel.LoadView(null);
            this.cultureSettingsMock.VerifySet(csm => csm.SelectedSupportedCulture = cultureInfo);
        }

        #endregion

        #endregion

        #region Private Classes

        /// <summary>
        /// MainWindowViewModelAccessor class implementation.
        /// </summary>
        private class MainWindowViewModelAccessor : MainWindowViewModel
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the MainWindowViewModelAccessor class.
            /// </summary>
            /// <param name="assemblySettings">The assembly settings for the running assembly.</param>
            /// <param name="cultureSettings">The culture settings for the running assembly.</param>
            /// <param name="dialogServices">The dialog services for the running assembly.</param>
            public MainWindowViewModelAccessor(
                IAssemblySettings assemblySettings,
                ICultureSettings cultureSettings,
                IDialogServices dialogServices) :
                base(
                    assemblySettings,
                    cultureSettings,
                    dialogServices)
            {
            }

            #endregion

            #region Methods

            /// <summary>
            /// Simulate loading the view.
            /// </summary>
            /// <param name="view">The view to be loaded.</param>
            public void LoadView(object view)
            {
                this.OnViewLoaded(view);
            }

            #endregion
        }

        #endregion
    }
}
