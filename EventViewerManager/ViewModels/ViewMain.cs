//-----------------------------------------------------------------------
// <copyright file="ViewMain.cs" company="None">
//     Copyright (c) None. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace EventViewerManager.ViewModels
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using EventViewerManager.Entities;
    using EventViewerManager.Models;

    /// <summary>
    /// Provide ViewModels for the MainWindow
    /// </summary>
    internal class ViewMain : INotifyPropertyChanged
    {
        #region Instance vars

        /// <summary> EventViewer instance </summary>
        private EventViewerModel eventViewer;

        /// <summary> Command for add event log </summary>
        private ICommand cmdAddEventLog;

        /// <summary> Command for delete event log</summary>
        private ICommand cmdDelEventLog;

        /// <summary> Command for delete event source</summary>
        private ICommand cmdDelEventSource;

        /// <summary> Command refresh events</summary>
        private ICommand cmdRefreshEvents;

        /// <summary> Command Exit</summary>
        private ICommand cmdExit;

        /// <summary> Binded to txtEventLog control </summary>
        private string textEventLog;

        /// <summary> Binded to txtEventSource control</summary>
        private string textEventSource;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> Event Log name </summary>
        private string eventLog;

        /// <summary> Event log source</summary>
        private string eventSource;

        /// <summary> Message to display </summary>
        private string message;

        #endregion Instance vars

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewMain"/> class.
        /// </summary>
        public ViewMain()
        {
            this.eventViewer = new EventViewerModel();
        }

        #region Controls

        /// <summary>
        /// Gets the application version.
        /// </summary>
        /// <value>
        /// The application version.
        /// </value>
        public string ApplicationVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the event log.
        /// </summary>
        /// <value>
        /// The event log.
        /// </value>
        public string EventLog
        {
            get
            {
                return this.eventLog;
            }

            set
            {
                if (this.eventLog != value)
                {
                    this.eventLog = value;
                    this.NotifyChange("EventLog");
                    this.NotifyChange("EventSources");
                    this.TextEventLog = value;
                    this.TextEventSource = null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the event source.
        /// </summary>
        /// <value>
        /// The event source.
        /// </value>
        public string EventSource
        {
            get
            {
                return this.eventSource;
            }

            set
            {
                if (this.eventSource != value)
                {
                    this.eventSource = value;
                    this.NotifyChange("EventSource");
                    this.TextEventSource = value;
                }
            }
        }

        /// <summary>
        /// Gets the event logs list
        /// </summary>
        /// <value>
        /// The event logs.
        /// </value>
        public IEnumerable<EventViewerItem> EventLogs
        {
            get
            {
                return this.eventViewer.GetEventLogs();
            }
        }

        /// <summary>
        /// Gets the event sources list
        /// </summary>
        /// <value>
        /// The event sources.
        /// </value>
        public string[] EventSources
        {
            get
            {
                return this.eventViewer.GetLogSourceList(this.EventLog);
            }
        }

        /// <summary>
        /// Gets or sets the text event source.
        /// </summary>
        /// <value>
        /// The text event source.
        /// </value>
        public string TextEventSource
        {
            get
            {
                return this.textEventSource;
            }

            set
            {
                if (this.textEventSource != value)
                {
                    this.textEventSource = value;
                    this.NotifyChange("TextEventSource");
                }
            }
        }

        /// <summary>
        /// Gets or sets the text event log.
        /// </summary>
        /// <value>
        /// The text event log.
        /// </value>
        public string TextEventLog
        {
            get
            {
                return this.textEventLog;
            }

            set
            {
                if (this.textEventLog != value)
                {
                    this.textEventLog = value;
                    this.NotifyChange("TextEventLog");
                }
            }
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                if (this.message != value)
                {
                    this.message = value;
                    this.NotifyChange("Message");
                }
            }
        }

        #endregion Controls

        #region Commands

        /// <summary>
        /// Gets the CMD add event log.
        /// </summary>
        /// <value>
        /// The CMD add event log.
        /// </value>
        public ICommand CmdAddEventLog
        {
            get
            {
                if (this.cmdAddEventLog == null)
                {
                    this.cmdAddEventLog = new GenericCommand((p => this.AddEventLog(p)), (o => { return true; }));
                }

                return this.cmdAddEventLog;
            }
        }

        /// <summary>
        /// Gets the CMD del event log.
        /// </summary>
        /// <value>
        /// The CMD del event log.
        /// </value>
        public ICommand CmdDelEventLog
        {
            get
            {
                if (this.cmdDelEventLog == null)
                {
                    this.cmdDelEventLog = new GenericCommand((p => this.DelEventLog(p)), (o => { return true; }));
                }

                return this.cmdDelEventLog;
            }
        }

        /// <summary>
        /// Gets the CMD del event source.
        /// </summary>
        /// <value>
        /// The CMD del event source.
        /// </value>
        public ICommand CmdDelEventSource
        {
            get
            {
                if (this.cmdDelEventSource == null)
                {
                    this.cmdDelEventSource = new GenericCommand((p => this.DelEventSource(p)), (o => { return true; }));
                }

                return this.cmdDelEventSource;
            }
        }

        /// <summary>
        /// Gets the CMD refresh events.
        /// </summary>
        /// <value>
        /// The CMD refresh events.
        /// </value>
        public ICommand CmdRefreshEvents
        {
            get
            {
                if (this.cmdRefreshEvents == null)
                {
                    this.cmdRefreshEvents = new GenericCommand((p => this.Refresh(p)), (o => { return true; }));
                }

                return this.cmdRefreshEvents;
            }
        }

        /// <summary>
        /// Gets the CMD exit.
        /// </summary>
        /// <value>
        /// The CMD exit.
        /// </value>
        public ICommand CmdExit
        {
            get
            {
                if (this.cmdExit == null)
                {
                    this.cmdExit = new GenericCommand((p => this.ExitApplication(p)), (o => { return true; }));
                }

                return this.cmdExit;
            }
        }

        #endregion Commands

        #region Private Methods

        /// <summary>
        /// Notifies the change.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void NotifyChange(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        /// <param name="p">The p.</param>
        private void ExitApplication(object p)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Adds the event log.
        /// </summary>
        /// <param name="param">The param.</param>
        private void AddEventLog(object param)
        {
            this.ExecuteAndMessage(o =>
            {
                this.eventViewer.AddEventLog(this.TextEventLog, this.TextEventSource);
                this.Refresh(null);
                this.Message = "Event Log/Event Source successfully created";
            }, param);
        }

        /// <summary>
        /// Delete the event log.
        /// </summary>
        /// <param name="param">The param.</param>
        private void DelEventLog(object param)
        {
            string[] dontDelete = { "Application", "Security", "Setup", "System" };
            this.ExecuteAndMessage(o =>
            {
                if (!dontDelete.Any(p => p == this.TextEventLog))
                {
                    if (MessageBox.Show(string.Format("Do you want to delete the {0} Event Log ?", this.TextEventLog), "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        this.eventViewer.DeleteEventLog(this.TextEventLog);
                        this.Refresh(null);
                        this.Message = "Event Log successfully deleted";
                    }
                }
                else
                {
                    this.Message = "Cannot delete EventLog";
                }
            }, param);
        }

        /// <summary>
        /// Delete the event source.
        /// </summary>
        /// <param name="param">The param.</param>
        private void DelEventSource(object param)
        {
            this.ExecuteAndMessage(o =>
            {
                if (MessageBox.Show(string.Format("Do you want to delete the {0} Event Source ?", this.TextEventSource), "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    this.eventViewer.DeleteSource(this.TextEventLog, this.TextEventSource);
                    this.Refresh(null);
                    this.Message = "Event Source successfully deleted";
                }
            }, param);
        }

        /// <summary>
        /// Refreshes the event logs and event sources lists
        /// </summary>
        /// <param name="param">The param.</param>
        private void Refresh(object param)
        {
            this.ExecuteAndMessage(o =>
            {
                this.eventViewer.Refresh();
                this.NotifyChange("EventLogs");
                this.NotifyChange("EventSources");
            }, param);
        }

        /// <summary>
        /// Wrap execution of action inside Try/Catch for ease (eventually) error management
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="p">The p.</param>
        private void ExecuteAndMessage(Action<object> action, object p)
        {
            if (action != null)
            {
                try
                {
                    action.Invoke(p);
                }
                catch (Exception ex)
                {
                    this.Message = ex.Message;
                }
            }
        }

        #endregion Private Methods
    }
}