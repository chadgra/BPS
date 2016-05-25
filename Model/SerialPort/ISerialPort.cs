//-----------------------------------------------------------------------
// <copyright file="ISerialPort.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Model.SerialPort
{
    using System;
    using System.IO.Ports;

    /// <summary>
    /// ISerialPort interface definition.
    /// </summary>
    public interface ISerialPort : IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the serial baud rate.
        /// </summary>
        int BaudRate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the handshaking protocol for serial port transmission of data using a value from Handshake.
        /// </summary>
        Handshake Handshake
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether the port is opened.
        /// </summary>
        bool IsOpen
        {
            get;
        }

        /// <summary>
        /// Gets or sets the port for communications, including but not limited to all available COM ports.
        /// </summary>
        string PortName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value used to interpret the end of a call to the ReadLine and WriteLine methods.
        /// </summary>
        string NewLine
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of milliseconds before a time-out occurs when a read operation does not finish.
        /// </summary>
        int ReadTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of milliseconds before a time-out occurs when a write operation does not finish.
        /// </summary>
        int WriteTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether the object has been disposed.
        /// </summary>
        bool IsDisposed
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Opens a new serial port connection.
        /// </summary>
        void Open();

        /// <summary>
        /// Closes the port connection, sets the IsOpen property to false, and disposes of the internal Stream object.
        /// </summary>
        void Close();

        /// <summary>
        /// Reads all immediately available bytes, based on the encoding, in both the stream and the input buffer.
        /// </summary>
        /// <returns>The contents of the stream and the input buffer.</returns>
        string ReadExisting();

        /// <summary>
        /// Reads up to the NewLine value in the input buffer.
        /// </summary>
        /// <returns>The contents of the input buffer up to the first occurrence of a NewLine value.</returns>
        string ReadLine();

        /// <summary>
        /// Discards data from the serial driver's receive buffer.
        /// </summary>
        void DiscardInBuffer();

        /// <summary>
        /// Writes the specified string to the serial port.
        /// </summary>
        /// <param name="value">The string for output.</param>
        void Write(string value);

        /// <summary>
        /// Writes a specified number of bytes to the serial port using data from a buffer.
        /// </summary>
        /// <param name="buffer">The byte array that contains the data to write to the port.</param>
        /// <param name="offset">
        /// The zero-based byte offset in the buffer parameter at which to begin copying bytes to the port.
        /// </param>
        /// <param name="count">The number of bytes to write.</param>
        void Write(byte[] buffer, int offset, int count);

        /// <summary>
        /// Writes the specified string and a new line value to the output buffer.
        /// </summary>
        /// <param name="value">The string to write to the output buffer.</param>
        void WriteLine(string value);

        #endregion
    }
}
