using CodingMilitia.PlayBall.GroupManagement.Business.Models;
using CodingMilitia.PlayBall.GroupManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingMilitia.PlayBall.GroupManagement.Business.Imp.Mappings
{
    public static class GroupMappings
    {
        public static Group ToService(this GroupEntity entity)
        {
            return (entity != null) ? new Group
            {
                Id = entity.Id,
                Name = entity.Name
            } : null;
        }

        public static GroupEntity ToEntity(this Group model)
        {
            return (model != null) ? new GroupEntity
            {
                Id = model.Id,
                Name = model.Name
            } : null;
        }

        public static IReadOnlyCollection<Group> ToService(this IReadOnlyCollection<GroupEntity> entities)
            => entities.MapCollection(ToService);
    }
}
