//-----------------------------------------------------------------------
// <copyright file="ExceptionViewModelTest.cs" company="Owlet Care Inc">
//   Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewModelTestProject.Dialogs
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Net.Mail;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ViewModel.Dialogs;

    /// <summary>
    /// Unit tests for ExceptionViewModelTest.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ExceptionViewModelTest
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
            var exception = new ArgumentException("test message");
            var exceptionViewModel = new ExceptionViewModel(exception);
            Assert.AreEqual(exception, exceptionViewModel.Exception);
            Assert.AreEqual("software@owletcare.com", exceptionViewModel.DestinationAddress);
        }

        #endregion

        #region Property Tests

        /// <summary>
        /// Tests the Message property.
        /// </summary>
        [TestMethod]
        public void MessageTest()
        {
            const string Message = "some message";
            var exception = new ArgumentException(Message);
            var exceptionViewModel = new ExceptionViewModel(exception);
            Assert.AreEqual(exception.GetType().Name + ": " + Message, exceptionViewModel.Message);
        }

        /// <summary>
        /// Tests the StackTrace property.
        /// </summary>
        [TestMethod]
        public void StackTraceTest()
        {
            var exception = new ArgumentException("test message");
            var exceptionViewModel = new ExceptionViewModel(exception);
            Assert.AreEqual(exception.StackTrace, exceptionViewModel.StackTrace);
        }

        /// <summary>
        /// Tests the Data property.
        /// </summary>
        [TestMethod]
        public void DataTest()
        {
            const string Key = "key";
            const string Value = "value";
            var exception = new ArgumentException("test message");
            exception.Data.Add(Key, Value);
            var exceptionViewModel = new ExceptionViewModel(exception);
            Assert.IsTrue(exceptionViewModel.Data.Contains(Key));
            Assert.IsTrue(exceptionViewModel.Data.Contains(Value));
        }

        #endregion

        #region Method Tests

        /// <summary>
        /// Tests the SendEmail method.
        /// </summary>
        [TestMethod]
        public void SendEmailTest()
        {
            var exception = new ArgumentException("test message");
            var exceptionViewModel = new ExceptionViewModel(exception);
            var today = DateTime.Now;
            var fileName = Path.ChangeExtension(today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), "log");
            using (var stream = File.Create(fileName))
            {
                stream.WriteByte(20);
            }

            // Send to a real address so that it succeeds, but an inactive account so we don't spam anyone.
            exceptionViewModel.DestinationAddress = "test@owletcare.com";
            exceptionViewModel.SendEmail();
            Thread.Sleep(500);
            File.Delete(fileName);
        }

        /// <summary>
        /// Tests the SendEmail method when an SmtpException is thrown..
        /// </summary>
        [TestMethod]
        public void SendEmailExceptionTest()
        {
            var exception = new ArgumentException("test message");
            var exceptionViewModel = new ExceptionViewModel(exception);
            exceptionViewModel.SmtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;

            // Send to a real address so that it succeeds, but an inactive account so we don't spam anyone.
            exceptionViewModel.DestinationAddress = "test@owletcare.com";
            exceptionViewModel.SendEmail();
        }

        /// <summary>
        /// Tests the Exit method.
        /// </summary>
        [TestMethod]
        public void ExitTest()
        {
            // This test should Assert some values when the logic is expanded.
            var exception = new ArgumentException("test message");
            var exceptionViewModel = new ExceptionViewModel(exception);
            exceptionViewModel.Exit();
        }

        #endregion

        #endregion
    }
}
