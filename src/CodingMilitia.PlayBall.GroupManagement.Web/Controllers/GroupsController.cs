using Microsoft.AspNetCore.Mvc;
using CodingMilitia.PlayBall.GroupManagement.Web.Models;
using System.Collections.Generic;
using System.Linq;
using CodingMilitia.PlayBall.GroupManagement.Business.Services;
using CodingMilitia.PlayBall.GroupManagement.Web.Mappings;

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
        public IActionResult Index()
        {
            return View(_groupService.GetAll().ToViewModel());
        }

        [HttpGet]
        [Route("id")]
        public IActionResult Details(long id)
        {
            var group = _groupService.GetById(id);
            if(group==null)
            {
                return NotFound();
            }
            return View(group.ToViewModel());
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, GroupViewModel model)
        {
            var group = _groupService.Update(model.ToServiceModel());
            if(group==null)
            {
                return NotFound();
            }

            group.Name = model.Name;
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
        public IActionResult Create(GroupViewModel model)
        {
            _groupService.Add(model.ToServiceModel());
            return RedirectToAction("Index");
        }
    }
}