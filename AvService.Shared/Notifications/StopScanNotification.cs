using Reinforced.Typings.Attributes;

namespace AvService.Shared
{
    [TsClass]
    public class StopScanNotification : Notification
    {
        public virtual string Reason { get; }
    }
}
