using CodingMilitia.PlayBall.GroupManagement.Business.Imp.Services;
using CodingMilitia.PlayBall.GroupManagement.Business.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            // dependency injection
            //services.AddSingleton<IGroupService, InMemoryGroupsService>();
            services.AddScoped<IGroupService, GroupsService>();

            //can add more business services

            return services;
        }

        public static TConfig ConfigurePOCO<TConfig>(this IServiceCollection services,
            IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var config = new TConfig();
            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }
    }
}
