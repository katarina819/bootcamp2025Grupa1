namespace MoviesApp.DTO
{
    public class DirectorDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;   
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
    }
}
