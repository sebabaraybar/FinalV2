
using Entities.Models;

namespace Business.Services;

public interface ISeasonService
{
    void Create(Season season);
    List<Season> GetAll();
    void Update(Season season);
    void Delete(Season season);
    Season? GetById(int id);
    public IEnumerable<int> GetAllSeasonNumbers();
}