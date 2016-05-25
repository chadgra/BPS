//-----------------------------------------------------------------------
// <copyright file="DialogServices.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewModel.Utilities
{
    using System.Collections.ObjectModel;
    using Caliburn.Micro;
    using MvvmDialogs.ViewModels;

    /// <summary>
    /// DialogServices class implementation.
    /// </summary>
    public class DialogServices : IDialogServices
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DialogServices class.
        /// </summary>
        /// <param name="windowManager">The window manager from caliburn micro.</param>
        public DialogServices(IWindowManager windowManager)
        {
            this.WindowManager = windowManager;
            this.Dialogs = new ObservableCollection<IDialogViewModel>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the window manager.
        /// </summary>
        public IWindowManager WindowManager
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the collection of dialogs.
        /// </summary>
        public ObservableCollection<IDialogViewModel> Dialogs
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Show a modal dialog. This method is used to show .net and 3rd party dialogs that have had a
        /// view model and presenter created for them using the MvvmDialogs framework.
        /// </summary>
        /// <param name="viewModel">A view model that implements the IDialogViewModel interface.</param>
        public void ShowDialog(IDialogViewModel viewModel)
        {
            this.Dialogs.Add(viewModel);
        }

        /// <summary>
        /// Show a modal dialog. This method is used to show custom dialogs that were created using the
        /// caliburn micro framework.
        /// </summary>
        /// <param name="viewModel">A view model that implements the IScreen interface.</param>
        /// <returns>Returns true if the screen closed with okay.</returns>
        public bool? ShowDialog(IScreen viewModel)
        {
            return this.WindowManager.ShowDialog(viewModel);
        }

        /// <summary>
        /// Show a modeless dialog. This method is used to show custom dialogs that were created using the
        /// caliburn micro framework.
        /// </summary>
        /// <param name="viewModel">A view model that implements the IScreen interface.</param>
        public void ShowWindow(IScreen viewModel)
        {
            this.WindowManager.ShowWindow(viewModel);
        }

        /// <summary>
        /// Show a popup window at the mouse position. This method is used to show custom dialogs that were
        /// created using the caliburn micro framework.
        /// </summary>
        /// <param name="viewModel">A view model that implements the IScreen interface.</param>
        public void ShowPopup(IScreen viewModel)
        {
            this.WindowManager.ShowPopup(viewModel);
        }

        #endregion
    }
}
