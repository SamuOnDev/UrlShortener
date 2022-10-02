using System.ComponentModel.DataAnnotations;

namespace UrlShortenerApiBackend.Models.DataModels
{
    public class UrlList
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public int UsesCounter { get; set; } = 0;
        [Required]
        public string Url { get; set; } = string.Empty;
        public string ShortUrl { get; set; } = string.Empty;
    }
}
