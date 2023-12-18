
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Entities.Enum;

namespace Entities.Models
{
    public class Season
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public Rating Rating { get; set; }
        public int SerieId { get; set; }

        public virtual Serie Serie { get; set; }
        // public int ChapterId { get; set; }
        // public virtual List<Chapter> Chapters { get; set; }
    }
}