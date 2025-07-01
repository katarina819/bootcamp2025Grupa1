using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Model
{
    public class Director
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public required string Name { get; set; }

    }
}
