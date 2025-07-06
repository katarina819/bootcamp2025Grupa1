using System.Data;
using System.Text;
using Npgsql;
using Microsoft.Extensions.Logging;
using MoviesApp.Model;

namespace MoviesAppRepository
{
    /// <summary>
    /// Repository for managing Genre entities.
    /// Provides CRUD operations for genres using a PostgreSQL database.
    /// </summary>
    public class GenreRepository : IGenreRepository
    {
        public readonly NpgsqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of <see cref="GenreRepository"/> with the given PostgreSQL connection.
        /// </summary>
        /// <param name="connection">The NpgsqlConnection to the database.</param>
        public GenreRepository(NpgsqlConnection connection) { _connection = connection; }

        /// <summary>
        /// Retrieves all genres from the database.
        /// </summary>
        /// <returns>A list of anonymous objects representing genres with Id and Name.</returns>
        public async Task<IList<object>> GetAllGenreAsync()
        {
            var genres = new List<object>();
            var query = "SELECT * FROM \"Genre\";";
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using (var command = new NpgsqlCommand(query, _connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var genre = new
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1)
                        };
                        genres.Add(genre);
                    }
                }
            }
            return genres;
        }

        /// <summary>
        /// Deletes a genre from the database by its Id.
        /// </summary>
        /// <param name="id">The unique identifier of the genre to delete.</param>
        public async Task DeleteGenreAsync(Guid id)
        {
            var query = "DELETE FROM \"Genre\" WHERE \"Id\" = @id;";
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates an existing genre in the database.
        /// </summary>
        /// <param name="genre">The genre object containing updated information.</param>
        public async Task UpdateGenreAsync(Genre genre)
        {
            var query = "UPDATE \"Genre\" SET \"Name\" = @name WHERE \"Id\" = @id;";
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@name", genre.Name);
                command.Parameters.AddWithValue("@id", genre.Id);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Adds a new genre to the database.
        /// </summary>
        /// <param name="genre">The genre object to add.</param>
        public async Task AddGenreAsync(Genre genre)
        {
            var query = "INSERT INTO \"Genre\" VALUES (@id, @name)";
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", genre.Id);
                command.Parameters.AddWithValue("@name", genre.Name);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Retrieves a genre from the database by its Id.
        /// </summary>
        /// <param name="id">The unique identifier of the genre to retrieve.</param>
        /// <returns>The <see cref="Genre"/> object with the specified Id.</returns>
        public async Task<Genre> GetGenreByIdAsync(Guid id)
        {
            var query = "SELECT * FROM \"Genre\" WHERE \"Id\" = @id;";
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    await reader.ReadAsync();
                    return new Genre
                    {
                        Id = reader.GetGuid(0),
                        Name = reader.GetString(1)
                    };
                }
            }
        }

        /// <summary>
        /// Retrieves the list of genre names associated with a specific movie.
        /// </summary>
        /// <param name="movieId">The unique identifier of the movie.</param>
        /// <returns>A list of genre names for the specified movie.</returns>
        public async Task<IList<string>> GetGenresByMovieIdAsync(Guid movieId)
        {
            var genres = new List<string>();

            var query = @"
        SELECT g.""Id"", g.""Name""
        FROM ""MovieGenre"" mg
        LEFT JOIN ""Genre"" g ON mg.""GenreId"" = g.""Id""
        WHERE mg.""MovieId"" = @id;
    ";

            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }

            using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("id", movieId);


                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        genres.Add

                        (

                         reader.GetString(1)
                        );
                    }
                }
            }

            return genres;
        }
    }
}
