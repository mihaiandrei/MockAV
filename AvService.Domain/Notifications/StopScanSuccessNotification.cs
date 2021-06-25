namespace AvService.Domain.Notifications
{
    public class StopScanSuccessNotification : StopScanNotification
    {
        public override string Reason => "Scan ended successfully";
    }
}
