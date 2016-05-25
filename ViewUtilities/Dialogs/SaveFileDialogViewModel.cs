//-----------------------------------------------------------------------
// <copyright file="SaveFileDialogViewModel.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewUtilities.Dialogs
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using MvvmDialogs.ViewModels;

    /// <summary>
    /// SaveFileDialogViewModel class implementation.
    /// </summary>
    public class SaveFileDialogViewModel : IDialogViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SaveFileDialogViewModel class.
        /// </summary>
        public SaveFileDialogViewModel()
        {
            this.FileNames = new List<string>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box automatically adds an extension to a file
        /// name if the user omits the extension.
        /// </summary>
        public bool AddExtension
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this FileDialog instance should automatically upgrade
        /// appearance and behavior when running on Windows Vista.
        /// </summary>
        public bool AutoUpgradeEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box displays a warning if the user specifies
        /// a file name that does not exist.
        /// </summary>
        public bool CheckFileExists
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box displays a warning if the user specifies
        /// a path that does not exist.
        /// </summary>
        public bool CheckPathExists
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box prompts the user for permission to create
        /// a file if the user specifies a file that does not exist.
        /// </summary>
        public bool CreatePrompt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the default file name extension.
        /// </summary>
        public string DefaultExt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box returns the location of the file referenced by
        /// the shortcut or whether it returns the location of the shortcut (.link)
        /// </summary>
        public bool DereferenceLinks
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a string containing the file name selected in the file dialog box.
        /// </summary>
        public string FileName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the file names of all selected files in the dialog box
        /// </summary>
        public ICollection<string> FileNames
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the current file name filter string, which determines the choices that appear in the "Save
        /// as file type" or "Files of type" box in the dialog box.
        /// </summary>
        public string Filter
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the index of the filter currently selected in the file dialog box.
        /// </summary>
        public int FilterIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the initial directory displayed by the file dialog box.
        /// </summary>
        public string InitialDirectory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Save As dialog box restores the current directory
        /// before closing.
        /// </summary>
        public bool OverwritePrompt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box restores the current directory before closing.
        /// </summary>
        public bool RestoreDirectory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Help button is displayed in the file dialog box.
        /// </summary>
        public bool ShowHelp
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box supports displaying and saving files that
        /// have multiple file name extensions.
        /// </summary>
        public bool SupportMultidottedExtensions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the file dialog box title.
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box accepts only valid Win32 file names.
        /// </summary>
        public bool ValidateNames
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
