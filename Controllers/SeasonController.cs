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
using Entities.ViewModels;

namespace SeriesBoxd.Controllers
{
    public class SeasonController : Controller
    {
        private readonly ISeasonService _seasonService;
        private readonly ISerieService _serieService;

        public SeasonController(ISeasonService seasonService, ISerieService serieService)
        {
            _seasonService = seasonService;
            _serieService = serieService;
        }

        public IActionResult Index()
        {
            var seasons = _seasonService.GetAll();
            return View(seasons);
        }


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

        public IActionResult Create()
        {
            var series = _serieService.GetAll();
            ViewData["Series"] = new SelectList(series, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Number,Description,Rating,SerieId")] SeasonCreateVM seasonCreateVM)
        {
            if (ModelState.IsValid)
            {
                var seasonToCreate = new Season
                {
                    Number = seasonCreateVM.Number,
                    Description = seasonCreateVM.Description,
                    Rating = seasonCreateVM.Rating,
                    SerieId = seasonCreateVM.SerieId
                };
                _seasonService.Create(seasonToCreate);
                // var seasonNumbers = _seasonService.GetAllSeasonNumbers();
                return RedirectToAction(nameof(Index));
            }
            return View(seasonCreateVM);
        }
        // GET: Serie/Edit/5
        public IActionResult Edit(int? id)
        {
            var series = _serieService.GetAll();
            ViewData["Series"] = new SelectList(series, "Id", "Name");
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
        public IActionResult RateSeason(int? id)
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
            var rateSeasonVM = new RateSeasonVM
            {
                Id = season.Id,
                Number = season.Number,
                Rating = season.Rating
            };

            return View(rateSeasonVM);
        }

        [HttpPost]
        public IActionResult RateSeason(RateSeasonVM rateSeasonVM)
        {
            var season = _seasonService.GetById(rateSeasonVM.Id);
            if (season == null)
            {
                return NotFound();
            }
            season.RatingCount++;
            season.RatingPoints += (int)rateSeasonVM.Rating;
            _seasonService.Update(season);
            var updatedRating = _seasonService.CalculateRating(season.Id);
            rateSeasonVM.UpdatedRating = updatedRating;

            return View(rateSeasonVM);
        }

        private bool SeasonExist(int id)
        {
            return _seasonService.GetById(id) != null;
        }
    }
}
