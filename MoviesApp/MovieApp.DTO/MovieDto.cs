using System;
using System.Collections.Generic;

namespace MoviesApp.DTO
{
    /// <summary>
    /// Data Transfer Object for general information about a Movie.
    /// </summary>
    public class MovieDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the movie.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name/title of the movie.
        /// This is a required field.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the duration of the movie in minutes.
        /// This is a required field.
        /// </summary>
        public required int Duration { get; set; }

        /// <summary>
        /// Gets or sets the rating of the movie (e.g., IMDb rating).
        /// This is an optional field.
        /// </summary>
        public float? Rating { get; set; }

        /// <summary>
        /// Gets or sets the release year of the movie.
        /// This is a required field.
        /// </summary>
        public required int ReleaseYear { get; set; }

        /// <summary>
        /// Gets or sets the list of genre names associated with the movie.
        /// This is a required field.
        /// </summary>
        public required List<string> Genres { get; set; }
    }
}
