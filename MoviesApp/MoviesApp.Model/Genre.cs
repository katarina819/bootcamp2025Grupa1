using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Model
{
    /// <summary>
    /// Represents a Genre entity in the system.
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// Gets or sets the unique identifier for the genre.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the genre.
        /// This field is required and must not exceed 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public required string Name { get; set; }
    }
}
