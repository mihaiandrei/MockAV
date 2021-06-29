using AvService.ClientSdk;
using System.Windows;

namespace AVClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AvServiceClient avServiceClient;

        public NotificationsReceiver NotificationsReceiver { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            NotificationsReceiver = new NotificationsReceiver(NotificationListBox);
            avServiceClient = new AvServiceClient(NotificationsReceiver);
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (avServiceClient.IsConnected || avServiceClient.IsConnecting)
                return;

            await avServiceClient.Connect();
            LogsListBox.Items.Add("Connected");
        }

        private async void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!avServiceClient.IsConnected)
                return;

            await avServiceClient.Disconect();
            LogsListBox.Items.Add("Disconected");
        }

        private async void StartOnDemandButton_Click(object sender, RoutedEventArgs e)
        {
            if (!avServiceClient.IsConnected)
                return;

            await avServiceClient.StartOnDemandScanAsync();

            LogsListBox.Items.Add("Started On Demand Scan");
        }

        private async void StopOnDemandButton_Click(object sender, RoutedEventArgs e)
        {
            await avServiceClient.StopOnDemandScan();
            LogsListBox.Items.Add("Stopped On Demand Scan");
        }

        private async void EnableRealtimeScanButton_Click(object sender, RoutedEventArgs e)
        {
            if (!avServiceClient.IsConnected)
                return;

            await avServiceClient.EnableRealTimeScan();
            LogsListBox.Items.Add("Enabled Realtime Scan");
        }

        private async void DisablRealtimeScanButton_Click(object sender, RoutedEventArgs e)
        {
            if (!avServiceClient.IsConnected)
                return;

            await avServiceClient.DisableRealTimeScan();
            LogsListBox.Items.Add("Disabled Realtime Scan");
        }

        private async void RequestUnsentNotifications_Click(object sender, RoutedEventArgs e)
        {
            if (!avServiceClient.IsConnected)
                return;

            await avServiceClient.PublishUnsentNotifications();
            LogsListBox.Items.Add("Requested Unsent Notifications");
        }
    }
}
