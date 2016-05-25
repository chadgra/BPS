//-----------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewModel
{
    using System.Linq;
    using Caliburn.Micro;
    using ViewModel.Properties;
    using ViewModel.Utilities;
    using ViewUtilities.Dialogs;

    /// <summary>
    /// MainWindowViewModel class implementation.
    /// </summary>
    public class MainWindowViewModel : Screen
    {
        #region Fields

        private readonly Settings settings;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MainWindowViewModel class.
        /// </summary>
        /// <param name="assemblySettings">The assembly settings for the running assembly.</param>
        /// <param name="cultureSettings">The culture settings for the running assembly.</param>
        /// <param name="dialogServices">The dialog services for the running assembly.</param>
        public MainWindowViewModel(
            IAssemblySettings assemblySettings,
            ICultureSettings cultureSettings,
            IDialogServices dialogServices)
        {
            this.settings = Settings.Default;
            this.AssemblySettings = assemblySettings;
            this.CultureSettings = cultureSettings;
            this.DialogServices = dialogServices;

            // Initialize the data from the settings.
            this.TestData = this.TestData;
        }

        #endregion

        #region Events

        #endregion

        #region Properties

        /// <summary>
        /// Gets the assembly settings.
        /// </summary>
        public IAssemblySettings AssemblySettings
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the culture settings.
        /// </summary>
        public ICultureSettings CultureSettings
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the dialog services.
        /// </summary>
        public IDialogServices DialogServices
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets some test data.
        /// </summary>
        public string TestData
        {
            get
            {
                return this.settings.TestData;
            }

            set
            {
                this.settings.TestData = string.IsNullOrEmpty(value) ? null : value;
                this.NotifyOfPropertyChange(() => this.TestData);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Do something.
        /// </summary>
        public void DoSomething()
        {
            this.TestData = new string(this.TestData.ToCharArray().Reverse().ToArray());
            var messageBoxViewModel = new MessageBoxViewModel { Text = this.TestData };
            this.DialogServices.ShowDialog(messageBoxViewModel);
        }

        /// <summary>
        /// Set the culture when the view is loaded, or else there are problems with some of the translated text
        /// in the ribbon bar.
        /// </summary>
        /// <param name="view">The view that was loaded.</param>
        protected override void OnViewLoaded(object view)
        {
            this.CultureSettings.SelectedSupportedCulture = this.CultureSettings.SelectedSupportedCulture;
        }

        #endregion
    }
}
