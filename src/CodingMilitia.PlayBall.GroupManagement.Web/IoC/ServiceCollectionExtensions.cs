using CodingMilitia.PlayBall.GroupManagement.Business.Imp.Services;
using CodingMilitia.PlayBall.GroupManagement.Business.Services;
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
            services.AddSingleton<IGroupService, InMemoryGroupsService>();

            //can add more business services

            return services;
        }
    }
}
