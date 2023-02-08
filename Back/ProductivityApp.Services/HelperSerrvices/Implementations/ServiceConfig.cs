using ProductivityApp.Services.HelperSerrvices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityApp.Services.HelperSerrvices.Implementations
{
    public class ServiceConfig<T> : IServiceConfig<T>
    {
        public string CronExpression { get; set; }
        public TimeZoneInfo TimeZoneInfo { get; set; }
    }
}
