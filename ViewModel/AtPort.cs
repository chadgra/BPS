//-----------------------------------------------------------------------
// <copyright file="AtPort.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewModel
{
    using System;
    using System.IO.Ports;
    using System.Threading;
    using Model.SerialPort;

    /// <summary>
    /// OwletPort class implementation.
    /// </summary>
    public class AtPort : IDisposable
    {
        #region Fields

        private const int DefaultTimeout = 500;
        private const string DefaultNewLine = "\r\n";

        private readonly string portName;

        private ISerialPort serialPort;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the AtPort class.
        /// </summary>
        /// <param name="portName">The name of the com port.</param>
        /// <param name="baudRate">The initial baud rate of the port.</param>
        public AtPort(string portName, int baudRate)
        {
            this.portName = portName;
            this.SerialPort = new RealSerialPort(portName, baudRate);
            this.SerialPort.ReadTimeout = DefaultTimeout;
            this.SerialPort.WriteTimeout = DefaultTimeout;
            this.SerialPort.NewLine = DefaultNewLine;
            this.SerialPort.Handshake = Handshake.RequestToSend;
            this.SerialPort.Open();
        }

        #endregion

        #region Destructors

        /// <summary>
        /// Finalizes an instance of the AtPort class.
        /// </summary>
        ~AtPort()
        {
            this.Dispose(false);
        }

        #endregion

        #region Events

        #endregion

        #region Properties

        /// <summary>
        /// Gets the serial port associated with this owlet port.
        /// </summary>
        public ISerialPort SerialPort
        {
            get
            {
                return this.serialPort;
            }

            private set
            {
                this.serialPort = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends a command to the owlet port.
        /// </summary>
        /// <param name="command">The command to send don't include the carriage return.</param>
        public void SendCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                return;
            }

            this.SerialPort.WriteLine(command);
        }

        /// <summary>
        /// Sends data to the owlet port.
        /// </summary>
        /// <param name="data">The string to send.</param>
        public void SendString(string data)
        {
            this.SerialPort.Write(data);
        }

        /// <summary>
        /// Receive a response from the owlet port.
        /// </summary>
        /// <returns>The response, which does not include the carriage return or new line.</returns>
        public string ReceiveResponse()
        {
            return this.ReceiveEvent(DefaultTimeout);
        }

        /// <summary>
        /// Verify that we receive no response for a certain amount of time.
        /// </summary>
        /// <param name="timeout">The amount of time in which we can receive no response.</param>
        /// <returns>A bool indicating if we received a response or not. True for no response received.</returns>
        public bool ReceiveNoResponse(int timeout)
        {
            Thread.Sleep(timeout);
            return string.IsNullOrEmpty(this.SerialPort.ReadExisting());
        }

        /// <summary>
        /// Receive an event from the owlet port.
        /// </summary>
        /// <param name="timeout">The amount of time to wait for an event, in milliseconds.</param>
        /// <returns>The event, which does not include the carriage return or new line.</returns>
        public string ReceiveEvent(int timeout)
        {
            this.SerialPort.ReadTimeout = timeout;
            var response = this.SerialPort.ReadLine();
            if (!string.IsNullOrEmpty(response))
            {
                throw new FormatException("Expected: <CRLF> Actual: " + response);
            }

            return this.SerialPort.ReadLine();
        }

        /// <summary>
        /// Receive a string from the owlet port.
        /// </summary>
        /// <param name="timeout">The amount of time to wait before reading the string, in milliseconds.</param>
        /// <returns>The string received.</returns>
        public string ReceiveString(int timeout)
        {
            Thread.Sleep(timeout);
            return this.serialPort.ReadExisting();
        }

        /// <summary>
        /// Flush the receive buffer so that we don't fail when we start testing immediately after a reset.
        /// </summary>
        public void FlushBuffer()
        {
            this.SendCommand("AT");
            Thread.Sleep(100);
            this.SerialPort.DiscardInBuffer();
        }

        /// <summary>
        /// Change the baud rate of this port.
        /// </summary>
        /// <param name="baudRate">The new baud rate.</param>
        public void ChangeBaudRate(int baudRate)
        {
            this.SerialPort.Close();
            this.SerialPort = new RealSerialPort(this.portName, baudRate);
            this.FlushBuffer();
        }

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
                this.SerialPort.Dispose();
            }
        }

        #endregion
    }
}
