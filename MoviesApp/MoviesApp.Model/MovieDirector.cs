using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Model
{
    public class MovieDirector
    {
        [Key]
        public Guid MovieId { get; set; }

        [Key]
        public Guid DirectorId { get; set; }
    }
}
