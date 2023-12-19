
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using SeriesBoxd.Data;

namespace Business.Services;

public class SeasonService : ISeasonService
{
    private readonly SerieContext _context;

    public SeasonService(SerieContext context)
    {
        _context = context;
    }
    public void Create(Season season)
    {
        _context.Add(season);
        _context.SaveChanges();
    }

    public void Delete(Season season)
    {
        _context.Remove(season);
        _context.SaveChanges();
    }

    public List<Season> GetAll()
    {
        return _context.Season
        .Include(x => x.Serie)
        .ToList();
    }

    public IEnumerable<int> GetAllSeasonNumbers()
    {
        var seasons = GetAll();
        return seasons.Select(season => season.Number);
    }

    public Season? GetById(int id)
    {
        var season = _context.Season
        .FirstOrDefault(s => s.Id == id);
        return season;
    }

    public void Update(Season season)
    {
        _context.Update(season);
        _context.SaveChanges();
    }
}