using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesAppModel
{
    public class GenreModels
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]

        public string Name { get; set; }
    }
}

