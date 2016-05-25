//-----------------------------------------------------------------------
// <copyright file="MessageBoxViewModel.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewUtilities.Dialogs
{
    using System.Windows.Forms;
    using MvvmDialogs.ViewModels;

    /// <summary>
    /// MessageBoxViewModel class implementation.
    /// </summary>
    public class MessageBoxViewModel : IDialogViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MessageBoxViewModel class.
        /// </summary>
        public MessageBoxViewModel()
        {
            this.Buttons = MessageBoxButtons.OK;
            this.Caption = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets one of the message box buttons values that specifies which buttons to display in the
        /// message box.
        /// </summary>
        public MessageBoxButtons Buttons
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the text to display in the title bar of the message box.
        /// </summary>
        public string Caption
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the message box default button values that specifies the default button for the message box.
        /// </summary>
        public MessageBoxDefaultButton DefaultButton
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the path and name of the help file to display when the user clicks the help button.
        /// </summary>
        public string HelpFilePath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets one of the message box icon values that specifies which icon to display in the message box.
        /// </summary>
        public MessageBoxIcon Icon
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the help navigator values.
        /// </summary>
        public HelpNavigator Navigator
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the message box options values that specifies which display and association options will be
        /// used for the message box. You may use 0 if you wish to use the defaults.
        /// </summary>
        public MessageBoxOptions Options
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the text to display in the message box.
        /// </summary>
        public string Text
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
