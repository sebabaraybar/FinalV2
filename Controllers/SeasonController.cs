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
using Business.Services;

namespace SeriesBoxd.Controllers
{
    public class SeasonController : Controller
    {
        private readonly ISeasonService _seasonService;

        public SeasonController(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        // GET: Serie
        public IActionResult Index(int serieId)
        {
            var seasonList = _seasonService.GetAll(serieId);
            return View(seasonList);
        }

        // GET: Serie/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var season = _seasonService.GetById(id.Value);
            if (season == null)
            {
                return NotFound();
            }

            return View(season);
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
        public IActionResult Create([Bind("Id,Number,Description,SerieId")] Season season)
        {
            if (ModelState.IsValid)
            {
                _seasonService.Create(season);
                return RedirectToAction(nameof(Index));
            }
            return View(season);
        }

        // GET: Serie/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var season = _seasonService.GetById(id.Value);
            if (season == null)
            {
                return NotFound();
            }
            return View(season);
        }

        // POST: Serie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Number,Description,SerieId")] Season season)
        {
            if (id != season.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _seasonService.Update(season);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeasonExist(season.Id))
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
            return View(season);
        }

        // GET: Serie/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var season = _seasonService.GetById(id.Value);
            if (season == null)
            {
                return NotFound();
            }

            return View(season);
        }

        // POST: Serie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var season = _seasonService.GetById(id);
            if (season != null)
            {
                _seasonService.Delete(season);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SeasonExist(int id)
        {
            return _seasonService.GetById(id) != null;
        }
    }
}
