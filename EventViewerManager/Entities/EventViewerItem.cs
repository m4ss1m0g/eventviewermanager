
namespace EventViewerManager.Entities
{
    using System.Diagnostics;

    /// <summary>
    /// Contains info about EventLog and relative sources
    /// </summary>
    public class EventViewerItem
    {
        /// <summary>
        /// Gets or sets the event log.
        /// </summary>
        /// <value>
        /// The event log.
        /// </value>
        public string EventLog { get; set; }

        /// <summary>
        /// Gets or sets the event source.
        /// </summary>
        /// <value>
        /// The event source.
        /// </value>
        public string EventSource { get; set; }

        /// <summary>
        /// Gets or sets the event log counts entries.
        /// </summary>
        /// <value>
        /// The event log counts.
        /// </value>
        public int EventLogCounts { get; set; }

        /// <summary>
        /// Gets or sets the event log object.
        /// </summary>
        /// <value>
        /// The event log object.
        /// </value>
        public EventLog EventLogObject { get; set; }

        /// <summary>
        /// Gets the event log title.
        /// </summary>
        /// <value>
        /// The event log title.
        /// </value>
        public string EventLogTitle
        {
            get { return string.Format("{0} ({1} events)", this.EventLog, this.EventLogCounts); }
        }
    }
}