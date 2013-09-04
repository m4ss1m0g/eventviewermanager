namespace EventViewerManager.Models
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using EventViewerManager.Entities;

    /// <summary>
    /// Manage Event Viewer Entries.
    /// You can Delete/Add EventLog and EventSource
    /// </summary>
    public class EventViewerModel
    {
        /// <summary> Message writed to EventViewer on create </summary>
        private const string MsgLogCreated = "Event Viewer Create succesfully";

        /// <summary> Registry Key where add sources </summary>
        private const string RegKeyEventLog = @"SYSTEM\CurrentControlSet\Services\EventLog\{0}";

        /// <summary> Event Viewer Dictionary </summary>
        private List<EventViewerItem> dictionaryLogs = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventViewerModel"/> class.
        /// </summary>
        public EventViewerModel()
        {
            this.Refresh();
        }

        /// <summary>
        /// Adds the event log.
        /// </summary>
        /// <param name="eventLog">The event log name.</param>
        /// <param name="eventLogSource">The event log source.</param>
        public void AddEventLog(string eventLog, string eventLogSource)
        {
            if (!string.IsNullOrEmpty(eventLog) && !string.IsNullOrEmpty(eventLogSource))
            {
                // Check if exist
                if (EventLog.Exists(eventLog))
                {
                    EventLog.CreateEventSource(eventLogSource, eventLog);
                }
                else
                {
                    EventLog log = new EventLog(eventLog);

                    // If no source name inserted setup the eventlog name as source
                    if (string.IsNullOrEmpty(eventLogSource))
                    {
                        log.Source = log.Log;
                    }
                    else
                    {
                        log.Source = eventLogSource;
                    }

                    log.WriteEntry(MsgLogCreated, EventLogEntryType.Information, 1000);
                }
            }
        }

        /// <summary>
        /// Gets the event logs list.
        /// </summary>
        /// <returns>List of all event logs</returns>
        public IEnumerable<EventViewerItem> GetEventLogs()
        {
            return this.dictionaryLogs.ToList();
        }

        /// <summary>
        /// Gets the log source list.
        /// </summary>
        /// <param name="eventViewer">The event viewer.</param>
        /// <returns>List of all sources for the selected event viewer</returns>
        public string[] GetLogSourceList(string eventViewer)
        {
            if (!string.IsNullOrEmpty(eventViewer))
            {
                if (this.dictionaryLogs.Any(p => p.EventLog == eventViewer))
                {
                    EventLog log = this.dictionaryLogs.First(p => p.EventLog == eventViewer).EventLogObject;
                    return (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                        string.Format(RegKeyEventLog, log.Log)).GetSubKeyNames().OrderBy(p => p).ToArray());
                }
            }

            return new string[] { };
        }

        /// <summary>
        /// Delete the source of the selected event log
        /// </summary>
        /// <param name="eventViewer">The event viewer name.</param>
        /// <param name="source">The source.</param>
        public void DeleteSource(string eventViewer, string source)
        {
            if (!string.IsNullOrEmpty(eventViewer))
            {
                //if (this.dictionaryLogs.ContainsKey(eventViewer))
                //{
                //    EventLog log = this.dictionaryLogs[eventViewer];
                if (this.dictionaryLogs.Any(p => p.EventLog == eventViewer))
                {
                    EventLog log = this.dictionaryLogs.First(p => p.EventLog == eventViewer).EventLogObject;
                    EventLog.DeleteEventSource(source);
                }
            }
        }

        /// <summary>
        /// Deletes the event log.
        /// </summary>
        /// <param name="eventLog">The event log name.</param>
        public void DeleteEventLog(string eventViewer)
        {
            if (!string.IsNullOrEmpty(eventViewer))
            {
                if (this.dictionaryLogs.Any(p => p.EventLog == eventViewer))
                {
                    EventLog log = this.dictionaryLogs.First(p => p.EventLog == eventViewer).EventLogObject;
                    EventLog.Delete(log.Log);
                }
            }
        }

        /// <summary>
        /// Refreshe the dictionary
        /// </summary>
        public void Refresh()
        {
            // Prendo la lista dei log e lo casto dentro il Dictionary con la descrizione e l'oggetto
            this.dictionaryLogs = (from l in EventLog.GetEventLogs()
                                   orderby l.Log
                                   select new EventViewerItem
                                   {
                                       EventLog = l.Log,
                                       EventLogCounts = l.Entries.Count,
                                       EventLogObject = l
                                   }
                                  ).ToList();
        }
    }
}