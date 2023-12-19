
using System.ComponentModel.DataAnnotations;
using Entities.Enum;

namespace Entities.ViewModels;
public class SeasonCreateVM
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string Description { get; set; }
    [Display(Name = "Serie's Name")]
    public int SerieId { get; set; }
    public Rating Rating { get; set; }

}