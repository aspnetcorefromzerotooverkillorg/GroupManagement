using Microsoft.AspNetCore.Mvc;
using CodingMilitia.PlayBall.GroupManagement.Web.Models;
using System.Collections.Generic;
using System.Linq;
using CodingMilitia.PlayBall.GroupManagement.Business.Services;
using CodingMilitia.PlayBall.GroupManagement.Web.Mappings;
using CodingMilitia.PlayBall.GroupManagement.Web.Filters;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace CodingMilitia.PlayBall.GroupManagement.Web.Controllers
{
    [ApiController]
    [Route("groups")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAsync(CancellationToken ct)
        {
            var result = await _groupService.GetAllAsync(ct);
            return Ok(result.ToModel());
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetByIdAsync(long id, CancellationToken ct)
        {
            var group = await _groupService.GetByIdAsync(id, ct);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group.ToModel());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, GroupModel model, CancellationToken ct)
        {
            model.Id = id;
            var group = await _groupService.UpdateAsync(model.ToServiceModel(), ct);

            return Ok(group.ToModel());
        }

        [HttpPost]
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> AddAsync(GroupModel model, CancellationToken ct)
        {
            var group = await _groupService.AddAsync(model.ToServiceModel(), ct);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = group.Id }, group);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveAsync(long id, CancellationToken ct)
        {
            await _groupService.RemoveAsync(id, ct);
            return NoContent();
        }
    }
}