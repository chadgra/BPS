//-----------------------------------------------------------------------
// <copyright file="NinjectBootstrapper.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace View
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
#if !DEBUG
    using System.Windows.Threading;
#endif
    using Caliburn.Micro;
    using Fluent;
    using Ninject;
#if !DEBUG
    using NLog;
#endif
    using ViewModel;
#if !DEBUG
    using ViewModel.Dialogs;
    using LogManager = NLog.LogManager;
#endif
    using ToolTipService = System.Windows.Controls.ToolTipService;

    /// <summary>
    /// NinjectBootstrapper class implementation.
    /// </summary>
    public class NinjectBootstrapper : BootstrapperBase, IDisposable
    {
        #region Fields

#if !DEBUG
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
#endif
        private IKernel kernel;
        private Func<DependencyObject, IEnumerable<FrameworkElement>> defaultElementLookup;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the NinjectBootstrapper class. 
        /// </summary>
        public NinjectBootstrapper()
        {
#if DEBUG
            Environment.SetEnvironmentVariable("Configuration", "Debug");
#else
            Environment.SetEnvironmentVariable("Configuration", "Release");
            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomainOnUnhandledException;
#endif

            // Set the tool tip duration to the max value.
            ToolTipService.ShowDurationProperty.OverrideMetadata(
                typeof(DependencyObject),
                new FrameworkPropertyMetadata(int.MaxValue));

            this.Initialize();
        }

        #endregion

        #region Destructors

        /// <summary>
        /// Finalizes an instance of the NinjectBootstrapper class.
        /// </summary>
        ~NinjectBootstrapper()
        {
            this.Dispose(false);
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

        /// <summary>
        /// The bulk of the clean-up code is done in this method.
        /// </summary>
        /// <param name="disposing">A parameter indicating whether we are disposing of the managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != this.kernel)
                {
                    this.kernel.Dispose();
                    this.kernel = null;
                }
            }
        }

        /// <summary>
        /// Configure the bootstrapper.
        /// </summary>
        protected override void Configure()
        {
            this.defaultElementLookup = BindingScope.GetNamedElements;
            BindingScope.GetNamedElements = k =>
                {
                    var namedElements = new List<FrameworkElement>();
                    namedElements.AddRange(this.defaultElementLookup(k));
                    var ribbon = this.LookForRibbon(k);
                    if (null != ribbon)
                    {
                        this.AppendRibbonNamedItem(ribbon, namedElements);
                    }

                    return namedElements;
                };

            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "View",
                DefaultSubNamespaceForViewModels = "ViewModel"
            };

            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);

            this.kernel = new StandardKernel();
            this.kernel.Load(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Get the instance of the object.
        /// </summary>
        /// <param name="service">The type to return.</param>
        /// <param name="key">The name of the instance</param>
        /// <returns>The instance of the object.</returns>
        protected override object GetInstance(Type service, string key)
        {
            if (null == service)
            {
                throw new ArgumentNullException("service");
            }

            return this.kernel.Get(service);
        }

        /// <summary>
        /// Get all instances of the object.
        /// </summary>
        /// <param name="service">The type to return.</param>
        /// <returns>All instances of the object.</returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            if (null == service)
            {
                throw new ArgumentNullException("service");
            }

            return this.kernel.GetAll(service);
        }

        /// <summary>
        /// Create the container.
        /// </summary>
        /// <param name="instance">The instances of the container.</param>
        protected override void BuildUp(object instance)
        {
            this.kernel.Inject(instance);
        }

        /// <summary>
        /// Makes sure that we display the correct view when we start
        /// </summary>
        /// <param name="sender">The source of this interrupt.</param>
        /// <param name="e">The event args.</param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            this.DisplayRootViewFor<MainWindowViewModel>();
        }

#if !DEBUG
        /// <summary>
        /// Handle exceptions that were not caught somewhere else in the application.
        /// </summary>
        /// <param name="sender">The source of the exception.</param>
        /// <param name="e">The event args that include the exception.</param>
        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (null != e)
            {
                this.HandledException(e.Exception);
            }
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            this.HandledException((Exception)e.ExceptionObject);
        }

        private void HandledException(Exception exception)
        {
            if (null == exception)
            {
                return;
            }

            while (null != exception.InnerException)
            {
                exception = exception.InnerException;
            }

            Logger.ErrorException("OnUnhandledException", exception);
            var windowManager = this.kernel.Get<IWindowManager>();
            Execute.OnUIThread(() => windowManager.ShowDialog(new ExceptionViewModel(exception)));

            Environment.Exit(-1);
        }
#endif

        private Ribbon LookForRibbon(DependencyObject k)
        {
            var contentControl = k as ContentControl;
            if (null != contentControl)
            {
                var child = contentControl.Content as DependencyObject;
                if (null != child)
                {
                    return this.LookForRibbon(child);
                }
            }

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(k); ++i)
            {
                var child = VisualTreeHelper.GetChild(k, i);
                var foundRibbon = child as Ribbon;
                if (null != foundRibbon)
                {
                    return foundRibbon;
                }

                foundRibbon = this.LookForRibbon(child);
                if (null != foundRibbon)
                {
                    return foundRibbon;
                }
            }

            return null;
        }

        private void AppendRibbonNamedItem(Ribbon ribbon, List<FrameworkElement> namedElements)
        {
            foreach (var ti in ribbon.Tabs)
            {
                foreach (var group in ti.Groups)
                {
                    namedElements.AddRange(this.defaultElementLookup(group));
                }
            }
        }

        #endregion
    }
}
