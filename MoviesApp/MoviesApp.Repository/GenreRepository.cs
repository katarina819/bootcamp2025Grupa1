using System.Data;
using System.Text;
using Npgsql;
using Microsoft.Extensions.Logging;
using MoviesApp.Model;

namespace MoviesAppRepository
{
    public class GenreRepository : IGenreRepository
    {
        public readonly NpgsqlConnection _connection;
        public GenreRepository(NpgsqlConnection connection) { _connection = connection; }

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





























    }





}

