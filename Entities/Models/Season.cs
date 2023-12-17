
namespace Entities.Models
{
    public class Season
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }

        // public int ChapterId { get; set; }
        // public virtual List<Chapter> Chapters { get; set; }
    }
}