//-----------------------------------------------------------------------
// <copyright file="OpenFileDialogViewModel.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewUtilities.Dialogs
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using MvvmDialogs.ViewModels;

    /// <summary>
    /// OpenFileDialogViewModel class implementation.
    /// </summary>
    public class OpenFileDialogViewModel : IDialogViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the OpenFileDialogViewModel class.
        /// </summary>
        public OpenFileDialogViewModel()
        {
            this.FileNames = new List<string>();
            this.SafeFileNames = new List<string>();
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
        /// Gets or sets a value indicating whether the dialog box allows multiple files to be selected.
        /// </summary>
        public bool Multiselect
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the read-only check box is selected.
        /// </summary>
        public bool ReadOnlyChecked
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
        /// Gets or sets the file name and extension for the file selected in the dialog box. The file name does
        /// not include the path.
        /// </summary>
        public string SafeFileName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets an array of file names and extensions for all the selected files in the dialog box.
        /// The file names do not include the path.
        /// </summary>
        public ICollection<string> SafeFileNames
        {
            get;
            private set;
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
        /// Gets or sets a value indicating whether the dialog box contains a read-only check box.
        /// </summary>
        public bool ShowReadOnly
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
