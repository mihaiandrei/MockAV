using System.Windows;

namespace AVClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AvServiceClient avServiceClient;

        public MainWindow()
        {
            InitializeComponent();

            avServiceClient = new AvServiceClient(new NotificationsReceiver());
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            await avServiceClient.Connect();
        }

        private async void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            await avServiceClient.Disconect();
        }

        private async void StartOnDemandButton_Click(object sender, RoutedEventArgs e)
        {
            await avServiceClient.StartOnDemandScanAsync();
        }

        private async void StopOnDemandButton_Click(object sender, RoutedEventArgs e)
        {
            await avServiceClient.StopOnDemandScan();
        }

        private async void EnableRealtimeScanButton_Click(object sender, RoutedEventArgs e)
        {
            await avServiceClient.EnableRealTimeScan();
        }

        private async void DisablRealtimeScanButton_Click(object sender, RoutedEventArgs e)
        {
            await avServiceClient.DisableRealTimeScan();
        }
    }
}
