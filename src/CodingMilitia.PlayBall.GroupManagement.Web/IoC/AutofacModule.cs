using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using CodingMilitia.PlayBall.GroupManagement.Business.Imp.Services;
using CodingMilitia.PlayBall.GroupManagement.Business.Models;
using CodingMilitia.PlayBall.GroupManagement.Business.Services;
using Microsoft.Extensions.Logging;

namespace CodingMilitia.PlayBall.GroupManagement.Web.IoC
{
    public class AutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<InMemoryGroupsService>()
            builder.RegisterType<GroupsService>()
                .Named<IGroupService>("groupsService")
                .SingleInstance();

            builder.RegisterDecorator<IGroupService>((context, service) =>
                new GroupServiceDecorator(service, context.Resolve<ILogger<GroupServiceDecorator>>()),
                "groupsService");
        }

        private class GroupServiceDecorator : IGroupService
        {
            private readonly IGroupService _inner;
            private readonly ILogger<GroupServiceDecorator> _logger;

            public GroupServiceDecorator(IGroupService inner, ILogger<GroupServiceDecorator> logger)
            {
                _inner = inner;
                _logger = logger;
            }

            public Task<Group> AddAsync(Group group, CancellationToken ct)
            {
                _logger.LogWarning($"### hello from {nameof(AddAsync)} ###");
                return _inner.AddAsync(group, ct);
            }

            public Task<IReadOnlyCollection<Group>> GetAllAsync(CancellationToken ct)
            {
                _logger.LogTrace($"### hello from {nameof(GetAllAsync)} ###");
                return _inner.GetAllAsync(ct);
            }

            public Task<Group> GetByIdAsync(long id, CancellationToken ct)
            {
                _logger.LogWarning($"### hello from {nameof(GetByIdAsync)} ###");
                return _inner.GetByIdAsync(id, ct);
            }

            public Task<Group> UpdateAsync(Group group, CancellationToken ct)
            {
                _logger.LogWarning($"### hello from {nameof(UpdateAsync)} ###");
                return _inner.UpdateAsync(group, ct);
            }
        }
    }
}
