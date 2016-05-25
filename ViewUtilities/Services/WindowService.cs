//-----------------------------------------------------------------------
// <copyright file="WindowService.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewUtilities
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    using ViewUtilities.Properties;

    /// <summary>
    /// WindowService class implementation.
    /// </summary>
    public static class WindowService
    {
        #region Fields

        /// <summary>
        /// The AutomaticToolTipEnabled dependency property.
        /// </summary>
        public static readonly DependencyProperty ShouldPersistLocationProperty = DependencyProperty.RegisterAttached(
            "ShouldPersistLocation",
            typeof(bool),
            typeof(WindowService),
            new FrameworkPropertyMetadata(ShouldPersistLocationChanged));

        #endregion

        #region Methods

        /// <summary>
        /// Sets the ShouldPersistLocation dependency property.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="enabled">If it's enabled.</param>
        public static void SetShouldPersistLocation(DependencyObject dependencyObject, bool enabled)
        {
            if (null == dependencyObject)
            {
                return;
            }

            dependencyObject.SetValue(ShouldPersistLocationProperty, enabled);
        }

        private static void ShouldPersistLocationChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (null == window || !((bool)e.NewValue))
            {
                return;
            }

            window.Initialized += OnWindowInitialized;
            window.Closing += OnWindowClosing;
        }

        private static void OnWindowInitialized(object sender, EventArgs eventArgs)
        {
            var window = sender as Window;
            if (null == window)
            {
                return;
            }

            Load(window, Settings.Default);
            AdjustSizeAndLocation(window);
        }

        private static void OnWindowClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            var window = sender as Window;
            if (null == window)
            {
                return;
            }

            Save(window, Settings.Default);
        }

        private static void Load(Window window, Settings settings)
        {
            window.Top = settings.WindowTop.HasValue ? settings.WindowTop.Value : window.Top;
            window.Left = settings.WindowLeft.HasValue ? settings.WindowLeft.Value : window.Left;
            window.Height = settings.WindowHeight.HasValue ? settings.WindowHeight.Value : window.Height;
            window.Width = settings.WindowWidth.HasValue ? settings.WindowWidth.Value : window.Width;
            window.SourceInitialized += (o, e) => window.WindowState = settings.WindowState;
        }

        private static void Save(Window window, Settings settings)
        {
            if (WindowState.Minimized != window.WindowState)
            {
                settings.WindowState = window.WindowState;
            }

            settings.WindowTop = window.Top;
            settings.WindowLeft = window.Left;
            settings.WindowHeight = window.Height;
            settings.WindowWidth = window.Width;
        }

        private static void AdjustSizeAndLocation(Window window)
        {
            var screenTop = SystemParameters.VirtualScreenTop;
            var screenLeft = SystemParameters.VirtualScreenLeft;
            var screenHeight = SystemParameters.VirtualScreenHeight;
            var screenWidth = SystemParameters.VirtualScreenWidth;

            window.Height = Math.Min(window.Height, screenHeight);
            window.Width = Math.Min(window.Width, screenWidth);

            if (window.Top < screenTop)
            {
                window.Top = screenTop;
            }
            else if ((window.Top + window.Height) > (screenTop + screenHeight))
            {
                window.Top = screenTop + screenHeight - window.Height;
            }

            if (window.Left < screenLeft)
            {
                window.Left = screenLeft;
            }
            else if ((window.Left + window.Width) > (screenLeft + screenWidth))
            {
                window.Left = screenLeft + screenWidth - window.Width;
            }
        }

        #endregion
    }
}
