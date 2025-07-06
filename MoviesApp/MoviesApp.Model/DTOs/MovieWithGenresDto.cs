using System;

namespace MoviesApp.DTO
{
    /// <summary>
    /// Data Transfer Object representing a Movie along with its associated Genre information.
    /// </summary>
    public class MovieWithGenreDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the movie.
        /// </summary>
        public Guid MovieId { get; set; }

        /// <summary>
        /// Gets or sets the name/title of the movie.
        /// </summary>
        public string MovieName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the duration of the movie in minutes.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the rating of the movie.
        /// </summary>
        public float Rating { get; set; }

        /// <summary>
        /// Gets or sets the release year of the movie.
        /// </summary>
        public int ReleaseYear { get; set; }

        /// <summary>
        /// Gets or sets the description or synopsis of the movie.
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the unique identifier of the genre associated with the movie.
        /// </summary>
        public Guid GenreId { get; set; }

        /// <summary>
        /// Gets or sets the name of the genre associated with the movie.
        /// </summary>
        public string GenreName { get; set; } = null!;
    }
}
