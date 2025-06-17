

using System.ComponentModel.DataAnnotations;

namespace MoviesAppModel
{
    public class MovieModels
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = null!;

        [Range(1, 600)]
        public int Duration { get; set; }

        [Range(0, 10)]
        public float Rating { get; set; }

        [Range(1900, 2100)]
        public int ReleaseYear { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }
    }
}