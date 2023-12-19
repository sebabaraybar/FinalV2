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
using Microsoft.AspNetCore.Authorization;

namespace SeriesBoxd.Controllers
{
    public class SerieController : Controller
    {
        private readonly ISerieService _serieService;
        private readonly IActorService _actorService;

        public SerieController(ISerieService serieService, IActorService actorService)
        {
            _serieService = serieService;
            _actorService = actorService;
        }

        // GET: Serie
        public IActionResult Index()
        {
            var model = _serieService.GetAll();
            return View(model);
        }

        // GET: Serie/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = _serieService.GetById(id.Value);
            if (serie == null)
            {
                return NotFound();
            }

            return View(serie);
        }

        // GET: Serie/Create
        [Authorize]
        public IActionResult Create()
        {
            var actorList = _actorService.GetAll();
            ViewData["Actors"] = new SelectList(actorList, "Id", "Name");
            return View();
        }

        // POST: Serie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Director,Genre,ActorIds")] SerieCreateVM serieCreateVM)
        {

            if (ModelState.IsValid)
            {
                var actors = _actorService.GetAll().Where(s => serieCreateVM.ActorIds.Contains(s.Id)).ToList();
                var serie = new Serie
                {
                    Name = serieCreateVM.Name,
                    Director = serieCreateVM.Director,
                    Genre = serieCreateVM.Genre,
                    Actors = actors
                };
                if (serieCreateVM.ActorIds != null)
                {
                    _serieService.Create(serie);
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(serieCreateVM);
        }

        // GET: Serie/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = _serieService.GetById(id.Value);

            if (serie == null)
            {
                return NotFound();
            }
            return View(serie);
        }

        // POST: Serie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Director,Genre")] Serie serie)
        {
            if (id != serie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _serieService.Update(serie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SerieExists(serie.Id))
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
            return View(serie);
        }

        // GET: Serie/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = _serieService.GetById(id.Value);
            if (serie == null)
            {
                return NotFound();
            }

            return View(serie);
        }

        // POST: Serie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _serieService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SerieExists(int id)
        {
            return _serieService.GetById(id) != null;
        }
    }
}
