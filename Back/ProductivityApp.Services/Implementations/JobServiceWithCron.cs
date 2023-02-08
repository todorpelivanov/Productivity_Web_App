using Microsoft.Extensions.Logging;
using ProductivityApp.Services.HelperSerrvices.Interfaces;
using ProductivityApp.Shared.CronExpressions.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityApp.Shared.CronExpressions.Implementation
{
    public class JobServiceWithCron : AbstractJobService
    {
        private readonly ILogger<JobServiceWithCron> _logger;

        public JobServiceWithCron(IServiceConfig<JobServiceWithCron> config, ILogger<JobServiceWithCron> logger)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CronJob 3 starts.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} CronJob 3 is working.");
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CronJob 3 is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
