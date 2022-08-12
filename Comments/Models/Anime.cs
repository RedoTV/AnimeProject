namespace Animes.Models
{
    public class Anime
    {
        public int Id { get; set; }
        public string? AnimeName { get; set; }
        public string? AnimeDescription { get; set; }
        public int Priority { get; set; }
        public string? Genres { get; set; }
        public string? PathToPreview { get; set; }
    }
}
