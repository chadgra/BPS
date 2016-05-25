//-----------------------------------------------------------------------
// <copyright file="MessageBoxPresenter.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewUtilities.Dialogs
{
    using System.Windows.Forms;
    using MvvmDialogs.Presenters;

    /// <summary>
    /// MessageBoxViewModel class implementation.
    /// </summary>
    public class MessageBoxPresenter : IDialogBoxPresenter<MessageBoxViewModel>
    {
        #region Methods

        /// <summary>
        /// Show the dialog.
        /// </summary>
        /// <param name="viewModel">The view model that contains the information to show.</param>
        public void Show(MessageBoxViewModel viewModel)
        {
            if (null == viewModel)
            {
                return;
            }

            if (string.IsNullOrEmpty(viewModel.HelpFilePath))
            {
                viewModel.Result = MessageBox.Show(
                    viewModel.Text,
                    viewModel.Caption,
                    viewModel.Buttons,
                    viewModel.Icon,
                    viewModel.DefaultButton,
                    viewModel.Options);
            }
            else
            {
                viewModel.Result = MessageBox.Show(
                    viewModel.Text,
                    viewModel.Caption,
                    viewModel.Buttons,
                    viewModel.Icon,
                    viewModel.DefaultButton,
                    viewModel.Options,
                    viewModel.HelpFilePath,
                    viewModel.Navigator);
            }
        }

        #endregion
    }
}
