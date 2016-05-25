//-----------------------------------------------------------------------
// <copyright file="IAssemblySettings.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewModel.Utilities
{
    using System;

    /// <summary>
    /// IAssemblyInfo interface definition.
    /// </summary>
    public interface IAssemblySettings
    {
        #region Properties

        /// <summary>
        /// Gets the full name of the executing assembly (including .exe).
        /// </summary>
        string FullName
        {
            get;
        }

        /// <summary>
        /// Gets the name of the executing assembly (not include .exe).
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets the version of the executing assembly.
        /// </summary>
        string Version
        {
            get;
        }

        /// <summary>
        /// Gets the branch named and commit date of the executing assembly.
        /// </summary>
        string BranchDate
        {
            get;
        }

        /// <summary>
        /// Gets the branch name of the executing assembly.
        /// </summary>
        string Branch
        {
            get;
        }

        /// <summary>
        /// Gets the commit date of the executing assembly.
        /// </summary>
        DateTime CommitDate
        {
            get;
        }

        /// <summary>
        /// Gets the configuration of this build.
        /// </summary>
        string Configuration
        {
            get;
        }

        /// <summary>
        /// Gets the version control id of the executing assembly.
        /// </summary>
        string GitId
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether there were local modifications to the source code that weren't committed.
        /// </summary>
        bool LocalModifications
        {
            get;
        }

        #endregion
    }
}
