//-----------------------------------------------------------------------
// <copyright file="OpenFileDialogPresenter.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewUtilities.Dialogs
{
    using System.Windows.Forms;
    using MvvmDialogs.Presenters;

    /// <summary>
    /// OpenFileDialogPresenter class implementation.
    /// </summary>
    public class OpenFileDialogPresenter : IDialogBoxPresenter<OpenFileDialogViewModel>
    {
        #region Methods

        /// <summary>
        /// Show the dialog.
        /// </summary>
        /// <param name="viewModel">The view model that contains the information to show.</param>
        public void Show(OpenFileDialogViewModel viewModel)
        {
            if (null == viewModel)
            {
                return;
            }

            var dialog = new OpenFileDialog();

            dialog.AddExtension = viewModel.AddExtension;
            dialog.AutoUpgradeEnabled = viewModel.AutoUpgradeEnabled;
            dialog.CheckFileExists = viewModel.CheckFileExists;
            dialog.CheckPathExists = viewModel.CheckPathExists;
            dialog.DefaultExt = viewModel.DefaultExt;
            dialog.DereferenceLinks = viewModel.DereferenceLinks;
            dialog.FileName = viewModel.FileName;
            dialog.Filter = viewModel.Filter;
            dialog.FilterIndex = viewModel.FilterIndex;
            dialog.InitialDirectory = viewModel.InitialDirectory;
            dialog.Multiselect = viewModel.Multiselect;
            dialog.ReadOnlyChecked = viewModel.ReadOnlyChecked;
            dialog.RestoreDirectory = viewModel.RestoreDirectory;
            dialog.ShowHelp = viewModel.ShowHelp;
            dialog.ShowReadOnly = viewModel.ShowReadOnly;
            dialog.SupportMultiDottedExtensions = viewModel.SupportMultidottedExtensions;
            dialog.Title = viewModel.Title;
            dialog.ValidateNames = viewModel.ValidateNames;

            viewModel.Result = dialog.ShowDialog();

            viewModel.Multiselect = dialog.Multiselect;
            viewModel.ReadOnlyChecked = dialog.ReadOnlyChecked;
            viewModel.ShowReadOnly = dialog.ShowReadOnly;
            viewModel.FileName = dialog.FileName;
            foreach (var fileName in dialog.FileNames)
            {
                viewModel.FileNames.Add(fileName);
            }

            viewModel.Filter = dialog.Filter;
            viewModel.InitialDirectory = dialog.InitialDirectory;
            viewModel.RestoreDirectory = dialog.RestoreDirectory;
            viewModel.SafeFileName = dialog.SafeFileName;
            foreach (var safeFileName in dialog.SafeFileNames)
            {
                viewModel.SafeFileNames.Add(safeFileName);
            }

            viewModel.Title = dialog.Title;
            viewModel.ValidateNames = dialog.ValidateNames;
        }

        #endregion
    }
}
