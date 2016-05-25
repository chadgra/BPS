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
            this.PortName = this.PortName;
            this.BluetoothAddresses = this.BluetoothAddresses;
            this.DiscoveryTimeout = this.DiscoveryTimeout;

            this.InitializeAtPort();
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
        /// Gets or sets the port name.
        /// </summary>
        public string PortName
        {
            get
            {
                return this.settings.PortName;
            }

            set
            {
                this.settings.PortName = string.IsNullOrEmpty(value) ? null : value;
                this.NotifyOfPropertyChange(() => this.PortName);
            }
        }

        /// <summary>
        /// Gets or sets the bluetooth addresses.
        /// </summary>
        public string BluetoothAddresses
        {
            get
            {
                return this.settings.BluetoothAddresses;
            }

            set
            {
                this.settings.BluetoothAddresses = string.IsNullOrEmpty(value) ? null : value;
                this.NotifyOfPropertyChange(() => this.BluetoothAddresses);
            }
        }

        /// <summary>
        /// Gets or sets the discovery timeout.
        /// </summary>
        public int DiscoveryTimeout
        {
            get
            {
                return this.settings.DiscoveryTimeout;
            }

            set
            {
                this.settings.DiscoveryTimeout = value;
                this.NotifyOfPropertyChange(() => this.DiscoveryTimeout);
            }
        }

        /// <summary>
        /// Gets the at port.
        /// </summary>
        public AtPort Port
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize the at port and set the important settings.
        /// </summary>
        public void InitializeAtPort()
        {
            this.Port = new AtPort(this.PortName, 115200);
            this.Port.SendCommand("ATFRST");
            this.Port.ReceiveEvent(1500);
            this.Port.ReceiveEvent(1500);
            this.Port.SendCommand("ATSDILE,1,0,0,0");
            this.Port.ReceiveResponse();
            this.Port.SendCommand("ATSDITLE," + this.DiscoveryTimeout + ",16,16");
            this.Port.ReceiveResponse();

            foreach (var address in this.BluetoothAddresses.Split(','))
            {
                this.Port.SendCommand("ATSWL," + address);
                this.Port.ReceiveResponse();
            }

            this.Port.SendCommand("ATSDBLE,2,0");
            this.Port.ReceiveResponse();
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
