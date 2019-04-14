﻿using CodingMilitia.PlayBall.GroupManagement.Business.Models;
using CodingMilitia.PlayBall.GroupManagement.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingMilitia.PlayBall.GroupManagement.Business.Imp.Services
{
    public class InMemoryGroupsService : IGroupService
    {
        private readonly List<Group> _groups = new List<Group>();
        private long _currentId = 0;

        public Group Add(Group group)
        {
            group.Id = ++_currentId;
            _groups.Add(group);
            return group;
        }

        public IReadOnlyCollection<Group> GetAll()
        {
            return _groups.AsReadOnly();
        }

        public Group GetById(long id)
        {
            return _groups.SingleOrDefault(g => g.Id == id);
        }

        public Group Update(Group group)
        {
            var toUpdate = _groups.SingleOrDefault(g => g.Id == group.Id);
            if (toUpdate == null) {
                return null;
            }

            toUpdate.Name = group.Name;

            return toUpdate;
        }
    }
}
