//-----------------------------------------------------------------------
// <copyright file="DialogServicesTest.cs" company="Owlet Care Inc">
//   Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewModelTestProject.Utilities
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Caliburn.Micro;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using MvvmDialogs.ViewModels;
    using Ninject;
    using Ninject.MockingKernel.Moq;
    using ViewModel.Utilities;

    /// <summary>
    /// Unit tests for DialogServicesTest.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable",
        Justification = "This is a test class, and won't be used by others.")]
    public class DialogServicesTest
    {
        #region Fields

        private readonly MoqMockingKernel kernel = new MoqMockingKernel();
        private Mock<IWindowManager> windowManagerMock;
        private DialogServices dialogServices;

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

            this.windowManagerMock = this.kernel.GetMock<IWindowManager>();
            this.dialogServices = this.kernel.Get<DialogServices>();
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
            Assert.IsNotNull(this.dialogServices.Dialogs);
            Assert.IsNotNull(this.dialogServices.WindowManager);
        }

        #endregion

        #region Method Tests

        /// <summary>
        /// Tests the ShowDialog method with an IDialogViewModel.
        /// </summary>
        [TestMethod]
        public void ShowDialogIDialogViewModelTest()
        {
            var viewModelMock = this.kernel.GetMock<IDialogViewModel>();
            this.dialogServices.ShowDialog(viewModelMock.Object);
            Assert.AreEqual(1, this.dialogServices.Dialogs.Count);
        }

        /// <summary>
        /// Tests the ShowDialog method with an IScreen.
        /// </summary>
        [TestMethod]
        public void ShowDialogIScreenTest()
        {
            var viewModelMock = this.kernel.GetMock<IScreen>();
            this.dialogServices.ShowDialog(viewModelMock.Object);
            this.windowManagerMock.Verify(
                wm =>
                    wm.ShowDialog(
                        It.Is<object>(obj => obj == viewModelMock.Object),
                        It.Is<object>(obj => obj == null),
                        It.Is<IDictionary<string, object>>(obj => obj == null)));
        }

        /// <summary>
        /// Tests the ShowWindow method with an IScreen.
        /// </summary>
        [TestMethod]
        public void ShowWindowIScreenTest()
        {
            var viewModelMock = this.kernel.GetMock<IScreen>();
            this.dialogServices.ShowWindow(viewModelMock.Object);
            this.windowManagerMock.Verify(
                wm =>
                    wm.ShowWindow(
                        It.Is<object>(obj => obj == viewModelMock.Object),
                        It.Is<object>(obj => obj == null),
                        It.Is<IDictionary<string, object>>(obj => obj == null)));
        }

        /// <summary>
        /// Tests the ShowPopup method with an IScreen.
        /// </summary>
        [TestMethod]
        public void ShowPopupIScreenTest()
        {
            var viewModelMock = this.kernel.GetMock<IScreen>();
            this.dialogServices.ShowPopup(viewModelMock.Object);
            this.windowManagerMock.Verify(
                wm =>
                    wm.ShowPopup(
                        It.Is<object>(obj => obj == viewModelMock.Object),
                        It.Is<object>(obj => obj == null),
                        It.Is<IDictionary<string, object>>(obj => obj == null)));
        }

        #endregion

        #endregion
    }
}
