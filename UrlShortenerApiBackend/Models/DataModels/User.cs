using System.ComponentModel.DataAnnotations;

namespace UrlShortenerApiBackend.Models.DataModels
{
    public class User
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        
    }
}
