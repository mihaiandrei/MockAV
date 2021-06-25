namespace AvService.Domain.Notifications
{
    public class StopScanNotification : Notification
    {
        public virtual string Reason { get; }
    }
}
