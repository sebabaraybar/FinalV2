
namespace Entities.Models
{
    public class Serie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Actor { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }

        // public int ActorId { get; set; }
        public int SeasonId { get; set; }
        // public virtual List<Actor> Actors { get; set; }
        public virtual List<Season> Seasons { get; set; }
    }
}