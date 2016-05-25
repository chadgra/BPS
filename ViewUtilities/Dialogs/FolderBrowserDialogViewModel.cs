//-----------------------------------------------------------------------
// <copyright file="FolderBrowserDialogViewModel.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewUtilities.Dialogs
{
    using System;
    using System.Windows.Forms;
    using MvvmDialogs.ViewModels;

    /// <summary>
    /// FolderBrowserDialogViewModel class implementation.
    /// </summary>
    public class FolderBrowserDialogViewModel : IDialogViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FolderBrowserDialogViewModel class.
        /// </summary>
        public FolderBrowserDialogViewModel()
        {
            this.RootFolder = Environment.SpecialFolder.Desktop;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the descriptive text displayed above the tree view control in the dialog box.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the root folder where the browsing starts from.
        /// </summary>
        public Environment.SpecialFolder RootFolder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the path selected by the user.
        /// </summary>
        public string SelectedPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the New Folder button appears in the folder browser dialog box.
        /// </summary>
        public bool ShowNewFolderButton
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the dialog result value.
        /// </summary>
        public DialogResult Result
        {
            get;
            set;
        }

        #endregion
    }
}
