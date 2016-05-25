//-----------------------------------------------------------------------
// <copyright file="PropertyChangeNotifier.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewUtilities
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// PropertyChangeNotifier class implementation.
    /// </summary>
    public sealed class PropertyChangeNotifier : DependencyObject, IDisposable
    {
        #region Fields

        /// <summary>
        /// Identifies the <see cref="SourceValue"/> dependency property
        /// </summary>
        public static readonly DependencyProperty SourceValueProperty = DependencyProperty.Register(
            "SourceValue",
            typeof(object),
            typeof(PropertyChangeNotifier),
            new FrameworkPropertyMetadata(null, OnPropertyChanged));

        private readonly WeakReference propertySource;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PropertyChangeNotifier class.
        /// </summary>
        /// <param name="propertySource">The source dependency property.</param>
        /// <param name="path">The name of the event of the property.</param>
        public PropertyChangeNotifier(DependencyObject propertySource, string path)
            : this(propertySource, new PropertyPath(path))
        {
        }

        /// <summary>
        /// Initializes a new instance of the PropertyChangeNotifier class.
        /// </summary>
        /// <param name="propertySource">The source dependency property.</param>
        /// <param name="property">The event of the property.</param>
        public PropertyChangeNotifier(DependencyObject propertySource, DependencyProperty property)
            : this(propertySource, new PropertyPath(property))
        {
        }

        /// <summary>
        /// Initializes a new instance of the PropertyChangeNotifier class.
        /// </summary>
        /// <param name="propertySource">The source dependency property.</param>
        /// <param name="property">The path to the event of the property.</param>
        public PropertyChangeNotifier(DependencyObject propertySource, PropertyPath property)
        {
            if (null == propertySource)
            {
                throw new ArgumentNullException("propertySource");
            }

            if (null == property)
            {
                throw new ArgumentNullException("property");
            }

            this.propertySource = new WeakReference(propertySource);
            var binding = new Binding();
            binding.Path = property;
            binding.Mode = BindingMode.OneWay;
            binding.Source = propertySource;
            BindingOperations.SetBinding(this, SourceValueProperty, binding);
        }

        #endregion

        #region Destructors

        /// <summary>
        /// Finalizes an instance of the PropertyChangeNotifier class.
        /// </summary>
        ~PropertyChangeNotifier()
        {
            this.Dispose(false);
        }

        #endregion

        #region Events

        /// <summary>
        /// An event that fires when the value changes.
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the source object of the event.
        /// </summary>
        public DependencyObject PropertySource
        {
            get
            {
                try
                {
                    // note, it is possible that accessing the target property
                    // will result in an exception so i’ve wrapped this check
                    // in a try catch
                    return this.propertySource.IsAlive ? this.propertySource.Target as DependencyObject : null;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of the property
        /// </summary>
        /// <seealso cref="SourceValueProperty"/>
        [Description("Returns/sets the value of the property")]
        [Category("Behavior")]
        [Bindable(true)]
        public object SourceValue
        {
            get
            {
                return this.GetValue(PropertyChangeNotifier.SourceValueProperty);
            }

            set
            {
                this.SetValue(PropertyChangeNotifier.SourceValueProperty, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Disposes of the managed and unmanaged resources used in this class and calls the garbage collector.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var notifier = (PropertyChangeNotifier)d;
            if (null != notifier.ValueChanged)
            {
                notifier.ValueChanged(notifier, EventArgs.Empty);
            }
        }

        /// <summary>
        /// The bulk of the clean-up code is done in this method.
        /// </summary>
        /// <param name="disposing">A parameter indicating whether we are disposing of the managed resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                BindingOperations.ClearBinding(this, SourceValueProperty);
            }
        }

        #endregion
    }
}
