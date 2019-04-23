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
    [Route("groups")]
    public class GroupsController: Controller
    {

        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var result = await _groupService.GetAllAsync(ct);
            return View(result.ToViewModel());
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> Details(long id, CancellationToken ct)
        {
            var group = await _groupService.GetByIdAsync(id, ct);
            if(group==null)
            {
                return NotFound();
            }
            return View(group.ToViewModel());
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, GroupViewModel model, CancellationToken ct)
        {
            var group = await _groupService.UpdateAsync(model.ToServiceModel(), ct);
            if(group==null)
            {
                return NotFound();
            }

            //group.Name = model.Name;
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GroupViewModel model, CancellationToken ct)
        {
            await _groupService.AddAsync(model.ToServiceModel(), ct);
            return RedirectToAction("Index");
        }
    }
}