
namespace Entities.Models;

public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CharacterName { get; set; }

    public int SerieId { get; set; }
    public virtual List<Serie> Series { get; set; }

}