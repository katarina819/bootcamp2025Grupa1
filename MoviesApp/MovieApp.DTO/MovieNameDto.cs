using System;

namespace MoviesApp.DTO
{
    /// <summary>
    /// Data Transfer Object for representing a Movie with only its Id and Name.
    /// </summary>
    public class MovieNameDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the movie.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name/title of the movie.
        /// </summary>
        public string Name { get; set; }
    }
}
