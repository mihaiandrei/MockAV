using System.Collections.Generic;

namespace AvService.Domain.Notifications
{
    public class ThreatFoundNotification : Notification
    {
        public ThreatFoundNotification(IEnumerable<InfectedObject> infectedObjects)
        {
            InfectedObjects = infectedObjects;
        }

        public IEnumerable<InfectedObject> InfectedObjects { get; }
    }
}
