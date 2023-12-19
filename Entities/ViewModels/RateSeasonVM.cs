


using System.ComponentModel.DataAnnotations;
using Entities.Enum;

namespace Entities.ViewModels
{
    public class RateSeasonVM
    {
        public int Id { get; set; }
        [Display(Name = "Season N°")]
        public int Number { get; set; }
        public Rating Rating { get; set; }
        [Display(Name = "Name of Serie")]
        public int SerieId { get; set; }
        public int RatingCount { get; set; }
        public int RatingPoints { get; set; }
        public double? UpdatedRating { get; set; }
    }
}