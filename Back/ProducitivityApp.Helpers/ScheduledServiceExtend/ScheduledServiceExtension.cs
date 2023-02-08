using Microsoft.Extensions.DependencyInjection;
using ProductivityApp.Services.HelperSerrvices.Implementations;
using ProductivityApp.Services.HelperSerrvices.Interfaces;
using ProductivityApp.Shared.CronExpressions.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducitivityApp.Helpers.ScheduledServiceExtend
{
    public static class ScheduledServiceExtension
    {
        public static IServiceCollection AddCronJob<T>(this IServiceCollection services, Action<IServiceConfig<T>> options) where T : JobServiceWithCron
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), @"Please provide Schedule Configurations.");
            }
            var config = new ServiceConfig<T>();
            options.Invoke(config);
            if (string.IsNullOrWhiteSpace(config.CronExpression))
            {
                throw new ArgumentNullException(nameof(IServiceConfig<T>.CronExpression), @"Empty Cron Expression is not allowed.");
            }

            services.AddSingleton<IServiceConfig<T>>(config);
            services.AddHostedService<T>();
            return services;
        }
    }
}

