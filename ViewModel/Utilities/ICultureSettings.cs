//-----------------------------------------------------------------------
// <copyright file="ICultureSettings.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewModel.Utilities
{
    using System.Collections.ObjectModel;
    using System.Globalization;

    /// <summary>
    /// ICultureSettings interface definition.
    /// </summary>
    public interface ICultureSettings
    {
        #region Properties

        /// <summary>
        /// Gets a list of the supported cultures.
        /// </summary>
        ObservableCollection<CultureInfo> SupportedCultures
        {
            get;
        }

        /// <summary>
        /// Gets or sets the selected supported culture.
        /// </summary>
        CultureInfo SelectedSupportedCulture
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load all the cultures that we support.
        /// </summary>
        void LoadSupportedCultures();

        #endregion
    }
}
