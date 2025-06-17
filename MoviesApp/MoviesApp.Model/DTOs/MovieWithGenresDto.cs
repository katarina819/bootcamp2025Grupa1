public class MovieWithGenreDto
{
    public Guid MovieId { get; set; }
    public string MovieName { get; set; } = null!;
    public int Duration { get; set; }
    public float Rating { get; set; }
    public int ReleaseYear { get; set; }
    public string Description { get; set; } = null!;

    public Guid GenreId { get; set; }
    public string GenreName { get; set; } = null!;
}
