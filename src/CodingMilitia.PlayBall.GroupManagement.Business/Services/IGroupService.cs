using CodingMilitia.PlayBall.GroupManagement.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CodingMilitia.PlayBall.GroupManagement.Business.Services
{
    public interface IGroupService
    {
        Task<IReadOnlyCollection<Group>> GetAllAsync(CancellationToken ct);
        Task<Group> GetByIdAsync(long id, CancellationToken ct);
        Task<Group> UpdateAsync(Group group, CancellationToken ct);
        Task<Group> AddAsync(Group group, CancellationToken ct);
        Task RemoveAsync(long id, CancellationToken ct);
    }
}
