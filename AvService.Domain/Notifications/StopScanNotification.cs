using System.Collections.Generic;

namespace AvService.Domain.Notifications
{
    public class StopScanNotification : Notification
    {
        public StopScanNotification(IEnumerable<InfectedObject> infectedObjects)
        {
            InfectedObjects = infectedObjects;
        }

        public virtual string Reason { get; }

        public IEnumerable<InfectedObject> InfectedObjects { get; }
    }
}
