using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Model
{
    /// <summary>
    /// Represents a Movie entity in the system.
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// Gets or sets the unique identifier for the movie.
        /// This field is required.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name/title of the movie.
        /// This field is required and must be between 1 and 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name cannot exceed 100 characters.")]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the duration of the movie in minutes.
        /// This field is required and must be within the range 1 to 600.
        /// </summary>
        [Required(ErrorMessage = "Duration is required.")]
        [Range(1, 600, ErrorMessage = "Duration must be in range 1-600 minutes.")]
        public required int Duration { get; set; }

        /// <summary>
        /// Gets or sets the rating of the movie.
        /// This is an optional field and must be between 0 and 10 if specified.
        /// </summary>
        [Range(0, 10, ErrorMessage = "Rating must be in range 0-10.")]
        public float? Rating { get; set; }

        /// <summary>
        /// Gets or sets the release year of the movie.
        /// This field is required and must be within the range 1888 to 2100.
        /// </summary>
        [Required(ErrorMessage = "Release year is required.")]
        [Range(1888, 2100, ErrorMessage = "Release year must be in range 1888-2100")]
        public required int ReleaseYear { get; set; }

        /// <summary>
        /// Gets or sets the description or synopsis of the movie.
        /// This is an optional field and cannot exceed 1000 characters.
        /// </summary>
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the director associated with the movie.
        /// This field is required.
        /// </summary>
        [Required]
        public Guid DirectorId { get; set; }
    }
}
