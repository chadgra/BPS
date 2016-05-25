//-----------------------------------------------------------------------
// <copyright file="BindingModule.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace View
{
    using SystemInterface;
    using SystemInterface.IO;
    using SystemWrapper;
    using SystemWrapper.IO;
    using Caliburn.Micro;
    using Ninject.Modules;
    using ViewModel;
    using ViewModel.Utilities;

    /// <summary>
    /// BindingModule class implementation.
    /// </summary>
    public class BindingModule : NinjectModule
    {
        #region Methods

        /// <summary>
        /// Load the default bindings.
        /// </summary>
        public override void Load()
        {
            // CaliburnMicro bindings.
            this.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            this.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

            // SystemWrapper bindings.
            this.Bind<IFile>().To<FileWrap>();
            this.Bind<IPath>().To<PathWrap>();
            this.Bind<IDirectory>().To<DirectoryWrap>();
            this.Bind<IDateTime>().To<DateTimeWrap>();

            // ViewModel bindings.
            this.Bind<IAssemblySettings>().To<AssemblySettings>().InSingletonScope();
            this.Bind<ICultureSettings>().To<CultureSettings>().InSingletonScope();
            this.Bind<IDialogServices>().To<DialogServices>().InSingletonScope();
            this.Bind<MainWindowViewModel>().To<MainWindowViewModel>().InSingletonScope();
        }

        #endregion
    }
}
