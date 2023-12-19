

using Entities.Models;

namespace Business.Interfaces;

public interface IActorService
{
    List<Actor> GetAll();
    Actor GetById(int value);
    void Create(Actor actor);
    void Update(Actor actor);
    void Delete(Actor actor);
}