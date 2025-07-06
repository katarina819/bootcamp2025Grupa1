using System.ComponentModel.DataAnnotations;

namespace MoviesApp.DTO
{
    /// <summary>
    /// Data Transfer Object for creating a new Director.
    /// </summary>
    public class DirectorCreateDto
    {
        /// <summary>
        /// Gets or sets the name of the director.
        /// This field is required and has a maximum length of 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Director name is required.")]
        [StringLength(100, ErrorMessage = "Director name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;
    }
}
