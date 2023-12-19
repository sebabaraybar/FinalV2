using Entities.Models;

namespace Entities.ViewModels;

public class SerieVM
{
    public List<Serie> Series { get; set; } = new List<Serie>();
    public string? NameFilter { get; set; }
}