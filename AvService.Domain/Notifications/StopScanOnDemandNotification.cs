using System.Collections.Generic;

namespace AvService.Domain.Notifications
{
    public class StopScanOnDemandNotification : StopScanNotification
    {
        public StopScanOnDemandNotification(IEnumerable<InfectedObject> infectedObjects) : base(infectedObjects)
        {
        }

        public override string Reason => "Scan was forcibly stopped";
    }
}
