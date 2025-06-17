using MoviesApp.Repository.Common;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Repository
{
    public class MovieRepository : IMovieRepository
    {
        public readonly NpgsqlConnection _connection;
        public MovieRepository(NpgsqlConnection connection) => _connection = connection ?? throw new ArgumentNullException(nameof(connection));
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
                            DescriptionAttribute = reader.GetString(5)
                        };
                        movies.Add(movie);

                    }
                }
            }
            return movies;
        }
    }
}

