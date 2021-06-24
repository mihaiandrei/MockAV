using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace AVClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HubConnection connection;

        public MainWindow()
        {
            InitializeComponent();

            connection = new HubConnectionBuilder()
                .WithAutomaticReconnect()
                .WithUrl("http://localhost:5000/scanhub")
                .Build();
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            connection.On<string>("ReceiveMessage", (message) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    //messagesList.Items.Add(newMessage);
                });
            });

            try
            {
                await connection.StartAsync();
                //  messagesList.Items.Add("Connection started");
            }
            catch (Exception ex)
            {
                // messagesList.Items.Add(ex.Message);
            }
        }

        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void StartOnDemandButton_Click(object sender, RoutedEventArgs e)
        {
            await connection.InvokeAsync("SendMessage", "message");
        }

        private void StopOnDemandButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EnableRealtimeScanButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DisablRealtimeScanButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
