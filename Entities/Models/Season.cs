
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Entities.Enum;

namespace Entities.Models
{
    public class Season
    {
        public int Id { get; set; }
        [Display(Name = "Season N°")]
        public int Number { get; set; }
        public string Description { get; set; }
        public Rating Rating { get; set; }
        [Display(Name = "Name of Serie")]
        public int SerieId { get; set; }
        public virtual Serie Serie { get; set; }
        // public int ChapterId { get; set; }
        // public virtual List<Chapter> Chapters { get; set; }
    }
}