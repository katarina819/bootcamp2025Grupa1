using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.DTO
{
    public class MovieCreateDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required int Duration { get; set; }
        public float? Rating { get; set; }
        public required int ReleaseYear { get; set; }
        public string? Description { get; set; }
        public required string DirectorName { get; set; }
        public required IList<Guid> Genres { get; set; }
        public required IList<Guid> Languages { get; set; }
    }
}
