using Npgsql;
using MoviesApp.Model;
using MoviesApp.Pagination;
using MoviesApp.Repository.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApp.Repository
{
    /// <summary>
    /// Repository for managing Director entities in the database.
    /// Provides methods to create, read, update, and delete Directors.
    /// Uses Npgsql to interact with a PostgreSQL database.
    /// </summary>
    public class DirectorRepository : IDirectorRepository
    {
        private readonly NpgsqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectorRepository"/> class with a given PostgreSQL connection.
        /// </summary>
        /// <param name="connection">An open or closed NpgsqlConnection to the database.</param>
        /// <exception cref="ArgumentNullException">Thrown when the connection is null.</exception>
        public DirectorRepository(NpgsqlConnection connection) => _connection = connection ?? throw new ArgumentNullException(nameof(connection));

        /// <summary>
        /// Retrieves a paginated list of Directors ordered by their name.
        /// </summary>
        /// <param name="page">The page number to retrieve (1-based).</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A <see cref="Paginated{Director}"/> containing the page of Directors and pagination info.</returns>
        /// <exception cref="ArgumentException">Thrown if page or pageSize is less than 1.</exception>
        public async Task<Paginated<Director>> GetAllDirectorsAsync(int page = 1, int pageSize = 10)
        {
            if (page < 1) throw new ArgumentException("Page number must be greater than 0.", nameof(page));
            if (pageSize < 1) throw new ArgumentException("Page size must be greater than 0.", nameof(pageSize));

            var result = new Paginated<Director>
            {
                Page = page,
                PageSize = pageSize,
                Items = new List<Director>()
            };

            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    await _connection.OpenAsync();
                }

                using var countRows = new NpgsqlCommand(@"SELECT COUNT(*) FROM ""Director""", _connection);
                result.TotalCount = Convert.ToInt32(await countRows.ExecuteScalarAsync());

                var offset = (page - 1) * pageSize;
                using var getRows = new NpgsqlCommand(@"SELECT ""Id"", ""Name"" FROM ""Director"" ORDER BY ""Name"" LIMIT @PageSize OFFSET @Offset", _connection);
                getRows.Parameters.AddWithValue("PageSize", pageSize);
                getRows.Parameters.AddWithValue("Offset", offset);

                using var reader = await getRows.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Items.Add(new Director
                    {
                        Id = reader.GetGuid(0),
                        Name = reader.GetString(1)
                    });
                }
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    await _connection.CloseAsync();
                }
            }

            return result;
        }

        /// <summary>
        /// Retrieves a Director entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Director.</param>
        /// <returns>The <see cref="Director"/> if found; otherwise, null.</returns>
        public async Task<Director?> GetDirectorByIdAsync(Guid id)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    await _connection.OpenAsync();
                }

                using var comm = new NpgsqlCommand(@"SELECT ""Id"", ""Name"" FROM ""Director"" WHERE ""Id"" = @Id", _connection);
                comm.Parameters.AddWithValue("Id", id);

                using var reader = await comm.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Director
                    {
                        Id = reader.GetGuid(0),
                        Name = reader.GetString(1),
                    };
                }
                return null;
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    await _connection.CloseAsync();
                }
            }
        }

        /// <summary>
        /// Creates a new Director entity in the database.
        /// </summary>
        /// <param name="director">The Director entity to create. The Id will be assigned.</param>
        /// <returns>The created Director with its newly assigned Id.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the director argument is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the director's Name is null or whitespace.</exception>
        public async Task<Director> CreateDirectorAsync(Director director)
        {
            if (director == null) throw new ArgumentNullException(nameof(director));
            if (string.IsNullOrWhiteSpace(director.Name))
                throw new ArgumentException("Name is required.");

            director.Id = Guid.NewGuid();

            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    await _connection.OpenAsync();
                }

                using var comm = new NpgsqlCommand(@"INSERT INTO ""Director"" (""Id"", ""Name"") VALUES (@Id,@Name)", _connection);
                comm.Parameters.AddWithValue("Id", director.Id);
                comm.Parameters.AddWithValue("Name", director.Name);

                await comm.ExecuteNonQueryAsync();
                return director;
            }
            finally
            {
                if (_connection != null)
                {
                    await _connection.CloseAsync();
                }
            }
        }

        /// <summary>
        /// Updates an existing Director entity in the database.
        /// </summary>
        /// <param name="director">The Director entity with updated data.</param>
        /// <returns>True if the update affected any rows; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the director argument is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the director's Name is null or whitespace.</exception>
        public async Task<bool> UpdateDirectorAsync(Director director)
        {
            if (director == null) throw new ArgumentNullException(nameof(director));
            if (string.IsNullOrWhiteSpace(director.Name))
                throw new ArgumentException("Name is required.");

            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    await _connection.OpenAsync();
                }

                using var comm = new NpgsqlCommand(@"UPDATE ""Director"" SET ""Name"" = @Name WHERE ""Id"" = @Id", _connection);
                comm.Parameters.AddWithValue("Id", director.Id);
                comm.Parameters.AddWithValue("Name", director.Name);

                var affectedRows = await comm.ExecuteNonQueryAsync();
                return affectedRows > 0;
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    await _connection.CloseAsync();
                }
            }
        }

        /// <summary>
        /// Deletes a Director entity from the database by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Director to delete.</param>
        /// <returns>True if the deletion affected any rows; otherwise, false.</returns>
        public async Task<bool> DeleteDirectorAsync(Guid id)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    await _connection.OpenAsync();
                }

                using var comm = new NpgsqlCommand(@"DELETE FROM ""Director"" WHERE ""Id"" = @Id", _connection);
                comm.Parameters.AddWithValue("Id", id);

                var affectedRows = await comm.ExecuteNonQueryAsync();
                return affectedRows > 0;
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    await _connection.CloseAsync();
                }
            }
        }
    }
}
