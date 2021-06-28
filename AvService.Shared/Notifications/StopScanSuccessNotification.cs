using Reinforced.Typings.Attributes;

namespace AvService.Shared
{
    [TsClass]
    public class StopScanSuccessNotification : StopScanNotification
    {
        public override string Reason => "Scan ended successfully";
    }
}
