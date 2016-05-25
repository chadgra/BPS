//-----------------------------------------------------------------------
// <copyright file="CultureSettings.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewModel.Utilities
{
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using SystemInterface.IO;
    using Caliburn.Micro;
    using Infralution.Localization.Wpf;
    using ViewModel.Properties;

    /// <summary>
    /// ApplicationCultureInfo class implementation.
    /// </summary>
    public class CultureSettings : PropertyChangedBase, ICultureSettings
    {
        #region Fields

        private readonly Settings settings;
        private readonly IDirectory directory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CultureSettings class.
        /// </summary>
        /// <param name="directory">An object that implements the IDirectory interface.</param>
        public CultureSettings(IDirectory directory)
        {
            this.settings = Settings.Default;
            this.directory = directory;
            this.SupportedCultures = new ObservableCollection<CultureInfo>();
            this.LoadSupportedCultures();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of the supported cultures.
        /// </summary>
        public ObservableCollection<CultureInfo> SupportedCultures
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the selected supported culture.
        /// </summary>
        public CultureInfo SelectedSupportedCulture
        {
            get
            {
                return this.settings.SelectedSupportedCulture;
            }

            set
            {
                this.settings.SelectedSupportedCulture = value;
                this.NotifyOfPropertyChange(() => this.SelectedSupportedCulture);
                CultureManager.UICulture = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load all the cultures that we support.
        /// </summary>
        public void LoadSupportedCultures()
        {
            var english = new CultureInfo("en-US");
            var subDirectories = this.directory.GetDirectories(".");
            this.SupportedCultures.Clear();

            foreach (var directoryName in subDirectories)
            {
                try
                {
                    var cultureName = Path.GetFileName(directoryName);
                    if (string.IsNullOrEmpty(cultureName))
                    {
                        continue;
                    }

                    var cultureInfo = CultureInfo.GetCultureInfo(cultureName);
                    this.SupportedCultures.Add(cultureInfo);
                }
                catch (CultureNotFoundException)
                {
                }
            }

            if (!this.SupportedCultures.Contains(english))
            {
                this.SupportedCultures.Insert(0, english);
            }
        }

        #endregion
    }
}
