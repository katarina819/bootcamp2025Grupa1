using MoviesAppModel;
using Npgsql;
using System.Data;

namespace MoviesAppRepository
{
    public class MovieRepository
    {
        private readonly string _connectionString;

        public MovieRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<MovieWithGenreDto>> GetMoviesWithGenresFlatAsync()
        {
            var result = new List<MovieWithGenreDto>();

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var sql = @"
            SELECT 
                m.""Id"" AS MovieId,
                m.""Name"" AS MovieName,
                m.""Duration"",
                m.""Rating"",
                m.""ReleaseYear"",
                m.""Description"",
                g.""Id"" AS GenreId,
                g.""Name"" AS GenreName
            FROM ""Movie"" m
            INNER JOIN ""MovieGenre"" mg ON m.""Id"" = mg.""MovieId""
            INNER JOIN ""Genre"" g ON mg.""GenreId"" = g.""Id""
            ORDER BY m.""Name"", g.""Name"";
        ";

            await using var cmd = new NpgsqlCommand(sql, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var dto = new MovieWithGenreDto
                {
                    MovieId = reader.GetGuid(0),
                    MovieName = reader.GetString(1),
                    Duration = reader.GetInt32(2),
                    Rating = (float)reader.GetDouble(3),
                    ReleaseYear = reader.GetInt32(4),
                    Description = reader.GetString(5),
                    GenreId = reader.GetGuid(6),
                    GenreName = reader.GetString(7),
                };

                result.Add(dto);
            }

            return result;
        }


        public async Task<List<MovieModels>> GetAllAsync()
        {
            var movies = new List<MovieModels>();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT * FROM \"Movie\"", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                movies.Add(new MovieModels
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Duration = reader.GetInt32(2),
                    Rating = reader.GetFloat(3),
                    ReleaseYear = reader.GetInt32(4),
                    Description = reader.GetString(5)
                });
            }

            return movies;
        }

        public async Task<MovieModels?> GetByIdAsync(Guid id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand("SELECT * FROM \"Movie\" WHERE \"Id\" = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new MovieModels
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Duration = reader.GetInt32(2),
                    Rating = reader.GetFloat(3),
                    ReleaseYear = reader.GetInt32(4),
                    Description = reader.GetString(5)
                };
            }

            return null;
        }

        public async Task AddAsync(MovieModels movie)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand(@"INSERT INTO ""Movie"" (""Id"", ""Name"", ""Duration"", ""Rating"", ""ReleaseYear"", ""Description"")
            VALUES (@Id, @Name, @Duration, @Rating, @ReleaseYear, @Description)", conn);

            cmd.Parameters.AddWithValue("Id", movie.Id);
            cmd.Parameters.AddWithValue("Name", movie.Name);
            cmd.Parameters.AddWithValue("Duration", movie.Duration);
            cmd.Parameters.AddWithValue("Rating", movie.Rating);
            cmd.Parameters.AddWithValue("ReleaseYear", movie.ReleaseYear);
            cmd.Parameters.AddWithValue("Description", movie.Description);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<bool> UpdateAsync(MovieModels movie)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand(@"UPDATE ""Movie"" SET ""Name"" = @Name, ""Duration"" = @Duration, ""Rating"" = @Rating,
            ""ReleaseYear"" = @ReleaseYear, ""Description"" = @Description WHERE ""Id"" = @Id", conn);

            cmd.Parameters.AddWithValue("Id", movie.Id);
            cmd.Parameters.AddWithValue("Name", movie.Name);
            cmd.Parameters.AddWithValue("Duration", movie.Duration);
            cmd.Parameters.AddWithValue("Rating", movie.Rating);
            cmd.Parameters.AddWithValue("ReleaseYear", movie.ReleaseYear);
            cmd.Parameters.AddWithValue("Description", movie.Description);

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand(@"DELETE FROM ""Movie"" WHERE ""Id"" = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}
