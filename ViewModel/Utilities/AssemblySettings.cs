//-----------------------------------------------------------------------
// <copyright file="AssemblySettings.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewModel.Utilities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// AssemblyInfo class implementation.
    /// </summary>
    public class AssemblySettings : IAssemblySettings
    {
        #region Fields

        private readonly Assembly assembly;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the AssemblySettings class.
        /// </summary>
        public AssemblySettings()
        {
            this.assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the full name of the executing assembly (including .exe).
        /// </summary>
        public string FullName
        {
            get
            {
                return this.assembly.ManifestModule.Name;
            }
        }

        /// <summary>
        /// Gets the name of the executing assembly (not include .exe).
        /// </summary>
        public string Name
        {
            get
            {
                return this.FullName.Replace(".exe", string.Empty).Replace(".dll", string.Empty);
            }
        }

        /// <summary>
        /// Gets the version of the executing assembly.
        /// </summary>
        public string Version
        {
            get
            {
                return this.assembly.GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Gets the branch named and commit date of the executing assembly.
        /// </summary>
        public string BranchDate
        {
            get
            {
                var result = string.Empty;
                var descriptionAttribute =
                    this.assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)
                        .OfType<AssemblyDescriptionAttribute>()
                        .FirstOrDefault();

                if (null != descriptionAttribute)
                {
                    result = descriptionAttribute.Description;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the branch name of the executing assembly.
        /// </summary>
        public string Branch
        {
            get
            {
                return this.BranchDate.Split().FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the commit date of the executing assembly.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1806:DoNotIgnoreMethodResults",
            Scope = "class", Justification = "If the parse fails, just return the default.")]
        public DateTime CommitDate
        {
            get
            {
                DateTime result;
                DateTime.TryParse(
                    this.BranchDate.Split().LastOrDefault(),
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out result);
                return result;
            }
        }

        /// <summary>
        /// Gets the configuration of this build.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Scope = "class", Justification = "This binds to the view.")]
        public string Configuration
        {
            get
            {
                return Environment.GetEnvironmentVariable("Configuration");
            }
        }

        /// <summary>
        /// Gets the version control id of the executing assembly.
        /// </summary>
        public string GitId
        {
            get
            {
                var result = string.Empty;
                var trademarkAttribute =
                    this.assembly.GetCustomAttributes(typeof(AssemblyTrademarkAttribute), false)
                        .OfType<AssemblyTrademarkAttribute>()
                        .FirstOrDefault();

                if (null != trademarkAttribute)
                {
                    result = trademarkAttribute.Trademark;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets a value indicating whether there were local modifications to the source code that weren't committed.
        /// </summary>
        public bool LocalModifications
        {
            get
            {
                return this.GitId.Contains("dirty");
            }
        }

        #endregion
    }
}
