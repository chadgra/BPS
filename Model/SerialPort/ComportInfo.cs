//-----------------------------------------------------------------------
// <copyright file="ComportInfo.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Model.SerialPort
{
    using System;
    using System.Collections.Generic;
    using System.Management;

    /// <summary>
    /// ComportInfo class implementation.
    /// </summary>
    public class ComportInfo
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ComportInfo class.
        /// </summary>
        public ComportInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ComportInfo class.
        /// </summary>
        /// <param name="name">The name of the com port.</param>
        /// <param name="description">The description of the com port.</param>
        public ComportInfo(string name, string description)
        {
            this.Name = name;
            this.Description = description + " (" + name + ")";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the comport.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the description of the comport.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the manufacturer of the comport.
        /// </summary>
        public string Manufacturer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the device id of the comport.
        /// </summary>
        public string DeviceId
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the information for the comports.
        /// </summary>
        /// <returns>A list containing the information for the comports.</returns>
        public static ComportInfo[] GetComportsInfo()
        {
            var comPortInfoList = new List<ComportInfo>();

            var options = ProcessConnection.ProcessConnectionOptions();
            var connectionScope = ProcessConnection.ConnectionScope(Environment.MachineName, options, @"\root\CIMV2");

            var objectQuery = new ObjectQuery("SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0");
            var comPortSearcher = new ManagementObjectSearcher(connectionScope, objectQuery);

            using (comPortSearcher)
            {
                foreach (ManagementObject obj in comPortSearcher.Get())
                {
                    if (obj == null)
                    {
                        continue;
                    }

                    var captionObj = obj["Caption"];
                    if (captionObj == null)
                    {
                        continue;
                    }

                    var caption = captionObj.ToString();
                    if (!caption.Contains("(COM"))
                    {
                        continue;
                    }

                    var comPortInfo = new ComportInfo();
                    comPortInfo.Name =
                        caption.Substring(
                            caption.LastIndexOf("(COM", StringComparison.OrdinalIgnoreCase))
                               .Replace("(", string.Empty)
                               .Replace(")", string.Empty);
                    comPortInfo.Description = caption;
                    var manufacturer = obj["Manufacturer"];
                    if (null != manufacturer)
                    {
                        comPortInfo.Manufacturer = manufacturer.ToString();
                    }

                    var deviceId = obj["DeviceID"];
                    if (null != deviceId)
                    {
                        comPortInfo.DeviceId = deviceId.ToString();
                    }

                    comPortInfoList.Add(comPortInfo);
                }
            }

            return comPortInfoList.ToArray();
        }
    }

    #endregion
}