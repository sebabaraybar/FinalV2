

using Business.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using SeriesBoxd.Data;

namespace Business.Services;

public class SerieService : ISerieService
{
    private readonly SerieContext _context;
    public SerieService(SerieContext context)
    {
        _context = context;
    }
    public void Create(Serie serie)
    {
        _context.Add(serie);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var toDelete = GetById(id);
        if (toDelete != null)
        {
            _context.Remove(toDelete);
            _context.SaveChanges();
        }
    }

    public List<Serie> GetAll()
    {
        var query = GetQuery();
        return query.ToList();
    }

    public List<Serie> GetAll(string filter)
    {
        var query = GetQuery();
        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(s => s.Name.ToLower().Contains(filter.ToLower()) || s.Director.ToLower().Contains(filter.ToLower()));
        }
        return query.ToList();
    }

    public Serie? GetById(int id)
    {
        var serie = GetQuery()
        .Include(s => s.Seasons)
        .FirstOrDefault(s => s.Id == id);
        return serie;
    }

    public void Update(Serie serie)
    {
        _context.Update(serie);
        _context.SaveChanges();
    }


    private IQueryable<Serie> GetQuery()
    {
        return from serie in _context.Serie select serie;
    }
}