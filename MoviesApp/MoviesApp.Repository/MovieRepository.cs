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
                string genreString = reader.IsDBNull(5) ? "" : reader.GetString(5);

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

            using (var command = new NpgsqlCommand("DELETE FROM \"Movie\" WHERE \"Id\" = @id;", _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateMovieAsync(MovieCreateDto movie)
        {
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }

            //check director

            Guid directorId = Guid.Empty;

            using (var selectCommand = new NpgsqlCommand("SELECT \"Id\" FROM \"Director\" WHERE \"Name\" ILIKE @directorName;", _connection))
            {
                selectCommand.Parameters.AddWithValue("@directorName", movie.DirectorName);

                using var reader = await selectCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    directorId = reader.GetGuid(0);
                }
            }

            if (directorId == Guid.Empty)
            {
                directorId = Guid.NewGuid();

                using (var insertCommand = new NpgsqlCommand("INSERT INTO \"Director\" (\"Id\", \"Name\") VALUES (@id, @name);", _connection))
                {
                    insertCommand.Parameters.AddWithValue("@id", directorId);
                    insertCommand.Parameters.AddWithValue("@name", movie.DirectorName);

                    await insertCommand.ExecuteNonQueryAsync();
                }
            }

            //check genres

            var existingGenreIds = new List<Guid>();

            using (var command = new NpgsqlCommand("SELECT \"GenreId\" FROM \"MovieGenre\" WHERE \"MovieId\" = @movieId", _connection))
            {
                command.Parameters.AddWithValue("@movieId", movie.Id);

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    existingGenreIds.Add(reader.GetGuid(0));
                }
            }
            var toAdd = movie.Genres.Except(existingGenreIds).ToList();       
            var toRemove = existingGenreIds.Except(movie.Genres).ToList();

            if (toRemove.Count != 0)
            {
                var deleteCommandText = "DELETE FROM \"MovieGenre\" WHERE \"MovieId\" = @movieId AND \"GenreId\" = ANY(@toRemove)";
                using (var deleteCommand = new NpgsqlCommand(deleteCommandText, _connection))
                {
                    deleteCommand.Parameters.AddWithValue("@movieId", movie.Id);
                    deleteCommand.Parameters.AddWithValue("@toRemove", toRemove.ToArray());
                    await deleteCommand.ExecuteNonQueryAsync();
                }
            }
            if (toAdd.Count != 0)
            {
                
                foreach (var genreId in toAdd)
                {
                    using (var insertCommand = new NpgsqlCommand("INSERT INTO \"MovieGenre\" (\"MovieId\", \"GenreId\") VALUES (@movieId, @genreId)", _connection))
                    {
                        insertCommand.Parameters.AddWithValue("@movieId", movie.Id);
                        insertCommand.Parameters.AddWithValue("@genreId", genreId);
                        await insertCommand.ExecuteNonQueryAsync();
                    }
                }
            }

            //check languages
            var existingLanguageIds = new List<Guid>();

            using (var command = new NpgsqlCommand("SELECT \"LanguageId\" FROM \"MovieLanguage\" WHERE \"MovieId\" = @movieId", _connection))
            {
                command.Parameters.AddWithValue("@movieId", movie.Id);

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    existingLanguageIds.Add(reader.GetGuid(0));
                }
            }
            toAdd = movie.Languages.Except(existingLanguageIds).ToList();
            toRemove = existingLanguageIds.Except(movie.Languages).ToList();

            if (toRemove.Count != 0)
            {
                var deleteCommandText = "DELETE FROM \"MovieLanguage\" WHERE \"MovieId\" = @movieId AND \"LanguageId\" = ANY(@toRemove)";
                using (var deleteCommand = new NpgsqlCommand(deleteCommandText, _connection))
                {
                    deleteCommand.Parameters.AddWithValue("@movieId", movie.Id);
                    deleteCommand.Parameters.AddWithValue("@toRemove", toRemove.ToArray());
                    await deleteCommand.ExecuteNonQueryAsync();
                }
            }
            if (toAdd.Count != 0)
            {
                foreach (var languageId in toAdd)
                {
                    using (var insertCommand = new NpgsqlCommand("INSERT INTO \"MovieLanguage\" (\"MovieId\", \"LanguageId\") VALUES (@movieId, @languageId)", _connection))
                    {
                        insertCommand.Parameters.AddWithValue("@movieId", movie.Id);
                        insertCommand.Parameters.AddWithValue("@languageId", languageId);
                        await insertCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            //update movie
            using (var command = new NpgsqlCommand("UPDATE \"Movie\" SET \"Name\" = @name, \"Duration\" = @duration, \"Rating\" = @rating, \"ReleaseYear\" = @releaseYear, \"Description\" = @description, \"DirectorId\" = @directorId WHERE \"Id\" = @id;", _connection))
            {
                command.Parameters.AddWithValue("@name", movie.Name);
                command.Parameters.AddWithValue("@duration", movie.Duration);
                command.Parameters.AddWithValue("@rating", movie.Rating);
                command.Parameters.AddWithValue("@releaseYear", movie.ReleaseYear);
                command.Parameters.AddWithValue("@description", movie.Description);
                command.Parameters.AddWithValue("@directorId", directorId);
                command.Parameters.AddWithValue("@id", movie.Id);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task AddMovieAsync(MovieCreateDto movie)
        {
            
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }

            Guid directorId = Guid.Empty;

            using (var selectCommand = new NpgsqlCommand("SELECT \"Id\" FROM \"Director\" WHERE \"Name\" ILIKE @directorName;", _connection))
            {
                selectCommand.Parameters.AddWithValue("@directorName", movie.DirectorName.Trim());

                using var reader = await selectCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    directorId = reader.GetGuid(0);
                }
            }

            if (directorId == Guid.Empty)
            {
                directorId = Guid.NewGuid();

                using (var insertCommand = new NpgsqlCommand("INSERT INTO \"Director\" (\"Id\", \"Name\") VALUES (@id, @name);", _connection))
                {
                    insertCommand.Parameters.AddWithValue("@id", directorId);
                    insertCommand.Parameters.AddWithValue("@name", movie.DirectorName.Trim());

                    await insertCommand.ExecuteNonQueryAsync();
                }
            }
            
            using (var command = new NpgsqlCommand("INSERT INTO \"Movie\" VALUES (@id, @name, @duration, @rating, @releaseYear, @description, @directorId);", _connection))
            {
                command.Parameters.AddWithValue("@id", movie.Id);
                command.Parameters.AddWithValue("@name", movie.Name);
                command.Parameters.AddWithValue("@duration", movie.Duration);
                command.Parameters.AddWithValue("@rating", movie.Rating);
                command.Parameters.AddWithValue("@releaseYear", movie.ReleaseYear);
                command.Parameters.AddWithValue("@description", movie.Description);
                command.Parameters.AddWithValue("@directorId", directorId);
                await command.ExecuteNonQueryAsync();
            }

            foreach(var genreId in movie.Genres)
            {
                using (var command = new NpgsqlCommand("INSERT INTO \"MovieGenre\" VALUES (@id, @genreId);", _connection))
                {
                    command.Parameters.AddWithValue("@id", movie.Id);
                    command.Parameters.AddWithValue("@genreId", genreId);
                    await command.ExecuteNonQueryAsync();
                }
            }

            foreach (var languageId in movie.Languages)
            {
                using (var command = new NpgsqlCommand("INSERT INTO \"MovieLanguage\" VALUES (@id, @languageId);", _connection))
                {
                    command.Parameters.AddWithValue("@id", movie.Id);
                    command.Parameters.AddWithValue("@languageId", languageId);
                    await command.ExecuteNonQueryAsync();
                }
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

        public async Task<Paginated<MovieDetailsDto>> GetMoviesSortedAsync(string sortBy, string order, int page = 1, int pageSize = 10, Guid? genreId = null)
        {
            page = Math.Max(1, page);
            var result = new Paginated<MovieDetailsDto>
            {
                Page = page,
                PageSize = pageSize,
                Items = new List<MovieDetailsDto>()
            };

            var baseQuery = @"SELECT * FROM ""MovieFullView"" ";
            var whereClause = "";
            if (genreId.HasValue)
            {
                whereClause = @"WHERE ""Id"" IN (
                                SELECT m.""Id""
                                FROM ""Movie"" m
                                JOIN ""MovieGenre"" mg ON m.""Id"" = mg.""MovieId""
                                WHERE mg.""GenreId"" = @genreId
                            )";
            }
            var orderClause = $@"ORDER BY ""{sortBy}"" {order}
                              LIMIT @pageSize OFFSET @offset;";
            var query = baseQuery + whereClause + " " + orderClause;

            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using var countRows = new NpgsqlCommand(@"SELECT COUNT(*) FROM ""MovieFullView"" " + whereClause + ";", _connection);
            if (genreId.HasValue)
            {
                countRows.Parameters.AddWithValue("genreId", genreId.Value);
            }
            result.TotalCount = Convert.ToInt32(await countRows.ExecuteScalarAsync());

            var offset = (page - 1) * pageSize;
            using var getRows = new NpgsqlCommand(query, _connection);
            getRows.Parameters.AddWithValue("pageSize", pageSize);
            getRows.Parameters.AddWithValue("offset", offset);
            if (genreId.HasValue)
            {
                getRows.Parameters.AddWithValue("genreId", genreId.Value);
            }


            using var reader = await getRows.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                string genreString = reader.IsDBNull(7) ? "" : reader.GetString(7);

                var genres = genreString
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .Distinct()
                    .ToList();

                string languageString = reader.IsDBNull(8) ? "" : reader.GetString(8);

                var languages = languageString
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .Distinct()
                    .ToList();

                result.Items.Add(new MovieDetailsDto
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Duration = reader.GetInt32(2),
                    ReleaseYear = reader.GetInt32(3),
                    Rating = reader.GetFloat(4),
                    Description = reader.GetString(5),
                    DirectorName = reader.GetString(6),
                    Genres = genres,
                    Languages = languages
                });

            }
            return result;
        }
       

        public async Task<Paginated<MovieDto>> GetMoviesFilterNameAsync(string filter, int page = 1, int pageSize = 10)
        {
            var result = new Paginated<MovieDto>
            {
                Page = page,
                PageSize = pageSize,
                Items = new List<MovieDto>()
            };
            var query = "SELECT * FROM \"MovieView\" WHERE LOWER(\"Name\") ILIKE @filter LIMIT @PageSize OFFSET @Offset;";
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using var countRows = new NpgsqlCommand("SELECT COUNT(*) FROM \"MovieView\" WHERE LOWER(\"Name\") ILIKE @filter", _connection);
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
                        Genres = genres
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
        public async Task<DirectorCreateDto> GetDirectorByIdAsync(Guid id)
        {
            
            var query = "SELECT \"Name\" FROM \"Director\" WHERE \"Id\" = @id;";
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
                    return new DirectorCreateDto
                    {
                        Name = reader.GetString(0)
                    };
                }
            }
           
        }

        public async Task<List<MovieNameDto>> GetFilteredMovieNamesAsync(string filter)
        {
            var results = new List<MovieNameDto>();
            var query = "SELECT * FROM \"Movie\" WHERE \"Name\" ILIKE @filter;";
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            using var command = new NpgsqlCommand(query, _connection);
            command.Parameters.AddWithValue("@filter", $"%{filter.ToLower()}%");
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    results.Add(new MovieNameDto
                    {
                        Id = reader.GetGuid(0),
                        Name = reader.GetString(1),
                    });
                }
            }
            return results;
        }


    }
}


