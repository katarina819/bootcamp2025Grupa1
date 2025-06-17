using System.Data;
using System.Text;
using MoviesAppModel;
using Npgsql;
using Microsoft.Extensions.Logging;

namespace MoviesAppRepository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<GenreRepository> _logger;

        public GenreRepository(string connectionString, ILogger<GenreRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<IEnumerable<GenreModels>> GetAllAsync(string? search, string? sort, int? page, int? pageSize)
        {
            var genres = new List<GenreModels>();

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var query = new StringBuilder("SELECT * FROM \"Genre\" WHERE 1=1");

            if (!string.IsNullOrWhiteSpace(search))
                query.Append(" AND \"Name\" ILIKE @Name");

            if (!string.IsNullOrWhiteSpace(sort))
            {
                var direction = sort.StartsWith("-") ? "DESC" : "ASC";
                var field = sort.TrimStart('-');

                if (field.ToLower() == "name")
                    query.Append($" ORDER BY \"Name\" {direction}");
            }
            else
            {
                query.Append(" ORDER BY \"Name\" ASC");
            }

            // Default vrijednosti ako nisu zadani
            int actualPage = page ?? 1;
            int actualPageSize = pageSize ?? 10;

            query.Append(" OFFSET @Offset LIMIT @Limit");

            await using var cmd = new NpgsqlCommand(query.ToString(), conn);
            if (!string.IsNullOrWhiteSpace(search))
                cmd.Parameters.AddWithValue("Name", $"%{search}%");

            cmd.Parameters.AddWithValue("Offset", (actualPage - 1) * actualPageSize);
            cmd.Parameters.AddWithValue("Limit", actualPageSize);

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                genres.Add(new GenreModels
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1)
                });
            }

            return genres;
        }


        public async Task<GenreModels?> GetByIdAsync(Guid id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand("SELECT \"Id\", \"Name\" FROM \"Genre\" WHERE \"Id\" = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new GenreModels
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1)
                };
            }

            return null;
        }

        public async Task AddAsync(GenreModels genre)
        {
            if (genre.Id == Guid.Empty)
                genre.Id = Guid.NewGuid();

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand("INSERT INTO \"Genre\" (\"Id\", \"Name\") VALUES (@id, @name)", conn);
            cmd.Parameters.AddWithValue("id", genre.Id);
            cmd.Parameters.AddWithValue("name", genre.Name);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(GenreModels genre)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand("UPDATE \"Genre\" SET \"Name\" = @name WHERE \"Id\" = @id", conn);
            cmd.Parameters.AddWithValue("id", genre.Id);
            cmd.Parameters.AddWithValue("name", genre.Name);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand("DELETE FROM \"Genre\" WHERE \"Id\" = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<GenreModels>> GetFilteredAsync(string? search, string? sort, int page, int pageSize)
        {
            var genres = new List<GenreModels>();

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var query = new StringBuilder("SELECT * FROM \"Genre\" WHERE 1=1");

            if (!string.IsNullOrWhiteSpace(search))
                query.Append(" AND \"Name\" ILIKE @Name");

            if (!string.IsNullOrWhiteSpace(sort))
            {
                var direction = sort.StartsWith("-") ? "DESC" : "ASC";
                var field = sort.TrimStart('-');

                if (field.ToLower() == "name")
                    query.Append($" ORDER BY \"Name\" {direction}");
            }
            else
            {
                query.Append(" ORDER BY \"Name\" ASC");
            }

            query.Append(" OFFSET @Offset LIMIT @Limit");

            await using var cmd = new NpgsqlCommand(query.ToString(), conn);
            if (!string.IsNullOrWhiteSpace(search))
                cmd.Parameters.AddWithValue("Name", $"%{search}%");

            cmd.Parameters.AddWithValue("Offset", (page - 1) * pageSize);
            cmd.Parameters.AddWithValue("Limit", pageSize);

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                genres.Add(new GenreModels
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1)
                });
            }

            return genres;
        }

    }
}

