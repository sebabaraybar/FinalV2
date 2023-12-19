



using Business.Interfaces;
using Entities.Models;
using SeriesBoxd.Data;

namespace Business.Services;

public class ActorService : IActorService
{

    private readonly SerieContext _context;
    public ActorService(SerieContext context)
    {
        _context = context;
    }

    public void Create(Actor actor)
    {
        _context.Add(actor);
        _context.SaveChanges();
    }

    public void Delete(Actor actor)
    {
        _context.Remove(actor);
        _context.SaveChanges();
    }

    public List<Actor> GetAll()
    {
        return _context.Actor.ToList();
    }

    public Actor? GetById(int id)
    {
        var actor = _context.Actor
        .FirstOrDefault(s => s.Id == id);
        return actor;
    }

    public void Update(Actor actor)
    {
        _context.Update(actor);
        _context.SaveChanges();
    }

}