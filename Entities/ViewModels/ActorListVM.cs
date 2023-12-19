
using Entities.Models;

namespace Entities.ViewModels;

public class ActorListVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CharacterName { get; set; }

    public List<Serie> Series { get; set; }
}