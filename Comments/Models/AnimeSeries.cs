using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Animes.Models
{
    public class AnimeSeries
    {
        
        public int Id  { get; set; }
        public int SeriesNumber { get; set; }
        public string PathToVideo { get; set; } = null!;

        public Anime Anime { get; set; } = null!;
    }
}
