using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.DTO
{
    public class MovieDetailsDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required int Duration { get; set; }
        public float? Rating { get; set; }
        public required int ReleaseYear { get; set; }
        public string? Description { get; set; }
        public string DirectorName { get; set; }
        public IList<string> Genres { get; set; }
        public IList<string> Languages { get; set; }
    }
}
