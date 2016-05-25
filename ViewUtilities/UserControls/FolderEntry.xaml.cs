//-----------------------------------------------------------------------
// <copyright file="FolderEntry.xaml.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewUtilities
{
    using System.Windows;
    using System.Windows.Forms;

    /// <summary>
    /// Interaction logic for FolderEntry.xaml
    /// </summary>
    public partial class FolderEntry
    {
        #region Fields

        /// <summary>
        /// The dependency property for the Text property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(FolderEntry),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// The dependency property for the Description property.
        /// </summary>
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            "Description",
            typeof(string),
            typeof(FolderEntry),
            new PropertyMetadata(default(string)));

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FolderEntry class.
        /// </summary>
        public FolderEntry()
        {
            this.InitializeComponent();
        }

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
        /// Gets or sets the description.
        /// </summary>
        public string Description
        {
            get
            {
                return (string)GetValue(DescriptionProperty);
            }

            set
            {
                SetValue(DescriptionProperty, value);
            }
        }

        #endregion

        #region Methods

        private void BrowseFolder(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = this.Description;
                dialog.SelectedPath = this.Text;
                dialog.ShowNewFolderButton = true;
                var result = dialog.ShowDialog();

                if (DialogResult.OK != result)
                {
                    return;
                }

                this.Text = dialog.SelectedPath;
                var bindingExpression = this.GetBindingExpression(TextProperty);
                if (null != bindingExpression)
                {
                    bindingExpression.UpdateSource();
                }
            }
        }

        #endregion
    }
}
