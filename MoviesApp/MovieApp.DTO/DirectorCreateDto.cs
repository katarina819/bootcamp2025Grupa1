using System.ComponentModel.DataAnnotations;

namespace MoviesApp.DTO
{
    public class DirectorCreateDto
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }= string.Empty;
      
    }
}
