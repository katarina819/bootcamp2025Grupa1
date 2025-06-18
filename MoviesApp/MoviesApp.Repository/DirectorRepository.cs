using Npgsql;
using MoviesApp.Model;
using MoviesApp.Pagination;

namespace MoviesApp.Repository
{
    public class DirectorRepository : IDirectorRepository
    {
        private readonly NpgsqlConnection _connection;

        public DirectorRepository(NpgsqlConnection connection) => _connection = connection ?? throw new ArgumentNullException(nameof(connection));

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
                using var getRows = new NpgsqlCommand(@"SELECT ""Id"", ""FirstName"", ""LastName"" FROM ""Director"" ORDER BY ""LastName"", ""FirstName"" LIMIT @PageSize OFFSET @Offset", _connection);
                getRows.Parameters.AddWithValue("PageSize", pageSize);
                getRows.Parameters.AddWithValue("Offset", offset);

                using var reader = await getRows.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Items.Add(new Director
                    {
                        Id = reader.GetGuid(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2)
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
        
   
        public async Task<Director?> GetDirectorByIdAsync(Guid id) 
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    await _connection.OpenAsync();
                }

                using var comm = new NpgsqlCommand(@"SELECT ""Id"", ""FirstName"", ""LastName"" FROM ""Director"" WHERE ""Id"" = @Id", _connection);
                comm.Parameters.AddWithValue("Id", id);

                using var reader = await comm.ExecuteReaderAsync();
                if (await reader.ReadAsync()) 
                {
                    return new Director
                    {
                        Id = reader.GetGuid(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2)
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

        public async Task<Director> CreateDirectorAsync(Director director) 
        {
            if (director == null) throw new ArgumentNullException(nameof(director));
            if (string.IsNullOrWhiteSpace(director.FirstName) || string.IsNullOrWhiteSpace(director.LastName))
                throw new ArgumentException("FirstName and LastName are required.");

            director.Id = Guid.NewGuid();

            try 
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    await _connection.OpenAsync();
                }

                using var comm = new NpgsqlCommand(@"INSERT INTO ""Director"" (""Id"", ""FirstName"", ""LastName"") VALUES (@Id,@FirstName, @LastName)", _connection);
                comm.Parameters.AddWithValue("Id", director.Id);
                comm.Parameters.AddWithValue("FirstName", director.FirstName);
                comm.Parameters.AddWithValue("LastName", director.LastName);

                await comm.ExecuteNonQueryAsync();
                return director;
            }
            finally 
            {
                if (_connection != null)
                { 
                    await (_connection.CloseAsync());
                }
            }
        }


        public async Task<bool> UpdateDirectorAsync(Director director) 
        {
            if (director == null) throw new ArgumentNullException(nameof(director));
            if (string.IsNullOrWhiteSpace(director.FirstName) || String.IsNullOrWhiteSpace(director.LastName))
                throw new ArgumentException("FirstName and LastName are required.");
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    await _connection.OpenAsync();
                }

                using var comm = new NpgsqlCommand(@"UPDATE ""Director"" SET ""FirstName"" = @FirstName, ""LastName"" = @LastName WHERE ""Id"" = @Id", _connection);
                comm.Parameters.AddWithValue("Id", director.Id);
                comm.Parameters.AddWithValue("FirstName", director.FirstName);
                comm.Parameters.AddWithValue("LastName", director.LastName);

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
