using Reinforced.Typings.Attributes;
using System.Collections.Generic;

namespace AvService.Shared
{
    [TsClass]
    public class ThreatFoundNotification : Notification
    {
        public ThreatFoundNotification(IEnumerable<InfectedObject> infectedObjects)
        {
            InfectedObjects = infectedObjects;
        }

        public IEnumerable<InfectedObject> InfectedObjects { get; }
    }
}
