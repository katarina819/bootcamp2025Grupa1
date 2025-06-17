using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Model
{
    public class Director
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(100, ErrorMessage = "FirstName cannot exceed 100 characters.")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(100, ErrorMessage = "LastName cannot exceed 100 characters.")]
        public required string LastName { get; set; }

    }
}
