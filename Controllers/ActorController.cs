using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using SeriesBoxd.Data;
using Business.Interfaces;
using Entities.ViewModels;

namespace SeriesBoxd.Controllers
{
    public class ActorController : Controller
    {
        private readonly ISerieService _serieService;
        private readonly IActorService _actorService;

        public ActorController(ISerieService serieService, IActorService actorService)
        {
            _serieService = serieService;
            _actorService = actorService;
        }

        // GET: Serie
        public IActionResult Index()
        {
            var model = _actorService.GetAll();
            return View(model);
        }

        // GET: Serie/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = _actorService.GetById(id.Value);
            if (actor == null)
            {
                return NotFound();
            }
            var actorVM = new ActorListVM
            {
                Name = actor.Name,
                CharacterName = actor.CharacterName,
                Series = actor.Series != null ? actor.Series : new List<Serie>()
            };

            return View(actorVM);
        }

        // GET: Serie/Create
        public IActionResult Create()
        {
            var serieList = _serieService.GetAll();
            ViewData["Series"] = new SelectList(serieList, "Id", "Name");
            return View();
        }

        // POST: Serie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,CharacterName,SerieIds")] ActorCreateVM actorCreateVM)
        {

            if (ModelState.IsValid)
            {
                var series = _serieService.GetAll().Where(s => actorCreateVM.SerieIds.Contains(s.Id)).ToList();
                var actor = new Actor
                {
                    Name = actorCreateVM.Name,
                    CharacterName = actorCreateVM.CharacterName,
                    Series = series
                };
                _actorService.Create(actor);
                return RedirectToAction(nameof(Index));


            }
            return View(actorCreateVM);
        }

        // GET: Serie/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = _actorService.GetById(id.Value);

            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: Serie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,CharacterName")] Actor actor)
        {
            if (id != actor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _actorService.Update(actor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: Serie/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = _actorService.GetById(id.Value);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Serie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var actor = _actorService.GetById(id);
            _actorService.Delete(actor);
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _actorService.GetById(id) != null;
        }
    }
}
