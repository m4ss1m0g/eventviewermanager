
namespace EventViewerManager
{
    using System.Windows;
    using System.Windows.Controls;
    using EventViewerManager.Entities;
    using EventViewerManager.ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary> Instance of the ViewModel </summary>
        private ViewMain view;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.view = new ViewMain();
            this.DataContext = this.view;
        }

        /// <summary>
        /// Handles the SelectionChanged event of the LstEventLogs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void LstEventLogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItem != null)
                this.view.EventLog = ((sender as ListBox).SelectedItem as EventViewerItem).EventLog;
        }

        /// <summary>
        /// Handles the SelectionChanged event of the LstEventSources control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void LstEventSources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItem != null)
                this.view.EventSource = (sender as ListBox).SelectedItem.ToString();
        }
    }
}