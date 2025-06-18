using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Model
{
    public class Genre
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]

        public required string Name { get; set; }
    }
}

