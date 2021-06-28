using Reinforced.Typings.Attributes;

namespace AvService.Shared
{
    [TsClass]
    public class InfectedObject
    {
        public string FilePath { get; set; }
        public string ThreatName { get; set; }
    }
}