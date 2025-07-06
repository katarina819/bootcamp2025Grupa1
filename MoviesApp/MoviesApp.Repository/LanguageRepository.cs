using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using MoviesApp.Model;
using MoviesApp.Repository.Common;

namespace MoviesApp.Repository
{
    /// <summary>
    /// Repository for managing Language entities.
    /// Provides methods for CRUD operations on Language table using PostgreSQL.
    /// </summary>
    public class LanguageRepository : ILanguageRepository
    {
        private readonly NpgsqlConnection _conn;

        /// <summary>
        /// Initializes a new instance of <see cref="LanguageRepository"/> with the specified PostgreSQL connection.
        /// </summary>
        /// <param name="conn">The NpgsqlConnection to the database.</param>
        public LanguageRepository(NpgsqlConnection conn) => _conn = conn;

        /// <summary>
        /// Retrieves all languages from the database.
        /// </summary>
        /// <returns>A collection of <see cref="Language"/> objects.</returns>
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
                    Id = rdr.GetGuid(0),
                    Name = rdr.GetString(1)
                });
            }
            return list;
        }

        /// <summary>
        /// Retrieves a language by its unique identifier.
        /// </summary>
        /// <param name="id">The Id of the language to retrieve.</param>
        /// <returns>The <see cref="Language"/> object if found; otherwise, null.</returns>
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

        /// <summary>
        /// Inserts a new language into the database.
        /// </summary>
        /// <param name="lang">The <see cref="Language"/> object to create.</param>
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

        /// <summary>
        /// Updates an existing language in the database.
        /// </summary>
        /// <param name="lang">The <see cref="Language"/> object with updated information.</param>
        public async Task UpdateAsync(Language lang)
        {
            await _conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(
                "UPDATE \"Language\" SET \"Name\" = @name WHERE \"Id\" = @id",
                _conn);
            cmd.Parameters.AddWithValue("id", lang.Id);
            cmd.Parameters.AddWithValue("name", lang.Name);
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Deletes a language from the database by its Id.
        /// </summary>
        /// <param name="id">The Id of the language to delete.</param>
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
