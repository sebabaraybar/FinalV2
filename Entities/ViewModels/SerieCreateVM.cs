
using Entities.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Entities.ViewModels;

public class SerieCreateVM
{
    public string Name { get; set; }
    public string Director { get; set; }
    public Genre Genre { get; set; }
    public List<int> ActorIds { get; set; }
}