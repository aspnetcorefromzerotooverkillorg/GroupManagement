using System;
using System.Collections.Generic;
using System.Linq;
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
            builder.RegisterType<InMemoryGroupsService>()
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

            public Group Add(Group group)
            {
                _logger.LogWarning($"### hello from {nameof(Add)} ###");
                return _inner.Add(group);
            }

            public IReadOnlyCollection<Group> GetAll()
            {
                _logger.LogTrace($"### hello from {nameof(GetAll)} ###");
                return _inner.GetAll();
            }

            public Group GetById(long id)
            {
                _logger.LogWarning($"### hello from {nameof(GetById)} ###");
                return _inner.GetById(id);
            }

            public Group Update(Group group)
            {
                _logger.LogWarning($"### hello from {nameof(Update)} ###");
                return _inner.Update(group);
            }
        }
    }
}
