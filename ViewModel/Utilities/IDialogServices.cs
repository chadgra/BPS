//-----------------------------------------------------------------------
// <copyright file="IDialogServices.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewModel.Utilities
{
    using System.Collections.ObjectModel;
    using Caliburn.Micro;
    using MvvmDialogs.ViewModels;

    /// <summary>
    /// IDialogServices interface definition.
    /// </summary>
    public interface IDialogServices
    {
        #region Properties

        /// <summary>
        /// Gets the window manager.
        /// </summary>
        IWindowManager WindowManager
        {
            get;
        }

        /// <summary>
        /// Gets the collection of dialogs.
        /// </summary>
        ObservableCollection<IDialogViewModel> Dialogs
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Show a modal dialog. This method is used to show .net and 3rd party dialogs that have had a
        /// view model and presenter created for them using the MvvmDialogs framework.
        /// </summary>
        /// <param name="viewModel">A view model that implements the IDialogViewModel interface.</param>
        void ShowDialog(IDialogViewModel viewModel);

        /// <summary>
        /// Show a modal dialog. This method is used to show custom dialogs that were created using the
        /// caliburn micro framework.
        /// </summary>
        /// <param name="viewModel">A view model that implements the IScreen interface.</param>
        /// <returns>Returns true if the screen closed with okay.</returns>
        bool? ShowDialog(IScreen viewModel);

        /// <summary>
        /// Show a modeless dialog. This method is used to show custom dialogs that were created using the
        /// caliburn micro framework.
        /// </summary>
        /// <param name="viewModel">A view model that implements the IScreen interface.</param>
        void ShowWindow(IScreen viewModel);

        /// <summary>
        /// Show a popup window at the mouse position. This method is used to show custom dialogs that were
        /// created using the caliburn micro framework.
        /// </summary>
        /// <param name="viewModel">A view model that implements the IScreen interface.</param>
        void ShowPopup(IScreen viewModel);

        #endregion
    }
}
