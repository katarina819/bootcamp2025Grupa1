namespace MoviesApp.DTO
{
    /// <summary>
    /// Data Transfer Object for representing a Director.
    /// </summary>
    public class DirectorDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the director.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the director.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
