using Reinforced.Typings.Attributes;
using System;

namespace AvService.Shared
{
    [TsClass]
    public class Notification
    {
        public Notification()
        {
            NotificationTime = DateTime.Now;
        }
        public DateTime NotificationTime { get; set; }
    }
}
