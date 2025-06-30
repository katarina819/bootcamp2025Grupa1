using System.ComponentModel.DataAnnotations;

namespace MoviesApp.DTO
{
    public class DirectorCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
      
    }
}
