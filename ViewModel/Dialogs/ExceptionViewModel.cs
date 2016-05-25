//-----------------------------------------------------------------------
// <copyright file="ExceptionViewModel.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewModel.Dialogs
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net.Configuration;
    using System.Net.Mail;
    using Caliburn.Micro;
    using ConfigurationManager = System.Configuration.ConfigurationManager;

    /// <summary>
    /// ExceptionViewModel class implementation.
    /// </summary>
    public class ExceptionViewModel : Screen
    {
        #region Fields

        private const int PastDaysToEmail = 1;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ExceptionViewModel class.
        /// </summary>
        /// <param name="exception">The exception that will be viewed.</param>
        public ExceptionViewModel(Exception exception)
        {
            this.Exception = exception;
            this.DestinationAddress = "software@owletcare.com";

            // SMTP settings are retrieved from the App.config file.
            this.SmtpClient = new SmtpClient();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Exception that is being displayed.
        /// </summary>
        public Exception Exception
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the message of the exception.
        /// </summary>
        public string Message
        {
            get
            {
                return this.Exception.GetType().Name + ": " + this.Exception.Message;
            }
        }

        /// <summary>
        /// Gets the stack trace of the exception.
        /// </summary>
        public string StackTrace
        {
            get
            {
                return this.Exception.StackTrace;
            }
        }

        /// <summary>
        /// Gets the data of the exception.
        /// </summary>
        public string Data
        {
            get
            {
                var data = string.Empty;

                foreach (var key in this.Exception.Data.Keys)
                {
                    data += string.Format(
                        CultureInfo.InvariantCulture,
                        "{0} = {1}\r\n",
                        key,
                        this.Exception.Data[key]);
                }

                return data;
            }
        }

        /// <summary>
        /// Gets or sets the destination email address;
        /// </summary>
        public string DestinationAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the smtp client to be modified for unit tests.
        /// </summary>
        public SmtpClient SmtpClient
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves the file.
        /// </summary>
        public void SendEmail()
        {
            var result = false;
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            var email = new MailMessage(
                smtpSection.From,
                this.DestinationAddress,
                AppDomain.CurrentDomain.FriendlyName + " Exception",
                this.Exception.ToString());

            // Attach the log file from today, and the log files from the last "PastDaysToEmail" days.
            var day = DateTime.Now;
            for (var i = 0; i <= PastDaysToEmail; i++)
            {
                var fileName = Path.ChangeExtension(day.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), ".log");
                if (File.Exists(fileName))
                {
                    email.Attachments.Add(new Attachment(fileName));
                }

                day = day - new TimeSpan(1, 0, 0, 0);
            }

            try
            {
                this.SmtpClient.Send(email);
                result = true;
            }
            catch (SmtpException)
            {
            }
            finally
            {
                email.Dispose();
                this.TryClose(result);
            }
        }

        /// <summary>
        /// Exit the dialog.
        /// </summary>
        public void Exit()
        {
            this.TryClose(true);
        }

        #endregion
    }
}
