using MoviesApp.DTO;
using MoviesApp.Model;
using MoviesApp.Pagination;
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
        public async Task<Paginated<MovieDto>> GetAllMoviesAsync(int page = 1, int pageSize = 10)
        {
            var result = new Paginated<MovieDto>
            {
                Page = page,
                PageSize = pageSize,
                Items = new List<MovieDto>()
            };

            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using var countRows = new NpgsqlCommand("SELECT COUNT(*) FROM \"Movie\"", _connection);
            result.TotalCount = Convert.ToInt32(await countRows.ExecuteScalarAsync());

            var offset = (page - 1) * pageSize;
            using var getRows = new NpgsqlCommand("SELECT * FROM \"MovieView\" ORDER BY \"Name\" LIMIT @PageSize OFFSET @Offset", _connection);
            getRows.Parameters.AddWithValue("PageSize", pageSize);
            getRows.Parameters.AddWithValue("Offset", offset);

            using var reader = await getRows.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                string genreString = reader.IsDBNull(4) ? "" : reader.GetString(5);

                var genres = genreString
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .ToList();

                result.Items.Add(new MovieDto
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Duration = reader.GetInt32(2),
                    ReleaseYear = reader.GetInt32(3),
                    Rating = reader.GetFloat(4),
                    Genres = genres,
                });

            }
            return result;
        }

        public async Task DeleteMovieAsync(Guid id)
        {

            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }

            using (var command = new NpgsqlCommand("DELETE FROM \"Movie\" WHERE \"Id\" = @id;", _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
            }

            using (var command = new NpgsqlCommand("DELETE FROM \"MovieGenre\" WHERE \"MovieId\" = @id", _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
            }

            using (var command = new NpgsqlCommand("DELETE FROM \"MovieLanguage\" WHERE \"MovieId\" = @id", _connection))
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
                        Description = reader.GetString(5),
                        DirectorId = reader.GetGuid(6)
                    };
                }
            }
        }

        public async Task<Paginated<Movie>> GetMoviesSortedAsync(string sortBy, string order, int page = 1, int pageSize = 10)
        {
            var result = new Paginated<Movie>
            {
                Page = page,
                PageSize = pageSize,
                Items = new List<Movie>()
            };

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
                    query += " ASC";
                    break;
                case "DESC":
                    query += " DESC";
                    break;
            }
            query += " LIMIT @PageSize OFFSET @Offset;";

            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using var countRows = new NpgsqlCommand("SELECT COUNT(*) FROM \"Movie\"", _connection);
            result.TotalCount = Convert.ToInt32(await countRows.ExecuteScalarAsync());

            var offset = (page - 1) * pageSize;
            using var getRows = new NpgsqlCommand(query, _connection);
            getRows.Parameters.AddWithValue("PageSize", pageSize);
            getRows.Parameters.AddWithValue("Offset", offset);

            using var reader = await getRows.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Items.Add(new Movie
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Duration = reader.GetInt32(2),
                    Rating = reader.GetFloat(3),
                    ReleaseYear = reader.GetInt32(4),
                    Description = reader.GetString(5)
                });

            }
            return result;
        }
       

        public async Task<Paginated<Movie>> GetMoviesFilterNameAsync(string filter, int page = 1, int pageSize = 10)
        {
            var result = new Paginated<Movie>
            {
                Page = page,
                PageSize = pageSize,
                Items = new List<Movie>()
            };
            var query = "SELECT * FROM \"Movie\" WHERE LOWER(\"Name\") ILIKE @filter LIMIT @PageSize OFFSET @Offset;";
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using var countRows = new NpgsqlCommand("SELECT COUNT(*) FROM \"Movie\" WHERE LOWER(\"Name\") ILIKE @filter", _connection);
            countRows.Parameters.AddWithValue("@filter", filter);
            result.TotalCount = Convert.ToInt32(await countRows.ExecuteScalarAsync());

            var offset = (page - 1) * pageSize;
            using var command = new NpgsqlCommand(query, _connection);
            command.Parameters.AddWithValue("@filter", filter);
            command.Parameters.AddWithValue("@PageSize", pageSize);
            command.Parameters.AddWithValue("@Offset", offset);
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.Items.Add(new Movie
                    {
                        Id = reader.GetGuid(0),
                        Name = reader.GetString(1),
                        Duration = reader.GetInt32(2),
                        Rating = reader.GetFloat(3),
                        ReleaseYear = reader.GetInt32(4),
                        Description = reader.GetString(5)
                    });
                }
            }
            
            return result;
        }

        public async Task<IList<string>> GetGenresByMovieIdAsync(Guid movieId)
        {
            var genres = new List<string>();
            var query = "SELECT g.\"Name\" FROM \"MovieGenre\" mg\r\n" +
                "left join \"Genre\" g on mg.\"GenreId\" = g.\"Id\"\r\n" +
                "WHERE mg.\"MovieId\" = @id;";
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", movieId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        genres.Add
                        (
                             reader.GetString(0)
                        );
                    }
                }
            }
            return genres;
        }

        public async Task<IList<string>> GetLanguagesByMovieIdAsync(Guid movieId)
        {
            var genres = new List<string>();
            var query = "SELECT l.\"Name\" FROM \"MovieLanguage\" ml\r\n" +
                "left join \"Language\" l on ml.\"LanguageId\" = l.\"Id\"\r\n" +
                "WHERE ml.\"MovieId\" = @id;";
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", movieId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        genres.Add
                        (
                             reader.GetString(0)
                        );
                    }
                }
            }
            return genres;
        }
        public async Task<DirectorCreateDto> GetDirectorByMovieIdAsync(Guid movieId)
        {
            
            var query = "SELECT d.\"FirstName\", d.\"LastName\" FROM \"MovieDirector\" md\r\n" +
                "LEFT JOIN \"Director\" d ON md.\"DirectorId\" = d.\"Id\"\r\n" +
                "WHERE md.\"MovieId\" = @id;";
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", movieId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    await reader.ReadAsync();
                    return new DirectorCreateDto
                    {
                        FirstName = reader.GetString(0),
                        LastName = reader.GetString(1)
                    };
                }
            }
           
        }

    }
}


