//-----------------------------------------------------------------------
// <copyright file="ProcessConnection.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Model.SerialPort
{
    using System.Management;

    /// <summary>
    /// ProcessConnection class implementation.
    /// </summary>
    internal static class ProcessConnection
    {
        /// <summary>
        /// Setup connection options.
        /// </summary>
        /// <returns>The connection options.</returns>
        public static ConnectionOptions ProcessConnectionOptions()
        {
            var options = new ConnectionOptions();
            options.Impersonation = ImpersonationLevel.Impersonate;
            options.Authentication = AuthenticationLevel.Default;
            options.EnablePrivileges = true;
            return options;
        }

        /// <summary>
        /// Setup the scope.
        /// </summary>
        /// <param name="machineName">The machine name.</param>
        /// <param name="options">The connection options.</param>
        /// <param name="path">The path.</param>
        /// <returns>The management scope.</returns>
        public static ManagementScope ConnectionScope(string machineName, ConnectionOptions options, string path)
        {
            var connectScope = new ManagementScope();
            connectScope.Path = new ManagementPath(@"\\" + machineName + path);
            connectScope.Options = options;
            connectScope.Connect();
            return connectScope;
        }
    }
}
