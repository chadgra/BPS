//-----------------------------------------------------------------------
// <copyright file="RealSerialPort.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Model.SerialPort
{
    using System.IO.Ports;
    using NLog;

    /// <summary>
    /// RealSerialPort class implementation.
    /// </summary>
    public class RealSerialPort : SerialPort, ISerialPort
    {
        #region Fields

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RealSerialPort class.
        /// </summary>
        /// <param name="portName">The port to use.</param>
        /// <param name="baudRate">The baud rate.</param>
        public RealSerialPort(string portName, int baudRate)
            : base(portName, baudRate)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the object has been disposed.
        /// </summary>
        public bool IsDisposed
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Reads all immediately available bytes, based on the encoding, in both the stream and the input buffer.
        /// </summary>
        /// <returns>The contents of the stream and the input buffer.</returns>
        public new string ReadExisting()
        {
            var existing = base.ReadExisting();
            Logger.Info(this.PortName + "< " + existing);
            return existing;
        }

        /// <summary>
        /// Reads up to the NewLine value in the input buffer.
        /// </summary>
        /// <returns>The contents of the input buffer up to the first occurrence of a NewLine value.</returns>
        public new string ReadLine()
        {
            var line = base.ReadLine();
            if (!string.IsNullOrEmpty(line))
            {
                Logger.Info(this.PortName + "< " + line);
            }

            return line;
        }

        /// <summary>
        /// Writes the specified string to the serial port.
        /// </summary>
        /// <param name="value">The string for output.</param>
        public new void Write(string value)
        {
            base.Write(value);
            Logger.Info(this.PortName + "> " + value);
        }

        /// <summary>
        /// Writes the specified string and a new line value to the output buffer.
        /// </summary>
        /// <param name="value">The string to write to the output buffer.</param>
        public new void WriteLine(string value)
        {
            base.WriteLine(value);
            Logger.Info(this.PortName + "> " + value);
        }

        /// <summary>
        /// The bulk of the clean-up code is done in this method.
        /// </summary>
        /// <param name="disposing">A parameter indicating whether we are disposing of the managed resources.</param>
        protected override void Dispose(bool disposing)
        {
            this.IsDisposed = true;
            base.Dispose(disposing);
        }

        #endregion
    }
}
