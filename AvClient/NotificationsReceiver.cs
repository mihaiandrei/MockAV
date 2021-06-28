using AvService.Shared;
using System.Windows.Controls;

namespace AVClient
{
    public class NotificationsReceiver : INotificationsReceiver
    {
        ListBox listBox;

        public NotificationsReceiver(ListBox listBox)
        {
            this.listBox = listBox;
        }

        public void ReceiveNotification(Notification notification)
        {
            listBox.Items.Add($"{notification.GetType().Name} -  {notification.NotificationTime}");
        }
    }
}
