using System.Collections.Generic;

namespace AvService.Domain.Notifications
{
    public class StopScanSuccessNotification : StopScanNotification
    {
        public StopScanSuccessNotification(IEnumerable<InfectedObject> infectedObjects) : base(infectedObjects)
        {
        }

        public override string Reason => "Scan ended successfully";
    }
}
