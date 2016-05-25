//-----------------------------------------------------------------------
// <copyright file="EditableTextBlock.xaml.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewUtilities
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    using KeyEventArgs = System.Windows.Input.KeyEventArgs;

    /// <summary>
    /// Interaction logic for EditableTextBlock.xaml
    /// </summary>
    public partial class EditableTextBlock
    {
        #region Fields

        /// <summary>
        /// The dependency property for the Text property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(EditableTextBlock),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// The dependency property for the IsInEditMode property.
        /// </summary>
        public static readonly DependencyProperty IsInEditModeProperty = DependencyProperty.Register(
            "IsInEditMode",
            typeof(bool),
            typeof(EditableTextBlock),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the EditableTextBlock class.
        /// </summary>
        public EditableTextBlock()
        {
            this.InitializeComponent();
            this.TextBlock.Visibility = Visibility.Visible;
            this.TextBox.Visibility = Visibility.Hidden;
        }

        #endregion

        #region Events

        /// <summary>
        /// An event that fires when the text changes.
        /// </summary>
        public event EventHandler TextChanged =
            delegate
            {
            };

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is in edit more.
        /// </summary>
        public bool IsInEditMode
        {
            get
            {
                return (bool)GetValue(IsInEditModeProperty);
            }

            set
            {
                if (value == this.IsInEditMode)
                {
                    return;
                }

                this.SetValue(IsInEditModeProperty, value);
                if (value)
                {
                    this.EditText();
                }
                else
                {
                    this.SaveEdit();
                }
            }
        }

        #endregion

        #region Methods

        private void EditText()
        {
            this.TextBlock.Visibility = Visibility.Hidden;
            this.TextBox.Visibility = Visibility.Visible;
            this.TextBox.SelectAll();
            this.Dispatcher.BeginInvoke((Action)(() => this.TextBox.Focus()), DispatcherPriority.Render);
        }

        private void SaveEdit()
        {
            // Setting the Text isn't necessary for it to appear correctly (since the value is bound)
            // but it is necessary if any handler of the TextChanged event needs to look at the Text.
            this.Text = this.TextBox.Text;
            this.TextBlock.Visibility = Visibility.Visible;
            this.TextBox.Visibility = Visibility.Hidden;
            this.TextChanged(this, EventArgs.Empty);
        }

        private void TextBlockOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((MouseButton.Left == e.ChangedButton) && (2 == e.ClickCount))
            {
                this.IsInEditMode = true;
            }
        }

        private void TextBlockOnKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.F2 == e.Key)
            {
                this.IsInEditMode = true;
            }
        }

        private void TextBoxOnLostFocus(object sender, RoutedEventArgs e)
        {
            if (e is KeyboardFocusChangedEventArgs)
            {
                return;
            }

            this.IsInEditMode = false;
        }

        private void TextBoxOnKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                this.TextBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        #endregion
    }
}
