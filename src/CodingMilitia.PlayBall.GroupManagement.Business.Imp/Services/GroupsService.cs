using CodingMilitia.PlayBall.GroupManagement.Business.Imp.Mappings;
using CodingMilitia.PlayBall.GroupManagement.Business.Models;
using CodingMilitia.PlayBall.GroupManagement.Business.Services;
using CodingMilitia.PlayBall.GroupManagement.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodingMilitia.PlayBall.GroupManagement.Business.Imp.Services
{
    public class GroupsService : IGroupService
    {
        private readonly GroupManagementDbContext _context;

        public GroupsService(GroupManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Group> AddAsync(Group group, CancellationToken ct)
        {
            var added = _context.Groups.Add(group.ToEntity());
            await _context.SaveChangesAsync(ct);
            return added.Entity.ToService();
        }

        public async Task<IReadOnlyCollection<Group>> GetAllAsync(CancellationToken ct)
        {
            var groups = await _context.Groups.AsNoTracking().ToListAsync(ct);
            return groups.ToService();
        }

        public async Task<Group> GetByIdAsync(long id, CancellationToken ct)
        {
            var group = await _context.Groups
                                .AsNoTracking()
                                .SingleOrDefaultAsync(g => g.Id == id, ct);
            return group.ToService();
        }

        public async Task<Group> UpdateAsync(Group group, CancellationToken ct)
        {
            var existingGroup = await _context.Groups
                                        .AsNoTracking()
                                        .SingleOrDefaultAsync(g => g.Id == group.Id);
            existingGroup.Name = group.Name;
            await _context.SaveChangesAsync(ct);
            return existingGroup.ToService();
        }

        public async Task RemoveAsync(long id, CancellationToken ct)
        {
            var entityToRemove = await _context.Groups.SingleOrDefaultAsync(g => g.Id == id);
            _context.Groups.Remove(entityToRemove);
            await _context.SaveChangesAsync(ct);
        }
    }
}
