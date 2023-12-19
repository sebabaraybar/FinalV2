
using System.ComponentModel.DataAnnotations;
using Entities.Enum;

namespace Entities.ViewModels;
public class ActorCreateVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CharacterName { get; set; }
    [Display(Name = "Serie's Name")]

    public List<int> SerieIds { get; set; }

}