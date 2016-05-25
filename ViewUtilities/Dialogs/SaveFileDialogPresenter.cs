//-----------------------------------------------------------------------
// <copyright file="SaveFileDialogPresenter.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewUtilities.Dialogs
{
    using System.Windows.Forms;
    using MvvmDialogs.Presenters;

    /// <summary>
    /// SaveFileDialogPresenter class implementation.
    /// </summary>
    public class SaveFileDialogPresenter : IDialogBoxPresenter<SaveFileDialogViewModel>
    {
        #region Methods

        /// <summary>
        /// Show the dialog.
        /// </summary>
        /// <param name="viewModel">The view model that contains the information to show.</param>
        public void Show(SaveFileDialogViewModel viewModel)
        {
            if (null == viewModel)
            {
                return;
            }

            var dialog = new SaveFileDialog();

            dialog.AddExtension = viewModel.AddExtension;
            dialog.AutoUpgradeEnabled = viewModel.AutoUpgradeEnabled;
            dialog.CheckFileExists = viewModel.CheckFileExists;
            dialog.CheckPathExists = viewModel.CheckPathExists;
            dialog.CreatePrompt = viewModel.CreatePrompt;
            dialog.DefaultExt = viewModel.DefaultExt;
            dialog.DereferenceLinks = viewModel.DereferenceLinks;
            dialog.FileName = viewModel.FileName;
            dialog.Filter = viewModel.Filter;
            dialog.FilterIndex = viewModel.FilterIndex;
            dialog.InitialDirectory = viewModel.InitialDirectory;
            dialog.OverwritePrompt = viewModel.OverwritePrompt;
            dialog.RestoreDirectory = viewModel.RestoreDirectory;
            dialog.ShowHelp = viewModel.ShowHelp;
            dialog.SupportMultiDottedExtensions = viewModel.SupportMultidottedExtensions;
            dialog.Title = viewModel.Title;
            dialog.ValidateNames = viewModel.ValidateNames;

            viewModel.Result = dialog.ShowDialog();

            viewModel.FileName = dialog.FileName;
            foreach (var fileName in dialog.FileNames)
            {
                viewModel.FileNames.Add(fileName);
            }

            viewModel.Filter = dialog.Filter;
            viewModel.InitialDirectory = dialog.InitialDirectory;
            viewModel.RestoreDirectory = dialog.RestoreDirectory;
            viewModel.Title = dialog.Title;
            viewModel.ValidateNames = dialog.ValidateNames;
        }

        #endregion
    }
}
