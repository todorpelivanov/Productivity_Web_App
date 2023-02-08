using ProductivityApp.Shared.CronExpressions.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityApp.Shared.CronExpressions.Implementation
{
    public class ScheduleConfig : IScheduleConfig
    {
        public string CronExpression { get; set; } = "";
        public TimeZoneInfo TimeZoneInfo { get; set; } = TimeZoneInfo.Local;
    }
}
