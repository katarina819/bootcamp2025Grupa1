using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using MoviesApp.Model;
using MoviesApp.Repository.Common;

namespace MoviesApp.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly NpgsqlConnection _conn;

        public LanguageRepository(NpgsqlConnection conn) => _conn = conn;

        public async Task<IEnumerable<Language>> GetAllAsync()
        {
            var list = new List<Language>();
            
            await _conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(
                "SELECT \"Id\", \"Name\" FROM \"Language\"",
                _conn);

            await using var rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                list.Add(new Language
                {
                    Id   = rdr.GetGuid(0),
                    Name = rdr.GetString(1)
                });
            }
            return list;
        }

        public async Task<Language?> GetByIdAsync(Guid id)
        {
            
            await _conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(
                "SELECT \"Id\", \"Name\" FROM \"Language\" WHERE \"Id\" = @id",
                _conn);
            cmd.Parameters.AddWithValue("id", id);

            await using var rdr = await cmd.ExecuteReaderAsync();
            return await rdr.ReadAsync()
                ? new Language { Id = rdr.GetGuid(0), Name = rdr.GetString(1) }
                : null;
        }

        public async Task CreateAsync(Language lang)
        {
            
            await _conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(
                "INSERT INTO \"Language\" (\"Id\", \"Name\") VALUES (@id, @name)",
                _conn);
            cmd.Parameters.AddWithValue("id", lang.Id);
            cmd.Parameters.AddWithValue("name", lang.Name);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Language lang)
        {
            
            await _conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(
                "UPDATE \"Language\" SET \"Name\" = @name WHERE \"Id\" = @id",
                _conn);
            cmd.Parameters.AddWithValue("id",   lang.Id);
            cmd.Parameters.AddWithValue("name", lang.Name);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            
            await _conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(
                "DELETE FROM \"Language\" WHERE \"Id\" = @id",
                _conn);
            cmd.Parameters.AddWithValue("id", id);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
