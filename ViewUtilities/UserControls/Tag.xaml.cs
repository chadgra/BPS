//-----------------------------------------------------------------------
// <copyright file="Tag.xaml.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewUtilities
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for Tag.xaml
    /// </summary>
    public partial class Tag
    {
        #region Fields

        /// <summary>
        /// The dependency property for the Text property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(Tag),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Tag class.
        /// </summary>
        public Tag()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// An event that fires when the delete button is pressed.
        /// </summary>
        public event EventHandler<RoutedEventArgs> DeleteButtonPressed =
            delegate
            {
            };

        /// <summary>
        /// An event that fires when the delete button is pressed.
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

        #endregion

        #region Methods

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            this.DeleteButtonPressed(this, e);
        }

        private void EditableTextBlockOnTextChanged(object sender, EventArgs e)
        {
            this.TextChanged(this, EventArgs.Empty);
        }

        #endregion
    }
}
