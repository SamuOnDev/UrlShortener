using System.ComponentModel.DataAnnotations;

namespace UrlShortenerApiBackend.Models.DataModels
{
    public class UrlList : BaseEntity
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public int UsesCounter { get; set; } = 0;
        [Required]
        public string Url { get; set; } = string.Empty;
        [Required]
        public string ShortUrl { get; set; } = string.Empty;
    }
}
