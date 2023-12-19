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

namespace SeriesBoxd.Controllers
{
    public class SerieController : Controller
    {
        private readonly ISerieService _serieService;

        public SerieController(ISerieService serieService)
        {
            _serieService = serieService;
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Serie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Actor,Director,Genre")] Serie serie)
        {
            if (ModelState.IsValid)
            {
                _serieService.Create(serie);
                return RedirectToAction(nameof(Index));
            }
            return View(serie);
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
        public IActionResult Edit(int id, [Bind("Id,Name,Actor,Director,Genre")] Serie serie)
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
