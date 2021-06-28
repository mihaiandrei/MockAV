using Reinforced.Typings.Attributes;

namespace AvService.Shared
{
    [TsClass]
    public class StopScanOnDemandNotification : StopScanNotification
    {
        public override string Reason => "Scan was forcibly stopped";
    }
}
