using MoviesApp.Model;
using MoviesApp.Repository.Common;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Repository
{
    public class MovieRepository : IMovieRepository
    {
        public readonly NpgsqlConnection _connection;
        public MovieRepository(NpgsqlConnection connection) => _connection = connection;
        public async Task<IList<object>> GetAllMoviesAsync()
        {
            var movies = new List<object>();
            var query = "SELECT * FROM \"Movie\";";

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
                        var movie = new
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            Duration = reader.GetInt32(2),
                            Rating = reader.GetFloat(3),
                            ReleaseYear = reader.GetInt32(4),
                            Description = reader.GetString(5)
                        };
                        movies.Add(movie);

                    }
                }
            }
            return movies;
        }

        public async Task DeleteMovieAsync(Guid id)
        {
            var query = "DELETE FROM \"Movie\" WHERE \"Id\" = @id;";

            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }

            using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            var query = "UPDATE \"Movie\" SET \"Name\" = @name, \"Duration\" = @duration, \"Rating\" = @rating, \"ReleaseYear\" = @releaseYear, \"Description\" = @description WHERE \"Id\" = @id;";
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }

            using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@name", movie.Name);
                command.Parameters.AddWithValue("@duration", movie.Duration);
                command.Parameters.AddWithValue("@rating", movie.Rating);
                command.Parameters.AddWithValue("@releaseYear", movie.ReleaseYear);
                command.Parameters.AddWithValue("@description", movie.Description);
                command.Parameters.AddWithValue("@id", movie.Id);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task AddMovieAsync(Movie movie)
        {
            var query = "INSERT INTO \"Movie\" VALUES (@id, @name, @duration, @rating, @releaseYear, @description)";
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", movie.Id);
                command.Parameters.AddWithValue("@name", movie.Name);
                command.Parameters.AddWithValue("@duration", movie.Duration);
                command.Parameters.AddWithValue("@rating", movie.Rating);
                command.Parameters.AddWithValue("@releaseYear", movie.ReleaseYear);
                command.Parameters.AddWithValue("@description", movie.Description);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<Movie> GetMovieByIdAsync(Guid id)
        {
            var query = "SELECT * FROM \"Movie\" WHERE \"Id\" = @id;";
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    await reader.ReadAsync();
                    return new Movie
                    {
                        Id = reader.GetGuid(0),
                        Name = reader.GetString(1),
                        Duration = reader.GetInt32(2),
                        Rating = reader.GetFloat(3),
                        ReleaseYear = reader.GetInt32(4),
                        Description = reader.GetString(5)
                    };
                }
            }
        }

        public async Task<List<Movie>> GetMoviesSortedAsync(string sortBy, string order)
        {
            var movies = new List<Movie>();
            var query = "SELECT * FROM \"Movie\" ORDER BY ";
            switch (sortBy)
            {
                case "Name":
                    query += "\"Name\"";
                    break;
                case "Duration":
                    query += "\"Duration\"";
                    break;
                case "Rating":
                    query += "\"Rating\"";
                    break;
                case "ReleaseYear":
                    query += "\"ReleaseYear\"";
                    break;
                case "Description":
                    query += "\"Description\"";
                    break;
            }
            switch (order)
            {
                case "ASC":
                    query += " ASC;";
                    break;
                case "DESC":
                    query += " DESC;";
                    break;
            }
            
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
                        var movie = new Movie
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            Duration = reader.GetInt32(2),
                            Rating = reader.GetFloat(3),
                            ReleaseYear = reader.GetInt32(4),
                            Description = reader.GetString(5)
                        };
                        movies.Add(movie);
                    }
                }
            }
            return movies;
        }

        public async Task<List<Movie>> GetMoviesFilterNameAsync(string filter)
        {
            var movies = new List<Movie>();
            var query = "SELECT * FROM \"Movie\" WHERE LOWER(\"Name\") LIKE @filter;";
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@filter", filter);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var movie = new Movie
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            Duration = reader.GetInt32(2),
                            Rating = reader.GetFloat(3),
                            ReleaseYear = reader.GetInt32(4),
                            Description = reader.GetString(5)
                        };
                        movies.Add(movie);
                    }
                }
            }
            return movies;
        }
    }
}


