//-----------------------------------------------------------------------
// <copyright file="Settings.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewUtilities.Properties
{
    using System;

    /// <summary>
    /// Settings class implementation.
    /// </summary>
    internal sealed partial class Settings
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Settings class.
        /// </summary>
        public Settings()
        {
            this.PropertyChanged += this.PropertyChangedEventHandler;
        }

        #endregion

        #region Methods

        private void PropertyChangedEventHandler(object sender, EventArgs e)
        {
            this.Save();
        }

        #endregion
    }
}
