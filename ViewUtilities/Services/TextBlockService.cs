//-----------------------------------------------------------------------
// <copyright file="TextBlockService.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewUtilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// TextBlockService class implementation.
    /// </summary>
    public static class TextBlockService
    {
        #region Fields

        /// <summary>
        /// The IsTextTrimmed dependency property key.
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "The example that I copied had this property as public static readonly.")]
        public static readonly DependencyPropertyKey IsTextTrimmedKey = DependencyProperty.RegisterAttachedReadOnly(
            "IsTextTrimmed",
            typeof(bool),
            typeof(TextBlockService),
            new PropertyMetadata(false));

        /// <summary>
        /// The IsTextTrimmed dependency property.
        /// </summary>
        public static readonly DependencyProperty IsTextTrimmedProperty = IsTextTrimmedKey.DependencyProperty;

        /// <summary>
        /// The AutomaticToolTipEnabled dependency property.
        /// </summary>
        public static readonly DependencyProperty AutomaticToolTipEnabledProperty = DependencyProperty.RegisterAttached(
            "AutomaticToolTipEnabled",
            typeof(bool),
            typeof(TextBlockService),
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

        private static readonly Dictionary<TextBlock, PropertyChangeNotifier> NotifierDictionary =
            new Dictionary<TextBlock, PropertyChangeNotifier>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="TextBlockService"/> class. 
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline",
            Justification = "Need to register for the SizeChanged event in the static constructor.")]
        static TextBlockService()
        {
            // Register for the SizeChanged event on all TextBlocks, even if the event was handled.
            EventManager.RegisterClassHandler(
                typeof(TextBlock),
                FrameworkElement.SizeChangedEvent,
                new SizeChangedEventHandler(OnTextBlockSizeChanged),
                true);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Cleans up the memory of the TextBlockService - to avoid memory leaks.  Call when new runs start.
        /// </summary>
        public static void CleanUpMemory()
        {
            var keys = NotifierDictionary.Select(keyValuePair => keyValuePair.Key).ToList();
            foreach (var key in keys)
            {
                NotifierDictionary.Remove(key);
            }
        }

        /// <summary>
        /// Event that runs when the size of the text block changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        public static void OnTextBlockSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var textBlock = sender as TextBlock;

            if (null == textBlock || (TextTrimming.None == textBlock.TextTrimming))
            {
                return;
            }

            PropertyChangeNotifier notifier;
            if (!NotifierDictionary.TryGetValue(textBlock, out notifier))
            {
                notifier = new PropertyChangeNotifier(textBlock, TextBlock.TextProperty);
                notifier.ValueChanged += OnTextBlockTextChanged;
                NotifierDictionary.Add(textBlock, notifier);
            }

            SetIsTextTrimmed(
                textBlock,
                TextTrimming.None != textBlock.TextTrimming && CalculateIsTextTrimmed(textBlock));
        }

        /// <summary>
        /// Event that runs when the text of the text block changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        public static void OnTextBlockTextChanged(object sender, EventArgs e)
        {
            var propertyChangeNotifier = sender as PropertyChangeNotifier;

            if (null == propertyChangeNotifier)
            {
                return;
            }

            var textBlock = propertyChangeNotifier.PropertySource as TextBlock;

            if (null == textBlock)
            {
                return;
            }

            SetIsTextTrimmed(
                textBlock,
                TextTrimming.None != textBlock.TextTrimming && CalculateIsTextTrimmed(textBlock));
        }

        /// <summary>
        /// Gets the value of the AutomaticToolTipEnabled property.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The boolean indicating if the value if the automatic tool tip is enabled.</returns>
        public static bool GetAutomaticToolTipEnabled(DependencyObject element)
        {
            if (null == element)
            {
                throw new ArgumentNullException("element");
            }

            return (bool)element.GetValue(AutomaticToolTipEnabledProperty);
        }

        /// <summary>
        /// Sets the value of the AutomaticToolTipEnabled property.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value to set.</param>
        public static void SetAutomaticToolTipEnabled(DependencyObject element, bool value)
        {
            if (null == element)
            {
                throw new ArgumentNullException("element");
            }

            element.SetValue(AutomaticToolTipEnabledProperty, value);
        }

        /// <summary>
        /// Gets the value of the IsTextTrimmed property.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>The boolean indicating if the value IsTextTrimmed.</returns>
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters",
            Justification = "This function only makes sense on a TextBlock")]
        public static bool GetIsTextTrimmed(TextBlock target)
        {
            if (null == target)
            {
                return false;
            }

            return (bool)target.GetValue(IsTextTrimmedProperty);
        }

        private static void SetIsTextTrimmed(TextBlock target, bool value)
        {
            target.SetValue(IsTextTrimmedKey, value);
        }

        private static bool CalculateIsTextTrimmed(TextBlock textBlock)
        {
            if (!textBlock.IsArrangeValid)
            {
                textBlock.UpdateLayout();
            }

            var typeface = new Typeface(
                textBlock.FontFamily,
                textBlock.FontStyle,
                textBlock.FontWeight,
                textBlock.FontStretch);

            // FormattedText is used to measure the whole width of the text held up by TextBlock container
            var formattedText = new FormattedText(
                textBlock.Text,
                System.Threading.Thread.CurrentThread.CurrentCulture,
                textBlock.FlowDirection,
                typeface,
                textBlock.FontSize,
                textBlock.Foreground);

            formattedText.MaxTextWidth = textBlock.ActualWidth;
            formattedText.Trimming = TextTrimming.None;

            // When the maximum text width of the FormattedText instance is set to the actual
            // width of the textBlock, if the textBlock is being trimmed to fit then the formatted
            // text will report a larger height than the textBlock. Should work whether the
            // textBlock is single or multi-line.
            return formattedText.Height > textBlock.ActualHeight;
        }

        #endregion
    }
}
