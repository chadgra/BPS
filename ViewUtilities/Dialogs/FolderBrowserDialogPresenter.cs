//-----------------------------------------------------------------------
// <copyright file="FolderBrowserDialogPresenter.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewUtilities.Dialogs
{
    using System.Windows.Forms;
    using MvvmDialogs.Presenters;

    /// <summary>
    /// FolderBrowserDialogPresenter class implementation.
    /// </summary>
    public class FolderBrowserDialogPresenter : IDialogBoxPresenter<FolderBrowserDialogViewModel>
    {
        #region Methods

        /// <summary>
        /// Show the dialog.
        /// </summary>
        /// <param name="viewModel">The view model that contains the information to show.</param>
        public void Show(FolderBrowserDialogViewModel viewModel)
        {
            if (null == viewModel)
            {
                return;
            }

            var dialog = new FolderBrowserDialog();

            dialog.Description = viewModel.Description;
            dialog.RootFolder = viewModel.RootFolder;
            dialog.SelectedPath = viewModel.SelectedPath;
            dialog.ShowNewFolderButton = viewModel.ShowNewFolderButton;

            viewModel.Result = dialog.ShowDialog();

            viewModel.SelectedPath = dialog.SelectedPath;
        }

        #endregion
    }
}
