
using Entities.Models;

namespace Business.Interfaces;

public interface ISerieService
{
    public void Create(Serie serie);
    public void Update(Serie serie);
    public void Delete(int id);
    List<Serie> GetAll();
    List<Serie> GetAll(string filter);
    Serie? GetById(int id);
}
