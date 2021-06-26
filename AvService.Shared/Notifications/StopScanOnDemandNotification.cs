using System.Collections.Generic;

namespace AvService.Domain.Notifications
{
    public class StopScanOnDemandNotification : StopScanNotification
    {
        public override string Reason => "Scan was forcibly stopped";
    }
}
