using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eTickets.Controllers
{

    [Authorize(Roles = UserRoles.Admin)]
    public class ActorsController : Controller
    {
        private readonly Data.Services.IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL, Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var actorsDetails = await _service.GetByIdAsync(id);

            if (actorsDetails == null)
            {
                return View("NotFound");
            }
            return View(actorsDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var actorsDetails = await _service.GetByIdAsync(id);
            if (actorsDetails == null)
            {
                return View("NotFound");
            }
            return View(actorsDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var actorsDetails = await _service.GetByIdAsync(id);
            if (actorsDetails == null)
            {
                return View("NotFound");
            }
            return View(actorsDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
              var actorsDetails = await _service.GetByIdAsync(id);
            if (actorsDetails == null)
            return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }

}